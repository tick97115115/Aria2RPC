using System.Diagnostics;
using System.Text;
using System.Timers;
using Aria2NET;
using RSG;

namespace Aria2RPC.Services
{
    public class Aria2RPCService: Process, IAria2RPCService
    {
        public Aria2RPCService(string? profilePath)
        {
            if (string.IsNullOrEmpty(profilePath))
            {
                profilePath = Path.Combine("Resources", "aria2-win-64bit", "aria2.conf");
            }

            var text = System.IO.File.ReadAllLines(profilePath);

            for (int i = 0; i < text.Length; i++)
            {
                string item = text[i];

                var couple = item.Split('=');
                string key = couple[0];
                string value;
                if (_profileDict.ContainsKey(key) && couple.Length == 2)
                {
                    value = couple[1];
                    _profileDict[key] = value;
                }
                else
                {
                    throw new Exception($"Config info error, config paramter \"--{key}\" isn't valid.");
                }
            }
        }

        private Dictionary<string, string> _profileDict = new Dictionary<string, string>()
        {
            /**
             * Basic Options - https://aria2.github.io/manual/en/html/aria2c.html#basic-options
             */
            {"dir", $"{Path.Combine("Resources", "download")}" },       // download directory 
            {"input-file", 
                File.Exists($"{Path.Combine("Resources", "aria2-win-64bit", "aria2.session")}") ?
                $"{Path.Combine("Resources", "aria2-win-64bit", "aria2.session")}" :
                string.Empty },
            {"log", $"{Path.Combine("Resources", "log")}" },       // log file location
            {"max-concurrent-downloads", "" },  // max concurrently download number. (suggest between 1 to 5)
            {"check-integrity", "" },       // check BitTorrent & Metalink download file integrity. ("true" or "false")

            /**
             * RPC Options - https://aria2.github.io/manual/en/html/aria2c.html#rpc-options
             */
            {"enable-rpc", "true" },    // must be chosen as enabled
            {"pause", "" },             // Pause download after added.
            {"pause-metadata", "" },    // Pause downloads created as a result of metadata download.
            {"rpc-allow-origin-all", "true" },      // Add Access-Control-Allow-Origin header field with value * to the RPC response.
            {"rpc-certificate", "" },           // Use the certificate in FILE for RPC server.
            {"rpc-listen-all", "" },            // Listen incoming JSON-RPC/XML-RPC requests on all network interfaces including 0.0.0.0.
            {"rpc-listen-port", "6800" },           // Specify a port number for JSON-RPC/XML-RPC server to listen to. Possible Values: 1024 -65535 Default: 6800
            {"rpc-max-request-size", "" },      // Set max size of JSON-RPC/XML-RPC request. If aria2 detects the request is more than SIZE bytes, it drops connection. Default: 2M
            {"rpc-private-key", "" },           
            {"rpc-save-upload-metadata", "" },
            {"rpc-secret", "" },                // Set RPC secret authorization token.
            {"rpc-secure", "" },                // https://aria2.github.io/manual/en/html/aria2c.html#cmdoption-rpc-secure

            /**
             * Advanced Options - https://aria2.github.io/manual/en/html/aria2c.html#advanced-options
             */
            {"allow-overwrite", "" },
            {"allow-piece-length-change", "" },
            {"always-resume", "" },
            {"async-dns", "" },
            {"async-dns-server", "" },
            {"auto-file-renaming", "" },
            {"auto-save-interval", "" },
            {"conditional-get", "" },
            {"conf-path", "" },             // https://aria2.github.io/manual/en/html/aria2c.html#cmdoption-conf-path
            {"console-log-level", "" },
            {"content-disposition-default-utf8", "" },
            {"daemon", "" },                // https://aria2.github.io/manual/en/html/aria2c.html#cmdoption-D
            {"deferred-input", "" },
            {"disable-ipv6", "" },
            {"disk-cache", "0" },           // for better disk life saving, recommended set value to 0,
            {"download-result", "" },
            {"dscp", "" },
            {"rlimit-nofile", "" },
            {"enable-color", "" },
            {"enable-mmap", "" },           // https://aria2.github.io/manual/en/html/aria2c.html#cmdoption-enable-mmap
            {"event-poll", "" },
            {"file-allocation", "" },       // https://aria2.github.io/manual/en/html/aria2c.html#cmdoption-file-allocation
            {"force-save", "" },
            {"save-not-found", "" },
            {"gid", "" },
            {"hash-check-only", "" },
            {"human-readable", "" },
            {"interface", "" },
            {"keep-unfinished-download-result", "" },
            {"max-download-result", "" },   // https://aria2.github.io/manual/en/html/aria2c.html#cmdoption-max-download-result
            {"max-mmap-limit","" },         // https://aria2.github.io/manual/en/html/aria2c.html#cmdoption-max-mmap-limit
            {"max-resume-failure-tries", "" },      // https://aria2.github.io/manual/en/html/aria2c.html#cmdoption-max-resume-failure-tries
            {"min-tls-version", "" },
            {"multiple-interface", "" },
            {"log-level", "" },
            {"on-bt-download-complete", "" },       // https://aria2.github.io/manual/en/html/aria2c.html#cmdoption-on-bt-download-complete
            {"on-download-complete", "" },          // https://aria2.github.io/manual/en/html/aria2c.html#cmdoption-on-download-complete
            {"on-download-error", "" },             // https://aria2.github.io/manual/en/html/aria2c.html#cmdoption-on-download-error
            {"on-download-pause", "" },             // https://aria2.github.io/manual/en/html/aria2c.html#cmdoption-on-download-pause
            {"on-download-start", "" },             // https://aria2.github.io/manual/en/html/aria2c.html#cmdoption-on-download-start
            {"on-download-stop", "" },              // https://aria2.github.io/manual/en/html/aria2c.html#cmdoption-on-download-stop
            {"optimize-concurrent-downloads", "" },     // https://aria2.github.io/manual/en/html/aria2c.html#cmdoption-optimize-concurrent-downloads
            {"piece-length", "" },
            {"show-console-readout", "" },
            {"stderr", "" },
            {"summary-interval", "" },
            {"force-sequential", "" },
            {"max-overall-download-limit", "" },    // https://aria2.github.io/manual/en/html/aria2c.html#cmdoption-max-overall-download-limit
            {"max-download-limit", "" },            // https://aria2.github.io/manual/en/html/aria2c.html#cmdoption-max-download-limit
            {"no-conf", "" },               // Disable loading aria2.conf file.
            {"no-file-allocation-limit", "" },      
            {"parameterized-uri", "" },     // https://aria2.github.io/manual/en/html/aria2c.html#cmdoption-P
            {"quiet", "" },
            {"realtime-chunk-checksum", "" },
            {"remove-control-file", "" },
            {"save-session", $"{Path.Combine("Resources", "aria2-win-64bit", "aria2.session")}" },          // https://aria2.github.io/manual/en/html/aria2c.html#cmdoption-save-session
            {"save-session-interval", "" },         // https://aria2.github.io/manual/en/html/aria2c.html#cmdoption-save-session-interval
            {"socket-recv-buffer-size", "" },
            {"stop", "" },
            {"stop-with-process", "" },
            {"truncate-console-readout", "" },
            {"version", "" },
        };

        public async Task RunAria2ServiceAsync()
        {
            StartInfo.FileName = @"Resources\aria2-win-64bit\aria2c.exe";

            var arg = new StringBuilder();
            foreach (var item in _profileDict)
            {
                if (string.IsNullOrEmpty(item.Value)) continue;
                arg.Append($"--{item.Key}={item.Value} ");
            }
            StartInfo.Arguments = arg.ToString();


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
            for (int i = 0; i < 20; i++)
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

        private void ShutdownCheck()
        {
            for (int i = 0; i < 20; i++)
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
            string protocal = _profileDict["rpc-secure"] == "true" ? "https" : "http";
            string port = _profileDict["rpc-listen-port"];
            if (string.IsNullOrEmpty(_profileDict["rpc-secret"]))
            {
                return new Aria2NetClient($"{protocal}://127.0.0.1:{port}/jsonrpc");
            }
            return new Aria2NetClient($"{protocal}://127.0.0.1:{port}/jsonrpc", _profileDict["rpc-secret"]);
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
    }
}
