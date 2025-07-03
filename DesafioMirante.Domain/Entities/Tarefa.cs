using DesafioMirante.Domain.Enuns;
using System.Text.Json.Serialization;

namespace DesafioMirante.Domain.Entities
{
    public class Tarefa
    {
        public virtual int Id { get; set; }
        public virtual string Titulo { get; set; }
        public virtual string Descricao { get; set; }
        public virtual DateTime DataVencimento { get; set; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public virtual StatusEnum Status { get; set; }
    }
}
