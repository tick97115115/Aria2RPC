using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Aria2RPC.Models
{
    public class Profile : IProfile
    {
        [JsonIgnore] private string _lastModification { get; set; } = Path.Combine("Resources", "aria2-win-64bit", "aria2.bk.json");
        public JsonSerializerOptions SerializerOptions = new JsonSerializerOptions()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };
        public string ConvertToJsonText()
        {
            return JsonSerializer.Serialize(this, SerializerOptions);
        }
        public string ConvertToProfileText()
        {
            var obj = JsonNode.Parse(ConvertToJsonText());
            var stringBuilder = new StringBuilder();
            foreach (var item in obj.AsObject())
            {
                stringBuilder.AppendLine($"{item.Key}={item.Value.ToString()}");
            }
            return stringBuilder.ToString();
        }
        public string ReadProfileText()
        {
            return File.ReadAllText(ConfPath);
        }
        public void SaveToProfile()
        {
            File.WriteAllText(ConfPath, ConvertToProfileText());
            File.WriteAllText(_lastModification, ConvertToJsonText());
        }
        public bool CheckProfileIntegrity()
        {
            bool result;
            try
            {
                result = File.ReadAllText(_lastModification) == ConvertToJsonText();
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /**
         * Basic Options - https://aria2.github.io/manual/en/html/aria2c.html#basic-options
         */
        [JsonPropertyName("dir")] public string? Dir { get; set; } = Path.Combine("Resources", "download");
        [JsonPropertyName("input-file")] public string? InputFile { get; set; } =
            File.Exists(Path.Combine("Resources", "aria2-win-64bit", "aria2.session")) ?
            Path.Combine("Resources", "aria2-win-64bit", "aria2.session") : null;
        [JsonPropertyName("log")] public string? Log { get; set; } = Path.Combine("Resources", "log");
        [JsonPropertyName("max-concurrent-downloads")] public byte? MaxConcurrentDownloads { get; set; }
        [JsonPropertyName("check-integrity")] public bool? CheckIntegrity { get; set; } = true;
        /**
         * RPC Options - https://aria2.github.io/manual/en/html/aria2c.html#rpc-options
         */
        [JsonPropertyName("enable-rpc")] public bool? EnableRpc { get; set; } = true;
        [JsonPropertyName("pause")] public bool? Pause { get; set; }
        [JsonPropertyName("pause-metadata")] public bool? PauseMetadata { get; set; }
        [JsonPropertyName("rpc-allow-origin-all")] public bool? RpcAllowOriginAll { get; set; } = true;
        [JsonPropertyName("rpc-certificate")] public string? RpcCertificate { get; set; }
        [JsonPropertyName("rpc-listen-all")] public bool? RpcListenAll { get; set; }
        [JsonPropertyName("rpc-listen-port")] public int? RpcListenPort { get; set; } // Default: 6800
        [JsonPropertyName("rpc-max-request-size")] public string? RpcMaxRequestSize { get; set; } // Default: 2M
        [JsonPropertyName("rpc-private-key")] public string? RpcPrivateKey { get; set; }
        [JsonPropertyName("rpc-save-upload-metadata")] public bool? RpcSaveUploadMetadata { get; set; }
        [JsonPropertyName("rpc-secret")] public string? RpcSecret { get; set; }
        [JsonPropertyName("rpc-secure")] public bool? RpcSecure { get; set; }
        /**
         * Advanced Options - https://aria2.github.io/manual/en/html/aria2c.html#advanced-options
         */

        [JsonPropertyName("allow-overwrite")] public bool? AllowOverwrite { get; set; }
        [JsonPropertyName("allow-piece-length-change")] public bool? AllowPieceLengthChange { get; set; }
        [JsonPropertyName("always-resume")] public bool? AlwaysResume { get; set; }
        [JsonPropertyName("async-dns")] public bool? AsyncDns { get; set; }
        [JsonPropertyName("async-dns-server")] public string? AsyncDnsServer { get; set; } //Comma separated list of DNS server address used in asynchronous DNS resolver. for example: "0.0.0.0,AD80:0000:0000:0000:ABAA:0000:00C2:0002,1.1.1.1"
        [JsonPropertyName("auto-file-renaming")] public bool? AutoFileRenaming { get; set; }
        [JsonPropertyName("auto-save-interval")] public int? AutoSaveInterval { get; set; } // Second
        [JsonPropertyName("conditional-get")] public bool? ConditionalGet { get; set; }
        [JsonPropertyName("conf-path")]  public string ConfPath { get; set; } = Path.Combine("Resources", "aria2-win-64bit", "aria2.conf");
        [JsonPropertyName("console-log-level")] public string? ConsoleLogLevel { get; }
        [JsonPropertyName("content-disposition-default-utf8")] public bool? ContentDispositionDefaultUtf8 { get; set; }
        [JsonPropertyName("daemon")] public bool? Deamon { get; set; }
        [JsonPropertyName("deferred-input")] public bool? DeferredInput { get; set; }
        [JsonPropertyName("disable-ipv6")] public bool? DisableIpv6 { get; set; }
        [JsonPropertyName("disk-cache")] public string? DiskCache { get; set; } // SIZE can include K or M (1K = 1024, 1M = 1024K). Default: 16M. This feature caches the downloaded data in memory, which grows to at most SIZE bytes.
        [JsonPropertyName("download-result")] public string? DownloadResult { get; set; }
        [JsonPropertyName("dscp")] public string? Dscp { get; set; }
        [JsonPropertyName("enable-color")] public bool? EnableColor { get; set; }
        [JsonPropertyName("enable-mmap")] public bool? EnableMmap { get; set; }
        [JsonPropertyName("event-poll")] public string? EventPoll { get; set; }
        [JsonPropertyName("file-allocation")] public string? FileAllocation { get; set; }
        [JsonPropertyName("force-save")] public bool? ForceSave { get; set; }
        [JsonPropertyName("save-not-found")] public bool? SaveNotFound { get; set; }
        [JsonPropertyName("gid")] public string? Gid { get; set; }
        [JsonPropertyName("hash-check-only")] public bool? HashCheckOnly { get; set; }
        [JsonPropertyName("human-readable")] public bool? HumanReadable { get; set; }
        [JsonPropertyName("interface")] public string? Interface { get; set; }
        [JsonPropertyName("keep-unfinished-download-result")] public bool? KeepUnfinishedDownloadResult { get; set; }
        [JsonPropertyName("max-download-result")] public int? MaxDownloadResult { get; set; }
        [JsonPropertyName("max-mmap-limit")] public ulong? MaxMmapLimit { get; set; }
        [JsonPropertyName("max-resume-failure-tries")] public byte? MaxResumeFailureTries { get; set; } // max - 255 times
        [JsonPropertyName("min-tls-version")] public string? MinTlsVersion { get; set; } // TLSv1.1, TLSv1.2, TLSv1.3. Default: TLSv1.2
        [JsonPropertyName("multiple-interface")] public string? MultipleInterface { get; set; }
        [JsonPropertyName("log-level")] public string? LogLevel { get; set; }
        [JsonPropertyName("on-bt-download-complete")] public string? OnBtDownloadComplete { get; set; }
        [JsonPropertyName("on-download-complete")] public string? OnDownloadComplete { get; set; }
        [JsonPropertyName("on-download-error")] public string? OnDownloadError { get; set; }
        [JsonPropertyName("on-download-pause")] public string? OnDownloadPause { get; set; }
        [JsonPropertyName("on-download-start")] public string? OnDownloadStart { get; set; }
        [JsonPropertyName("on-download-stop")] public string? OnDownloadStop { get; set; }
        [JsonPropertyName("optimize-concurrent-downloads")] public bool? OptimizeConcurrentDownloads { get; set; }
        [JsonPropertyName("piece-length")] public string? PieceLength { get; set; }
        [JsonPropertyName("show-console-readout")] public bool? ShowConsoleReadout { get; set; }
        [JsonPropertyName("stderr")] public bool? Stderr { get; set; }
        [JsonPropertyName("summary-interval")] public int? SummaryInterval { get; set; }
        [JsonPropertyName("force-sequential")] public bool? ForceSequential { get; set; }
        [JsonPropertyName("max-overall-download-limit")] public string? MaxOverallDownloadLimit { get; set; }
        [JsonPropertyName("max-download-limit")] public string? MaxDownloadLimit { get; set; }
        [JsonPropertyName("no-conf")] public bool? NoConf { get; set; }
        [JsonPropertyName("no-file-allocation-limit")] public string? NoFileAllocationLimit { get; set; }
        [JsonPropertyName("parameterized-uri")] public bool? ParameterizedUri { get; set; }
        [JsonPropertyName("quiet")] public bool? Quiet { get; set; }
        [JsonPropertyName("realtime-chunk-checksum")] public bool? RealtimeChunkChecksum { get; set; }
        [JsonPropertyName("remove-control-file")] public bool? RemoveControlFile { get; set; }
        [JsonPropertyName("save-session")] public string? SaveSession { get; set; } = Path.Combine("Resources", "aria2-win-64bit", "aria2.session");
        [JsonPropertyName("save-session-interval")] public int? SaveSessionInterval { get; set; }
        [JsonPropertyName("socket-recv-buffer-size")] public int? SocketRecvBufferSize { get; set; }
        [JsonPropertyName("stop")] public int? Stop { get; set; }
        [JsonPropertyName("stop-with-process")] public string StopWithProcess { get; set; } = Environment.ProcessId.ToString(); // useful
        [JsonPropertyName("truncate-console-readout")] public bool? TruncateConsoleReadout { get; set; }
    }
}