
namespace Fbtc.Domain.Entities
{
    public class ParametroAdimplencia
    {
        public int ParametroAdimplenciaId { get; set; }
        public bool NotificarVencAnuidade { get; set; }
        public bool NotificarVencEventos { get; set; }
        public bool EnviarBoletoEMail { get; set; }
        public int QtdDias { get; set; }
    }
}
