namespace ABEY {

    using UnityEngine;

    [CreateAssetMenu(fileName = "ConfigScriptable", menuName = "ABEY/ConfigScriptable", order = 0)]
    public class ConfigScriptable : ScriptableObject {
        
        [SerializeField] public string mapApiBaseUrl = "http://debugmode.online:5000/v1/map.png";


        public string MapApiBaseUrl => mapApiBaseUrl;
    }

}

