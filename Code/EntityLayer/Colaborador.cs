
namespace EntityLayer
{
    public class Colaborador
    {
        public virtual int ColaboradorId { get; set; }
        public virtual Pessoa Pessoa { get; set; }
        public virtual string TipoPerfil { get; set; }
    }
}
