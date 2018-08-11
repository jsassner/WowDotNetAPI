using System.Runtime.Serialization;

namespace WowDotNetAPI.Models {
    [DataContract]
    public class CharacterItemAzerite {
        [DataMember(Name="azeriteLevel")]
        public int AzeriteLevel { get; set; }

        [DataMember(Name = "azeriteExperience")]
        public int AzeriteExperience { get; set; }

        [DataMember(Name = "azeriteExperienceRemaining")]
        public int AzeriteExperienceRemaining { get; set; }
    }
}
