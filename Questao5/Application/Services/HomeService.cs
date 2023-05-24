using Questao5.Domain;
using Questao5.Domain.Entities;

namespace Questao5.Application.Services
{
    public class HomeService : IHomeService
    {
        private readonly IHomeRepository _repositorio;
        public HomeService(IHomeRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public ContaCorrente ValidaConta(string idcontacorrente, int? numero)
        {
            try
            {
                return _repositorio.ValidaConta(idcontacorrente, numero);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Movimento> Get(Movimento movimento)
        {
            try
            {
                return await _repositorio.Get(movimento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Saldo> Saldo(string idcontacorrente)
        {
            try
            {
                return await _repositorio.Saldo(idcontacorrente);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
