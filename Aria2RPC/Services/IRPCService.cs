using Aria2NET;

namespace Aria2RPC.Services
{
    public interface IAria2RPCService
    {
        public Aria2NetClient GetClient();

        public Task RunAria2ServiceAsync();

        public Task SoftStopAsync();

        public void SoftStop();

        public void ForceShutdown();
        public void LoadSettings();
        public void RestoreProfileToDefault();
    }
}