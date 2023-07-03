namespace ProjektProgramowanie
{
    public class lokaleToReturn : Model.lokale
    {
        public string avgOcena;
        public string avgCena;
        public lokaleToReturn(Model.lokale lokal, string avgOcena, string avgCena) {

         this.LokaleId=lokal.LokaleId;
         this.Nazwa=lokal.Nazwa;
         this.Miasto = lokal.Miasto;    
         this.Adres = lokal.Adres;
         this.Kuchnia=lokal.Kuchnia;
         this.avgOcena = avgOcena;
         this.avgCena=avgCena;
        }

    }
}
