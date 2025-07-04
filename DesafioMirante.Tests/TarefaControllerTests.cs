using DesafioMirante.API.Controllers;
using DesafioMirante.Domain.Entities;
using DesafioMirante.Domain.Interface;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DesafioMirante.Tests
{
    public class TarefaControllerTests
    {
        private readonly Mock<ITarefaRepository> _mockRepo;
        private readonly TarefaController _controller;

        public TarefaControllerTests()
        {
            _mockRepo = new Mock<ITarefaRepository>();
            _controller = new TarefaController(_mockRepo.Object);
        }

        [Fact]
        public async Task Get_DeveRetornarOkComTarefas()
        {
            // Arrange
            var tarefas = new List<Tarefa>
            {
                new Tarefa { Id = 1, Titulo = "Teste 1" },
                new Tarefa { Id = 2, Titulo = "Teste 2" }
            };

            _mockRepo.Setup(repo => repo.ObterTodos()).ReturnsAsync(tarefas);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<Tarefa>>(okResult.Value);
            Assert.Equal(2, ((List<Tarefa>)returnValue).Count);
        }

        [Fact]
        public async Task GetPorId_DeveRetornarOkComTarefa()
        {
            var tarefa = new Tarefa { Id = 1, Titulo = "Tarefa 1" };
            _mockRepo.Setup(r => r.ObterPorId(1)).ReturnsAsync(tarefa);

            var result = await _controller.GetPorId(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Tarefa>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task GetPorId_DeveRetornarErroQuandoNaoExiste()
        {
            _mockRepo.Setup(r => r.ObterPorId(1)).ThrowsAsync(new InvalidOperationException());

            var result = await _controller.GetPorId(1);

            var status = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, status.StatusCode);
        }

        [Fact]
        public async Task Create_DeveRetornarOk()
        {
            var novaTarefa = new Tarefa
            {
                Id = 1,
                Titulo = "Nova",
                Descricao = "uma nova descrição",
                DataVencimento = DateTime.Now.AddDays(5),
                Status = Domain.Enuns.StatusEnum.Pendente
            };
            _mockRepo.Setup(r => r.Adicionar(novaTarefa)).ReturnsAsync(novaTarefa);

            var result = await _controller.Create(novaTarefa);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Tarefa>(okResult.Value);

            Assert.Equal("Nova", returnValue.Titulo);
        }

        [Fact]
        public async Task Delete_DeveRetornarOkQuandoEncontrado()
        {
            var tarefa = new Tarefa { Id = 1 };
            _mockRepo.Setup(r => r.ObterPorId(1)).ReturnsAsync(tarefa);

            var result = await _controller.Delete(1);

            Assert.IsType<OkResult>(result);
        }

        [Theory]
        [InlineData("pendente")]
        public async Task GetPorStatus_DeveRetornarOk(string status)
        {
            var tarefas = new List<Tarefa> { new Tarefa { Id = 1, Titulo = "Teste", Status = Domain.Enuns.StatusEnum.Pendente } };
            _mockRepo.Setup(r => r.ObterPorStatus(status)).ReturnsAsync(tarefas);

            var result = await _controller.GetPorStatus(status);

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(tarefas, ok.Value);
        }

        [Fact]
        public async Task GetPorDataVencimento_DeveRetornarOk_QuandoTarefasEncontradas()
        {
            // Arrange
            var data = new DateTime(2025, 07, 04);
            var tarefas = new List<Tarefa>
    {
        new Tarefa { Id = 1, Titulo = "Tarefa 1", DataVencimento = data },
        new Tarefa { Id = 2, Titulo = "Tarefa 2", DataVencimento = data }
    };

            var mockRepo = new Mock<ITarefaRepository>();
            mockRepo.Setup(r => r.ObterPorDataVencimento(data)).ReturnsAsync(tarefas);

            var controller = new TarefaController(mockRepo.Object);

            // Act
            var resultado = await controller.GetPorDataVencimento(data);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado.Result);
            var retorno = Assert.IsAssignableFrom<IEnumerable<Tarefa>>(okResult.Value);
            Assert.Equal(2, retorno.Count());
        }

        [Fact]
        public async Task GetPorDataVencimento_DeveRetornarErro500_QuandoExcecaoLancada()
        {
            // Arrange
            var data = new DateTime(2025, 07, 04);

            var mockRepo = new Mock<ITarefaRepository>();
            mockRepo.Setup(r => r.ObterPorDataVencimento(data)).ThrowsAsync(new Exception("Erro de banco"));

            var controller = new TarefaController(mockRepo.Object);

            // Act
            var resultado = await controller.GetPorDataVencimento(data);

            // Assert
            var erroResult = Assert.IsType<ObjectResult>(resultado.Result);
            Assert.Equal(500, erroResult.StatusCode);
            Assert.Equal("Erro ao obter tarefas por data!", erroResult.Value);
        }
    }
}
