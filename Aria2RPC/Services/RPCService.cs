using Aria2NET;
using Aria2RPC.Models;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Aria2RPC.Services
{
    public class Aria2RPCService : Process, IAria2RPCService
    {
        public Profile Aria2Profile = new Profile();

        public async Task RunAria2ServiceAsync()
        {
            if (File.Exists(Aria2Profile.ConfPath) is false)
            {
                Aria2Profile.SaveProfile();
                LoadSettings();
            }
            else
            {
                LoadSettings();
            }
            StartInfo.FileName = @"Resources\aria2-win-64bit\aria2c.exe";
            StartInfo.Arguments = $"--conf-path={Aria2Profile.ConfPath} --stop-with-process={Aria2Profile.StopWithProcess}";

            EnableRaisingEvents = true;
            StartInfo.CreateNoWindow = false;
            StartInfo.RedirectStandardError = true;
            StartInfo.RedirectStandardInput = true;
            StartInfo.RedirectStandardOutput = true;
            Start();


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
            string protocal = Aria2Profile.RpcSecure == true ? "https" : "http";
            int port = Aria2Profile.RpcListenPort ?? 6800;
            if (string.IsNullOrEmpty(Aria2Profile.RpcSecret))
            {
                return new Aria2NetClient($"{protocal}://127.0.0.1:{port}/jsonrpc");
            }
            return new Aria2NetClient($"{protocal}://127.0.0.1:{port}/jsonrpc", Aria2Profile.RpcSecret);
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


        public void LoadSettings()
        {
            Aria2Profile = Aria2Profile.ReadSettings();
        }

        public void RestoreProfileToDefault()
        {
            var profile = new Profile();
            Aria2Profile = profile;
            profile.SaveProfile();
        }
    }
}