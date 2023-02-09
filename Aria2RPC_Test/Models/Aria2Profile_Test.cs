using Aria2RPC.Models;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Aria2RPC_Test.Models
{

    [TestClass]
    public class ProfileTestClass
    {
        public static Profile Aria2Profile { get; set; }
        public static string ProfilePath { get; set; } = Path.Combine("Test", "aria2.conf");


        [TestInitialize]
        public void TestInitialize()
        {
            Aria2Profile = new Profile();
        }


        [TestCleanup]
        public void TestCleanup()
        {
            if (File.Exists(ProfilePath)) File.Delete(ProfilePath);
        }

        [TestMethod]
        public void ConvertToJsonText_GetJsonFromProfile()
        {
            var original = Aria2Profile.ConvertToJsonText();
            var expect = JsonSerializer.Deserialize<Profile>(original, Aria2Profile.SerializerOptions).ConvertToJsonText();
            Assert.AreEqual(original, expect);
        }


        [TestMethod]
        public void CheckProfileIntegrity_Default()
        {
            Aria2Profile.SaveProfile();
            Assert.IsTrue(Aria2Profile.CheckProfileIntegrity());
        }

        [TestMethod]
        public void ReadProfileText_Default()
        {
            Aria2Profile.SaveProfile();
            Assert.AreEqual(Aria2Profile.ReadProfileText(), Aria2Profile.ConvertToProfileText());
        }
    }
}
