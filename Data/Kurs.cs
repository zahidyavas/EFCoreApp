namespace EFCoreApp.Data
{
    public class Kurs
    {
        public int KursId { get; set; } // Id dışında Class'ın adını da kullanarak Primary key oluşturabiliriz. [Key] attribute'u eklememize gerek kalmaz.
        public string? Baslik { get; set; }

    }
}
