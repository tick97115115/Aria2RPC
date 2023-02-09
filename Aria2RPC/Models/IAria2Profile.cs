namespace Aria2RPC.Models
{
    public interface IProfile
    {
        public Profile ReadSettings();
        public string ConvertToJsonText();
        public string ConvertToProfileText();
        public string ReadProfileText();
        public void SaveProfile();
        public bool CheckProfileIntegrity();
        public string ConvertToArgs();
    }
}
