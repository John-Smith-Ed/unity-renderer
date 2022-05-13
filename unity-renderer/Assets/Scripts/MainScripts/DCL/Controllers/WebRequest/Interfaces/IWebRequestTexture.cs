
namespace DCL
{
   

    public interface IWebRequestTexture : Controllers.WebRequest.IWebRequest
    {
        public bool isReadable { get; set; }
    }
}