using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MauiAppPrevisao.Models
{
    public class HistoricoConsulta
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Cidade { get; set; }
        public double Temperatura { get; set; }
        public string Clima { get; set; }
        public DateTime DataConsulta { get; set; }

        public int UsuarioId { get; set; }
    }
}
