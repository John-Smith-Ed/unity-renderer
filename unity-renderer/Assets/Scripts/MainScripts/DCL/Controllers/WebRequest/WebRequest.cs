using UnityEngine.Networking;

namespace DCL
{
    /// <summary>
    /// Our custom implementation of the UnityWebRequest.
    /// </summary>
    public class WebRequest : Controllers.WebRequest.IWebRequest
    {
        public UnityWebRequest CreateWebRequest(string url) { return UnityWebRequest.Get(url); }
    }
}