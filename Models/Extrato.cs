using System.ComponentModel;

namespace BancoApi.Models
{
    public class Extrato
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string User { get; set; } = string.Empty;
        public double Valor { get; set; }
        
    }
}