﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WowDotNetAPI.Utilities;
using System.Net;
using System.Web.Script.Serialization;
using WowDotNetAPI.Models;
using WowDotNetAPI.Explorers.Test;

namespace WowDotNetAPI.Test
{
    [TestClass]
    public class GuildTests
    {
        private static WowExplorer explorer;
        private static Guild guild;
        private static string APIKey = TestStrings.APIKey;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            explorer = new WowExplorer(Region.US, Locale.en_US, APIKey);
            guild = explorer.GetGuild("korgath", "immortality", GuildOptions.GetEverything);
        }

        [TestMethod]
        public void Get_Simple_Guild_Immortality_From_Korgath()
        {
            Assert.IsTrue(guild.Realm.Equals("korgath", StringComparison.InvariantCultureIgnoreCase));
            Assert.AreEqual(UnitSide.ALLIANCE, guild.Side);
            Assert.IsTrue(guild.Members.Any());
        }

        [TestMethod]
        public void Get_Valid_Night_Elf_Member_From_Immortality_Guild()
        {
            var guildMember = guild.Members.Where(m => m.Character.Name.Equals("fleas", StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

            Assert.IsTrue(guildMember.Character.Name.Equals("fleas", StringComparison.InvariantCultureIgnoreCase));

            Assert.AreEqual(110, guildMember.Character.Level);
            Assert.AreEqual(CharacterClass.DRUID, guildMember.Character.@Class);
            Assert.AreEqual(CharacterRace.NIGHT_ELF, guildMember.Character.Race);
            Assert.AreEqual(CharacterGender.MALE, guildMember.Character.Gender);
        }

        [TestMethod]
        public void Get_Valid_Member_From_Another_Guild()
        {
            Guild guild = explorer.GetGuild("laughing skull", "deus vox", GuildOptions.GetMembers | GuildOptions.GetAchievements);


            Assert.IsNotNull(guild.Members);
            Assert.IsNotNull(guild.AchievementPoints);

            Assert.IsTrue(guild.Name.Equals("deus vox", StringComparison.InvariantCultureIgnoreCase));
            Assert.IsTrue(guild.Realm.Equals("laughing skull", StringComparison.InvariantCultureIgnoreCase));
            Assert.IsTrue(guild.Members.Any());

            Assert.AreEqual(UnitSide.ALLIANCE, guild.Side);
        }

        [TestMethod]
        public void Get_Valid_Member_From_Horde_Guild()
        {
            Guild guild = explorer.GetGuild("skullcrusher", "rage", GuildOptions.GetMembers);

            Assert.IsNotNull(guild.Members);
            Assert.IsNull(guild.Achievements);

            Assert.IsTrue(guild.Name.Equals("rage", StringComparison.InvariantCultureIgnoreCase));
            Assert.IsTrue(guild.Realm.Equals("skullcrusher", StringComparison.InvariantCultureIgnoreCase));

            Assert.IsTrue(guild.Members.Any());

            Assert.IsTrue(guild.Side == UnitSide.HORDE);
        }

        [TestMethod]
        public void Get_Guild_With_Only_Achievements()
        {
            Guild guild = explorer.GetGuild("skullcrusher", "immortality", GuildOptions.GetAchievements);


            Assert.IsNull(guild.Members);
            Assert.IsNotNull(guild.Achievements);

            Assert.IsTrue(guild.Realm.Equals("skullcrusher", StringComparison.InvariantCultureIgnoreCase));
            Assert.AreEqual(UnitSide.ALLIANCE, guild.Side);
        }

        [TestMethod]
        public void Get_Guild_With_Only_Members()
        {
            Guild guild = explorer.GetGuild("skullcrusher", "immortality", GuildOptions.GetMembers);

            Assert.IsNotNull(guild.Members);
            Assert.IsNull(guild.Achievements);

            Assert.IsTrue(guild.Realm.Equals("skullcrusher", StringComparison.InvariantCultureIgnoreCase));
            Assert.AreEqual(UnitSide.ALLIANCE, guild.Side);
        }

        [TestMethod]
        public void Get_Guild_With_Only_No_Options()
        {
            var guild = explorer.GetGuild("skullcrusher", "immortality", GuildOptions.None);

            Assert.IsNull(guild.Members);
            Assert.IsNull(guild.Achievements);

            Assert.IsTrue(guild.Realm.Equals("skullcrusher", StringComparison.InvariantCultureIgnoreCase));
            Assert.AreEqual(UnitSide.ALLIANCE, guild.Side);
        }

        [TestMethod]
        public void Get_Guild_With_Base_Method_Call()
        {
            var guild = explorer.GetGuild("skullcrusher", "immortality");

            Assert.IsNull(guild.Members);
            Assert.IsNull(guild.Achievements);

            Assert.IsTrue(guild.Realm.Equals("skullcrusher", StringComparison.InvariantCultureIgnoreCase));
            Assert.AreEqual(UnitSide.ALLIANCE, guild.Side);
        }


        [TestMethod]
        public void Get_Guild_With_Connected_Realms() {
            WowExplorer explorer2 = new WowExplorer(Region.EU, Locale.en_GB, APIKey);
            Guild guild2 = explorer2.GetGuild("darksorrow", "mentality", GuildOptions.GetMembers);
            List<GuildMember> guildMembers = guild2.Members.Where(x => x.Character.Name.Equals("Danishpala", StringComparison.CurrentCultureIgnoreCase)).ToList();
            Assert.AreEqual(0, guildMembers.Count(x => !x.Character.Realm.Equals(x.Character.GuildRealm)));
        }

	    [TestMethod]
        public void Get_Guild_With_Void_Elf_Character() {
            WowExplorer explorer2 = new WowExplorer(Region.EU, Locale.en_GB, APIKey);
            Guild guild2 = explorer2.GetGuild("argent dawn", "the chosen", GuildOptions.GetMembers);
            var guildMember = guild2.Members.FirstOrDefault(x => x.Character.Name.Equals("Odd", StringComparison.CurrentCultureIgnoreCase));
            Assert.IsTrue(guildMember != null && guildMember.Character.Race == CharacterRace.VOID_ELF);
        }
    }
}
