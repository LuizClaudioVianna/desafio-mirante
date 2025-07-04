# desafio-mirante
Desafio desenvolvido para atender compôr equipe da empresa
Projeto API (DesafioMirante.API)
  Pacotes instalados:
    Microsoft.EntityFrameworkCore.InMemory -- que é um provedor de Entity Framework Core que usa um banco de dados em memória;
    Microsoft.Extensions.Http -- é um pacote do .NET que fornece extensões para configurar e usar HttpClient de forma robusta e eficiente, integrando com o sistema de injeção de dependência (DI) da ASP.NET Core;
    Swashbuckle.AspNetCore -- é um pacote extremamente útil no ecossistema ASP.NET Core, pois ele gera automaticamente documentação interativa da sua API REST no formato Swagger/OpenAPI.

#### Projeto DesafioMirante.Domain
  Pasta de Entidades - Para as entidades do sistema
  Pasta de Enuns - Para os enuns do sistema
  Pasta de Interfaces - Para os contratos/interfaces do repositório

#### Projeto DesafioMirante.Infra
  Pasta Context - Para a classe de contexto da aplicação
  Pasta Repositories - Para a classe de repositorio

#### Devido a instalação de um banco de dados InMemory, ao baixar o projeto e executá-lo na IDE ele deve exibir a documentação do Swagger com a exposição dos endpoints:
  * GET - api/Tarefa - Listando todas as tarefas (Iniciando com 3 em memória);
  * POST - api/Tarefa - Permitindo a inclusão;
  * GET - api/Tarefa/{id} - Permitindo buscar por id;
  * PUT - api/Tarefa/{id} - Permitindo a atualização;
  * DELETE - api/Tarefa/{id} - Permitindo a remoção;
  * GET - /api/Tarefa/obter-por-status?status=Pendente' - Permitindo a filtragem por status 'Pendente';
  * GET - /api/Tarefa/obter-por-data-vencimento?data=2025-08-03' - Permitindo a filtragem por data de vencimento '2025-08-03';
