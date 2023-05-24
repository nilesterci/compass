using Questao5.Domain.Entities;

namespace Questao5.Application
{
    public interface IHomeService
    {
        Task<Movimento> Get(Movimento movimento);
        ContaCorrente ValidaConta(string idcontacorrente, int? numero);
        Task<Saldo> Saldo(string idcontacorrente);
    }
}
