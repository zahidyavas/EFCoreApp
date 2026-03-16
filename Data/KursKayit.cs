using System.ComponentModel.DataAnnotations;

namespace EFCoreApp.Data
{
    public class KursKayit
    {
        [Key]
        [Display(Name = "Kayıt Id")]
        public int KayitId { get; set; }
        public int OgrenciId { get; set; }
        public Ogrenci Ogrenci { get; set; } = null!;
        public int KursId { get; set; }
        public Kurs Kurs { get; set; } = null!;
        public DateTime KursKayitTarih { get; set; }
    }
}
