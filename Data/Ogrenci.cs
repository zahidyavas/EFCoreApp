namespace EFCoreApp.Data
{
    public class Ogrenci
    {
        // Id dışında başka bir isim vermek istersek Örneğin OgrenciId gibi, o zaman Id'ye [Key] attribute'u eklememiz gerekir.
        public int OgrenciId { get; set; } // Primary key
        public string? OgrenciAd { get; set; }
        public string? OgrenciSoyad { get; set; }
        public string? Eposta { get; set; }
        public string? TelefonNumarasi { get; set; }
    }
}
