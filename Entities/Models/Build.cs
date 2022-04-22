using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataClasses.Models
{
    public class Build
    {
        public string BuildID { get; set; }
        public string ProductId { get; set; }
        public string DeveloperUserId { get; set; }
        public string Version { get; set; }
        public string Date { get; set; }
        public string BuildContent { get; set; }

        public Build(string buildID, string productId, string developerUserId, string version, string date, string buildContent)
        {
            BuildID = buildID;
            ProductId = productId;
            DeveloperUserId = developerUserId;
            Version = version;
            Date = date;
            BuildContent = buildContent;
        }
    }

}
