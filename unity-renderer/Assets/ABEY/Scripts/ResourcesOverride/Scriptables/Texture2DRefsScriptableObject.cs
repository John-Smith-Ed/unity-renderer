namespace ABEY {
    using UnityEngine;

    /// <summary>
    /// This is just a quick hack for removing all use of resources so we can 
    /// really start on making this app better
    /// </summary>
    [CreateAssetMenu(fileName = "Texture2DRefs", menuName = "ABEY/Texture2DRefs", order = 0)]
    public class Texture2DRefsScriptableObject : RefScriptableObject<Texture2D> { }
}