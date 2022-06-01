namespace ABEY {
    using UnityEngine;

    /// <summary>
    /// This is just a quick hack for removing all use of resources so we can 
    /// really start on making this app better
    /// </summary>
    [CreateAssetMenu(fileName = "ShaderRefs", menuName = "ABEY/ShaderRefs", order = 0)]
    public class ShaderRefsScriptableObject : RefScriptableObject<Shader> { }
}