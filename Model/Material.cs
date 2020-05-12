namespace App.Model 
{
    public class Material
    {
        public string id {get; set;}
        public string materialNumber  {get; set;}
        public string changedBy {get; set;}
        public string materialType {get; set;}
        public string industrySector {get; set;}
        public string eventType { get; set;}
        public long createDate {get; set;}
        public string sourceSystemId {get; set;}
    }
}