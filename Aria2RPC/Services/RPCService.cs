using Aria2NET;
using Aria2RPC.Models;
using System.Diagnostics;
using System.Text.Json;

namespace Aria2RPC.Services
{
    public class Aria2RPCService : Process, IAria2RPCService
    {
        private Profile _profile = new Profile();

        public async Task RunAria2ServiceAsync()
        {
            StartInfo.FileName = @"Resources\aria2-win-64bit\aria2c.exe";
            StartInfo.Arguments = $"--conf-path={_profile.ConfPath}";

            EnableRaisingEvents = true;
            StartInfo.CreateNoWindow = false;
            StartInfo.RedirectStandardError = true;
            StartInfo.RedirectStandardInput = true;
            StartInfo.RedirectStandardOutput = true;
            Start();

            if (_profile.CheckProfileIntegrity() is false)
            {
                _profile.SaveToProfile();
            }

            await StartupCheck();
        }

        private async Task StartupCheck()
        {
            var client = GetClient();
            for (int i = 0; i < 30; i++)
            {
                Thread.Sleep(1000);

                try
                {
                    var versionString = await client.GetVersionAsync();
                    if (versionString == null)
                    {
                        continue;
                    }
                    else
                    {
                        return;
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Startup Error");
                }
            }
        }

        public void ShutdownCheck()
        {
            for (int i = 0; i < 30; i++)
            {
                Thread.Sleep(1000);
                if (HasExited)
                {
                    return;
                }
            }
        }

        public Aria2NetClient GetClient()
        {
            string protocal = _profile.RpcSecure == true ? "https" : "http";
            int port = _profile.RpcListenPort ?? 6800;
            if (string.IsNullOrEmpty(_profile.RpcSecret))
            {
                return new Aria2NetClient($"{protocal}://127.0.0.1:{port}/jsonrpc");
            }
            return new Aria2NetClient($"{protocal}://127.0.0.1:{port}/jsonrpc", _profile.RpcSecret);
        }

        public async Task SoftStopAsync()
        {
            var client = GetClient();
            await client.ShutdownAsync();

            ShutdownCheck();
        }

        public void SoftStop()
        {
            var client = GetClient();
            client.ShutdownAsync().Wait();
            ShutdownCheck();
        }

        public void ForceShutdown()
        {
            var client = GetClient();
            client.ForceShutdownAsync().Wait();
            ShutdownCheck();
        }

        public void LoadAndSaveProfileFromJson(string jsonText)
        {
            _profile = JsonSerializer.Deserialize<Profile>(jsonText);
            _profile.SaveToProfile();
        }
    }
}