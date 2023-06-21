namespace ProjektProgramowanie.Model
{
    public class opinie
    {
        public int OpinieId { get; set; }
        public string Autor { get; set; }
        public string Opinia { get; set; }

        public DateTime DataWystawienia { get; set; }
        public int Ocena { get; set; }

        public int LokaleId { get; set; }

    }
}
