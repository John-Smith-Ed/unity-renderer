namespace ABEY {
    using UnityEngine;

    /// <summary>
    /// This is just a quick hack for removing all use of resources so we can 
    /// really start on making this app better
    /// </summary>
    [CreateAssetMenu(fileName = "TextAssetRefs", menuName = "ABEY/TextAssetRefs", order = 0)]
    public class TextAssetRefsScriptableObject : RefScriptableObject<TextAsset> { }
}