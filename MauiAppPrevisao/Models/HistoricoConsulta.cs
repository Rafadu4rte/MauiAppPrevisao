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

        // Data em que a consulta foi feita
        public DateTime DataConsulta { get; set; }

        // Ex.: temperatura obtida (opcional campos extras)
        public double Temperatura { get; set; }

        // FK para o usuário que fez a consulta
        public int UsuarioId { get; set; }
    } // Fecha classe HistoricoConsulta
} // Fecha namespace MauiAppPrevisao.Models