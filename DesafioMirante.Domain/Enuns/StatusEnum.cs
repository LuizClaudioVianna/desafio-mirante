using System.ComponentModel;

namespace DesafioMirante.Domain.Enuns
{
    public enum StatusEnum
    {
        [Description("Pendente")]
        Pendente,
        [Description("Em Andamento")]
        EmAndamento,
        [Description("Concluído")]
        Concluido
    }
}
