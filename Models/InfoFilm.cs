namespace GestionCinema.Models
{
    public class InfoFilm
    {
        public string IdFilm { get; set; }
        public string Titre { get; set; }
        public string CategorieId { get; set; }
        public TimeSpan Duree { get; set; }
        public byte[] Image { get; set; }
        public string ClassificationId { get; set; }
        public string Categorie { get; set; }
        public string Classification { get; set; }

        // public string ImageBase64
        // {
        //     get
        //     {
        //         return Image != null && Image.Length > 0
        //             ? $"data:image/png;base64,{Convert.ToBase64String(Image)}"
        //             : null;
        //     }
        // }
    }
}
