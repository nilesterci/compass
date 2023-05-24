using Questao5.Domain.Entities;

namespace Questao5.Domain
{
    public interface IHomeRepository
    {
        Task<Movimento> Get(Movimento movimento);
        ContaCorrente ValidaConta(string idcontacorrente, int? numero);
        Task<Saldo> Saldo(string idcontacorrente);
    }
}
