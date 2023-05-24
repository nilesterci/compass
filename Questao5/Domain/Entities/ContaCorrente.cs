namespace Questao5.Domain.Entities
{
    public class ContaCorrente
    {
        public string idcontacorrente { get; set; }
        public int numero { get; set; }
        public string nome { get; set; }
        public bool ativo { get; set; }
    }

    public class Saldo
    {
        public double saldo { get; set; }
        public string nome { get; set; }
        public int numero { get; set; }
        public DateTime dataconsulta { get; set; }
    }
}
