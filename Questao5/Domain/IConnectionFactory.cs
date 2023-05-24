using System.Data;

namespace Questao5.Domain
{
    public interface IConnectionFactory
    {
        IDbConnection Connection();
    }
}
