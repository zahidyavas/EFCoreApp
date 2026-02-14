using System.ComponentModel.DataAnnotations;
namespace EFCoreApp.Data
{
    public class Ogrenci
    {
        // Id dışında başka bir isim vermek istersek Örneğin OgrenciId gibi, o zaman Id'ye [Key] attribute'u eklememiz gerekir.
        [Display(Name = "Öğrenci Id")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        public int OgrenciId { get; set; } // Primary key

        [Display(Name = "Öğrenci İsim")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        public string? OgrenciAd { get; set; }

        [Display(Name = "Öğrenci Soyisim")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        public string? OgrenciSoyad { get; set; }

        [Display(Name = "Eposta Adresi")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        public string? Eposta { get; set; }

        [Display(Name = "Telefon Numarası")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        public string? TelefonNumarasi { get; set; }
    }
}
