using System.ComponentModel.DataAnnotations;

namespace EFCoreApp.Data
{
    public class Ogretmen
    {
        [Key]
        [Required]
        [Display(Name = "Öğretmen Id")]
        public int OgretmenId { get; set; }
        [Display(Name = "İsim")]
        public string? OgretmenAd { get; set; }
        [Display(Name = "Soyisim")]
        public string? OgretmenSoyad { get; set; }
        public string? AdSoyadOgretmen 
        { 
            get
            {
                return $"{OgretmenAd} {OgretmenSoyad}";
            }
        }
        [Display(Name = "E-posta")]
        public string? OgretmenEposta { get; set; }
        [Display(Name = "Telefon Numarası")]
        public string? OgretmenTelefon { get; set; }

        [DataType(DataType.Date)]
        public DateTime BaşlamaTarihi { get; set; }
        public ICollection<Kurs> Kurslar { get; set; } = new List<Kurs>();
    }
}
