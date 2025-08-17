namespace CreditAPI.DTOs
{
    public class KreditUpdateDto
    {
        public decimal Plafon { get; set; }
        public decimal Bunga { get; set; }
        public int Tenor { get; set; }
    }
}
