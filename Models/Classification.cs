namespace GestionCinema.Models
{
    public class Classification
    {
        public string IdClasse { get; set; }
        public string classification { get; set; }

        // Constructor
        public Classification(string classificationName)
        {
            classification = classificationName;
        }
        public Classification(){}
    }
}