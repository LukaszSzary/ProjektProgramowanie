namespace ProjektProgramowanie.Model
{
    public class promocje
    {
        public int PromocjeId { get; set; }
        public string Opis { get; set; }
        public DateTime DataRozpoczęcia { get; set; }
        public DateTime DataZakończenia { get; set; }
        
        public List<lokale> Lokale { get;  } = new();
    }
}
