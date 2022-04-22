using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataClasses.Models
{

    public class Assistant
    {
        public string AssistantID { get; set; }
        public string AssistantUserName { get; set; }
        public string AssistantRealName { get; set; }
        public string AssistantPass { get; set; }

        public Assistant(string assistantID, string assistantUserName, string assistantRealName, string assistantPass)
        {
            AssistantID = assistantID;
            AssistantUserName = assistantUserName;
            AssistantRealName = assistantRealName;
            AssistantPass = assistantPass;
        }
    }

}
