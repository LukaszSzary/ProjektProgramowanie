namespace ProjektProgramowanie.Model
{
    public class oferta
    {
        public int OfertaId { get; set; }
        public int LokaleId { get; set; }
        public lokale Lokal { get; set; }
        public int DaniaId { get; set; }
        public dania Danie { get; set; }
    }
}
