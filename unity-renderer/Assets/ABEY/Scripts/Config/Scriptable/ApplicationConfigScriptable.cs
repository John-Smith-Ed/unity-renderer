namespace ABEY {
    using UnityEngine;

    [CreateAssetMenu(fileName = "ApplicationConfigScriptable", menuName = "ABEY/ApplicationConfigScriptable", order = 0)]
    public class ApplicationConfigScriptable : ScriptableObject {
        
        [SerializeField] public string version = "1.0";

        public string Version => version; 
    }
}