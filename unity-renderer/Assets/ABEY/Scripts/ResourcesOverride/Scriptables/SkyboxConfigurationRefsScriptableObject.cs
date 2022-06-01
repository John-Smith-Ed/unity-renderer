namespace ABEY {
    using UnityEngine;
    using DCL.Skybox;
    /// <summary>
    /// This is just a quick hack for removing all use of resources so we can 
    /// really start on making this app better
    /// </summary>
    [CreateAssetMenu(fileName = "SkyboxConfigurationRefs", menuName = "ABEY/SkyboxConfigurationRefs", order = 0)]
    public class SkyboxConfigurationRefsScriptableObject : RefScriptableObject<SkyboxConfiguration> { }
}