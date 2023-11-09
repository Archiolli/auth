namespace BancoApi.Models
{
    public class Extrato
    {
        public int Id { get; set; }
        public double NumeroExtrato { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        
    }
}
