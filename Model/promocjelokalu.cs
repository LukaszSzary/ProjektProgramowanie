namespace ProjektProgramowanie.Model
{
    public class promocjelokalu
    {
        public int PromocjeLokaluId { get; set; }
        public int LokaleId { get; set; }
        public lokale Lokale { get; set; }
        public int PromocjeId { get; set; }
        public promocje Promocja { get; set; }
    }
}
