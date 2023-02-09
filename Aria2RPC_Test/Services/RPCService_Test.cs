using Aria2RPC.Services;

namespace Aria2RPC_Test.Services
{
    [TestClass]
    public class RPCService_Start_and_Stop
    {
        public static Aria2RPCService Service;

        [TestInitialize]
        public void TestInitialize()
        {
            Service = new Aria2RPCService();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (!Service.HasExited)
            {
                Service.Kill();
                Service.Close();
            }
        }

        [TestMethod]
        public async Task RPCService_Runable()
        {
            await Service.RunAria2ServiceAsync();
            var client = Service.GetClient();

            var versionResult = await client.GetVersionAsync();
            Assert.IsFalse(string.IsNullOrEmpty(versionResult.Version)); // 1.36.0
        }

        [TestMethod]
        public async Task SoftStopAsync_Stopable()
        {
            await Service.RunAria2ServiceAsync();

            await Service.SoftStopAsync();
            Assert.IsTrue(Service.HasExited);
        }

        [TestMethod]
        public void SoftStop_Stopable()
        {
            Service.RunAria2ServiceAsync().Wait();

            Service.SoftStop();
            Assert.IsTrue(Service.HasExited);
        }

        [TestMethod]
        public void ForceShutdown_Stopable()
        {
            Service.RunAria2ServiceAsync().Wait();

            Service.ForceShutdown();
            Service.ShutdownCheck();
            Assert.IsTrue(Service.HasExited);
        }

        [TestMethod]
        public void LoadSettings_Default()
        {
            Service.RunAria2ServiceAsync().Wait();
            Service.Aria2Profile.SaveProfile();
            Service.LoadSettings();
            Assert.IsTrue(Service.Aria2Profile.CheckProfileIntegrity());
        }
    }
}