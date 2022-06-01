namespace ABEY {
    using UnityEngine;

    /// <summary>
    /// This is just a quick hack for removing all use of resources so we can 
    /// really start on making this app better
    /// </summary>
    [CreateAssetMenu(fileName = "ShaderVariantRefs", menuName = "ABEY/ShaderVariantRefs", order = 0)]
    public class ShaderVariantRefsScriptableObject : RefScriptableObject<ShaderVariantCollection> { }
}