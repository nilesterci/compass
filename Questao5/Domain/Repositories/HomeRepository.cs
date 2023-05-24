using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;
using System.Text;

namespace Questao5.Domain.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly DatabaseConfig databaseConfig;
        public HomeRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }
        public ContaCorrente ValidaConta(string idcontacorrente, int? numero)
        {
            try
            {
                using var connection = new SqliteConnection(databaseConfig.Name);

                if (numero != null)
                {
                    var valida = connection.Query<ContaCorrente>($"SELECT * FROM  contacorrente cc where cc.numero = {numero} and cc.idcontacorrente = '{idcontacorrente.ToUpper()}';").FirstOrDefault();
                    return valida;
                }
                else
                {
                    var valida = connection.Query<ContaCorrente>($"SELECT * FROM  contacorrente cc where cc.idcontacorrente = '{idcontacorrente.ToUpper()}';").FirstOrDefault();
                    return valida;
                }
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
                using var connection = new SqliteConnection(databaseConfig.Name);

                await connection.ExecuteAsync("INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor)" +
    "VALUES (@idmovimento, @idcontacorrente, @datamovimento, @tipomovimento, @valor);", movimento);

                return movimento;
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
                StringBuilder sql = new StringBuilder();

                sql.Append("SELECT (SELECT SUM(m.valor) AS Credito ");
                sql.Append("          FROM movimento m ");
                sql.Append("         WHERE m.tipomovimento = 'C' ");
                sql.Append($"         AND m.idcontacorrente = '{idcontacorrente.ToUpper()}') - ");
                sql.Append("       (SELECT SUM(m.valor) AS Credito ");
                sql.Append("          FROM movimento m ");
                sql.Append("         WHERE m.tipomovimento = 'D' ");
                sql.Append($"         AND m.idcontacorrente = '{idcontacorrente.ToUpper()}') AS saldo, ");
                sql.Append("		 cc.nome, cc.numero, datetime('now') as dataconsulta ");
                sql.Append("		 from contacorrente cc ");
                sql.Append($"		 where cc.idcontacorrente = '{idcontacorrente.ToUpper()}' ; ");

                using var connection = new SqliteConnection(databaseConfig.Name);
                var saldo = connection.Query<Saldo>(sql.ToString()).FirstOrDefault();
                return saldo;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
