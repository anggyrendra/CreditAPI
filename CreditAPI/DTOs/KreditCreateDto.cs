namespace CreditAPI.DTOs
{
    public class KreditCreateDto
    {
        public decimal Plafon { get; set; }
        public decimal Bunga { get; set; }
        public int Tenor { get; set; }
    }
}
