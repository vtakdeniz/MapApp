using System;
namespace mapService.Models
{

    public class mapServiceDatabaseSettings //: ImapServiceDatabaseSettings
    {
        public mapServiceDatabaseSettings() { }

        public string PolygonCollectionName { get; set; }
        public string BranchCollectionName { get; set; }
        public string HospitalCollectionName { get; set; }
        public string HospitalPolygonCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

  /*  public interface ImapServiceDatabaseSettings
    {
        public string PolygonCollectionName { get; set; }
        public string BranchCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }*/
    
}
