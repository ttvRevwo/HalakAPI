using HalakAPI.Models;

namespace HalakAPI.DTOs
{
    public class HalakDTO
    {
        public int Id { get; set; }
        public string Faj { get; set; } = null!;
        public decimal MeretCm { get; set; }
        public virtual Tavak? To { get; set; }
    }
}
