namespace Questao5.Domain.Entities
{
    public class Movimento
    {
        public Guid idmovimento { get; set; }
        public Guid idcontacorrente { get; set; }
        public DateTime datamovimento { get; set; }
        public string tipomovimento { get; set; }
        public double valor { get; set; }

    }

    public class CadastrarMovimento
    {
        public Guid idmovimento { get; set; }
        public Guid idcontacorrente { get; set; }
        public DateTime datamovimento { get; set; }
        public string tipomovimento { get; set; }
        public double valor { get; set; }
        public int numero { get; set; }

    }
}
