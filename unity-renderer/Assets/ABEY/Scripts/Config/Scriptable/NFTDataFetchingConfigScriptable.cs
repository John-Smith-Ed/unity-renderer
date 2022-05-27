namespace ABEY {
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "NFTDataFetchingConfig", menuName = "ABEY/NFTDataFetchingConfigScriptable", order = 0)]
    public class NFTDataFetchingConfigScriptable : ScriptableObject {

        [SerializeField] Vector2 normalizedDimensions = new UnityEngine.Vector2(512f, 512f); // The image dimensions that correspond to Vector3.One scale
        [SerializeField] string  darApiUrl            = "https://schema.decentraland.org/dar";
        
        public Vector2 NormalizedDimensions => normalizedDimensions;
        public string  DarApiUrl            => darApiUrl;
    }   
}
