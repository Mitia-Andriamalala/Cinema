namespace GestionCinema.Models
{
    public static class FonctionAutre
    {
        public static string ImageBase64(byte[] sary)
        {
            return sary != null && sary.Length > 0
                ? $"data:image/png;base64,{Convert.ToBase64String(sary)}"
                : null;
        }
    }
}
