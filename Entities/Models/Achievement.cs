using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataClasses.Models
{

    public class Achievement
    {
        public string AchievementID { get; set; }
        public string ProductId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string XP { get; set; }
        public string IsHidden { get; set; }

        public Achievement(string achievementID, string productId, string name, string image, string description, string xP, string isHidden)
        {
            AchievementID = achievementID;
            ProductId = productId;
            Name = name;
            Image = image;
            Description = description;
            XP = xP;
            IsHidden = isHidden;
        }


    }

}
