using System.Diagnostics;

namespace Aria2WebService.Models
{
    public class Aria2Profile
    {
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
            {"rpc-listen-port", "6802" },           // Specify a port number for JSON-RPC/XML-RPC server to listen to. Possible Values: 1024 -65535 Default: 6800
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
            {"disk-cache", "" },
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
            {"stop-with-process", $"{Environment.ProcessId}" },
            {"truncate-console-readout", "" }
        };


        /**
         * Basic Options - https://aria2.github.io/manual/en/html/aria2c.html#basic-options
         */
        public string? Dir { get => _profileDict.TryGetValue("dir", out string value) ? value : null; set => _profileDict["dir"] = value; }
        public string? InputFile
        {
            get
            {
                if (_profileDict.TryGetValue("input-file", out string value) && File.Exists(value))
                {
                    return value;
                }
                return null;
            }
            set => _profileDict["input-file"] = value;
        }
        public string? Log { get => _profileDict.TryGetValue("log", out string value) ? value : null; set => _profileDict["log"] = value; }
        public byte? MaxConcurrentDownloads
        {
            get
            {
                if (_profileDict.TryGetValue("max-concurrent-downloads", out string value) && byte.TryParse(value, out byte result))
                {
                    return result;
                }
                throw new InvalidDataException($"option \"max-concurrent-downloads\" can't exceeding max value 255, actual value is \"{value}\"");
            }
            set => _profileDict["max-concurrent-downloads"] = value.ToString();
        }
        public bool? CheckIntegrity { get; set; }
        /**
         * RPC Options - https://aria2.github.io/manual/en/html/aria2c.html#rpc-options
         */
        public bool? EnableRpc { get; set; }
        public bool? Pause { get; set; }
        public bool? PauseMetadata { get; set; }
        public bool? RpcAllowOriginAll { get; set; }
        public string? RpcCertificate { get; set; }
        public bool? RpcListenAll { get; set; }
        public int? RpcListenPort { get; set; } // Default: 6800
        public string? RpcMaxRequestSize { get; set; } // Default: 2M
        public string? RpcPrivateKey { get; set; }
        public bool? RpcSaveUploadMetadata { get; set; }
        public string? RpcSecret { get; set; }
        public bool? RpcSecure { get; set; }
        /**
         * Advanced Options - https://aria2.github.io/manual/en/html/aria2c.html#advanced-options
         */
        public bool? AllowOverwrite { get; set; }
        public bool? AllowPieceLengthChange { get; set; }
        public bool? AlwaysResume { get; set; }
        public bool? AsyncDns { get; set; }
        public string? AsyncDnsServer { get; set; } //Comma separated list of DNS server address used in asynchronous DNS resolver. for example: "0.0.0.0,AD80:0000:0000:0000:ABAA:0000:00C2:0002,1.1.1.1"
        public bool? AutoFileRenaming { get; set; }
        public int? AutoSaveInterval { get; set; } // Second
        public bool? ConditionalGet { get; set; }
        public string? ConfPath { get; set; }
        public string? ConsoleLogLevel { get; }
        public bool? ContentDispositionDefaultUtf8 { get; set; }
        public bool? Deamon { get; set; }
        public bool? DeferredInput { get; set; }
        public bool? DisableIpv6 { get; set; }
        public string? DiskCache { get; set; } // SIZE can include K or M (1K = 1024, 1M = 1024K). Default: 16M. This feature caches the downloaded data in memory, which grows to at most SIZE bytes.
        public string? DownloadResult { get; set; }
        public string? Dscp { get; set; }
        public bool? EnableColor { get; set; }
        public bool? EnableMmap { get; set; }
        public string? EventPoll { get; set; }
        public string? FileAllocation { get; set; }
        public bool? ForceSave { get; set; }
        public bool? SaveNotFound { get; set; }
        public string? Gid { get; set; }
        public bool? HashCheckOnly { get; set; }
        public bool? HumanReadable { get; set; }
        public string? Interface { get; set; }
        public bool? KeepUnfinishedDownloadResult { get; set; }
        public int? MaxDownloadResult { get; set; }
        public ulong? MaxMmapLimit { get; set; }
        public byte? MaxResumeFailureTries { get; set; } // max - 255 times
        public string? MinTlsVersion { get; set; } // TLSv1.1, TLSv1.2, TLSv1.3. Default: TLSv1.2
        public string? MultipleInterface { get; set; }
        public string? LogLevel { get; set; }
        public string? OnBtDownloadComplete { get; set; }
        public string? OnDownloadComplete { get; set; }
        public string? OnDownloadError { get; set; }
        public string? OnDownloadPause { get; set; }
        public string? OnDownloadStart { get; set; }
        public string? OnDownloadStop { get; set; }
        public bool? OptimizeConcurrentDownloads { get; set; }
        public string? PieceLength { get; set; }
        public bool? ShowConsoleReadout { get; set; }
        public bool? Stderr { get; set; }
        public int? SummaryInterval { get; set; }
        public bool? ForceSequential { get; set; }
        public string? MaxOverallDownloadLimit { get; set; }
        public string? MaxDownloadLimit { get; set; }
        public bool? NoConf { get; set; }
        public string? NoFileAllocationLimit { get; set; }
        public bool? ParameterizedUri { get; set; }
        public bool? Quiet { get; set; }
        public bool? RealtimeChunkChecksum { get; set;}
        public bool? RemoveControlFile { get; set; }
        public bool? SaveSession { get; set;}
        public int? SaveSessionInterval { get; set; }
        public int? SocketRecvBufferSize { get; set; }
        public int? Stop { get; set; }
        public string? StopWithProcess { get; set; } // useful
        public bool? TruncateConsoleReadout { get; set; }
    }
}