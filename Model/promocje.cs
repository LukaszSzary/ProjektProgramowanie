namespace ProjektProgramowanie.Model
{
    public class promocje
    {
        public int PromocjeId { get; set; }
        public string Opis { get; set; }
        public DateTime DataRozpoczęcia { get; set; }
        public DateTime DataZakończenia { get; set; }
        //tu może być potrzebna IList
        public List<promocjelokalu> Promocjelokalu { get; set; }
    }
}
