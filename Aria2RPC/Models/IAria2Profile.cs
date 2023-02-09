namespace Aria2RPC.Models
{
    public interface IProfile
    {

        public string ConvertToJsonText();
        public string ConvertToProfileText();
        public string ReadProfileText();
        public void SaveToProfile();
        public bool CheckProfileIntegrity();
    }
}
