namespace ProjektProgramowanie.Model
{
    public class dania
    {
        public int DaniaId { get; set; }
        public string Opis { get; set; }
        public float Cena { get; set; }
        public string Nazwa { get; set; }
        //tu może być potrzebna IList
        public IList<oferta> Oferta { get; set; }
    }
}
