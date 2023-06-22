namespace ProjektProgramowanie.Model
{
    public class lokale
    {
        public int LokaleId { get; set; }
        public string Nazwa { get; set; }
        public string Miasto { get; set; }
        public string Adres { get; set; }
        public string Kuchnia { get; set; }


        public List<opinie> Opinie { get;  } = new(); 
        
        public List<dania> Dania { get;  } = new();
        
        public List<promocje> Promocje { get;  } = new();
    }
}
