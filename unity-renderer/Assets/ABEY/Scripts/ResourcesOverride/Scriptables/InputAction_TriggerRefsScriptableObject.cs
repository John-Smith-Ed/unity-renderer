namespace ABEY {
    using UnityEngine;

    /// <summary>
    /// This is just a quick hack for removing all use of resources so we can 
    /// really start on making this app better
    /// </summary>
    [CreateAssetMenu(fileName = "InputActionTriggerRefs", menuName = "ABEY/InputActionTriggerRefs", order = 0)]
    public class InputAction_TriggerRefsScriptableObject : RefScriptableObject<InputAction_Trigger> { }
}