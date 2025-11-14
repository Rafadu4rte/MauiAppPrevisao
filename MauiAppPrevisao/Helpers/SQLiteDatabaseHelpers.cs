using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiAppPrevisao.Models;
using SQLite;

namespace MauiAppPrevisao.Helpers
{
    public class SQLiteDatabaseHelpers
    {
        readonly SQLiteAsyncConnection _conn;

        public SQLiteDatabaseHelpers(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Usuario>().Wait();
            _conn.CreateTableAsync<HistoricoConsulta>().Wait();
        }

        // -------------------------
        // Usuários
        // -------------------------
        public Task<int> InsertUsuario(Usuario u)
        {
            return _conn.InsertAsync(u);
        }

        public Task<int> UpdateUsuario(Usuario u)
        {
            return _conn.UpdateAsync(u);
        }

        public Task<int> DeleteUsuario(int id)
        {
            return _conn.Table<Usuario>().DeleteAsync(i => i.Id == id);
        }

        public Task<List<Usuario>> GetAllUsuarios()
        {
            return _conn.Table<Usuario>().ToListAsync();
        }

        public Task<Usuario> GetUsuarioByEmail(string email)
        {
            string sql = "SELECT * FROM Usuario WHERE Email = ?";
            return _conn.QueryAsync<Usuario>(sql, email).ContinueWith(t => t.Result.FirstOrDefault());
        }

        // Método de login: retorna o usuário se email+senha baterem, senão null
        public async Task<Usuario> Login(string email, string senha)
        {
            try
            {
                List<Usuario> tmp = await _conn.QueryAsync<Usuario>(
                    "SELECT * FROM Usuario WHERE Email = ? AND Senha = ?", email, senha);

                return tmp.FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        // -------------------------
        // Histórico de consultas
        // -------------------------
        public Task<int> InsertHistorico(HistoricoConsulta h)
        {
            return _conn.InsertAsync(h);
        }

        public Task<int> DeleteHistorico(int id)
        {
            return _conn.Table<HistoricoConsulta>().DeleteAsync(i => i.Id == id);
        }

        public Task<List<HistoricoConsulta>> GetAllHistorico()
        {
            return _conn.Table<HistoricoConsulta>().ToListAsync();
        }

        public Task<List<HistoricoConsulta>> GetHistoricoByUsuario(int usuarioId)
        {
            string sql = "SELECT * FROM HistoricoConsulta WHERE UsuarioId = ? ORDER BY DataConsulta DESC";
            return _conn.QueryAsync<HistoricoConsulta>(sql, usuarioId);
        }

        // Buscar por cidade
        public Task<List<HistoricoConsulta>> SearchHistorico(int usuarioId, string cidade)
        {
            string sql = "SELECT * FROM HistoricoConsulta WHERE UsuarioId = ? AND Cidade LIKE '%' || ? || '%' ORDER BY DataConsulta DESC";
            return _conn.QueryAsync<HistoricoConsulta>(sql, usuarioId, cidade);
        }

        // Filtrar intervalo de datas
        public Task<List<HistoricoConsulta>> GetHistoricoIntervalo(int usuarioId, DateTime dataInicio, DateTime dataFim)
        {
            string sql = "SELECT * FROM HistoricoConsulta WHERE UsuarioId = ? AND DataConsulta >= ? AND DataConsulta <= ? ORDER BY DataConsulta DESC";
            return _conn.QueryAsync<HistoricoConsulta>(sql, usuarioId, dataInicio, dataFim);
        }
    } // Fecha classe SQLiteDatabaseHelpers
} // Fecha namespace MauiAppPrevisao.Helpers