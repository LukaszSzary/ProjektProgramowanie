namespace ProjektProgramowanie.Model
{
    public class lokale
    {
        public int LokaleId { get; set; }
        public string Nazwa { get; set; }
        public string Miasto { get; set; }
        public string Adres { get; set; }
        public string Kuchnia { get; set; }
        public List<opinie> Opinie { get; set; }
        //tu może być potrzebna IList
        public List<oferta> Oferta { get; set; }
        //tu może być potrzebna IList
        public List<promocjelokalu> Promocjelokalu { get; set; }
    }
}
