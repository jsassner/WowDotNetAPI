﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace WowDotNetAPI.Explorers.Models
{
    public class Guild
    {
        public string name { get; set; }
        public string realm { get; set; }
        public int side { get; set; }
        public int level { get; set; }
        public int achievementPoints { get; set; }
        public long lastModified { get; set; }

        public IEnumerable<Member> members { get; set; }

        //TODO:map enum for dictionary keys
        //achievementsCompleted
        //achievementsCompletedTimestamp
        //criteria 
        //criteriaQuantity
        //criteriaTimestamp
        //criteriaCreated

        public Dictionary<string, IEnumerable<long>> achievements { get; set; }

    }
}