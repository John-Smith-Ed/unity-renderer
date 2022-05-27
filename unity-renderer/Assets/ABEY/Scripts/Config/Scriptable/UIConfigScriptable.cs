namespace ABEY {
    using UnityEngine;

    [CreateAssetMenu(fileName = "UIConfig", menuName = "ABEY/UIConfigScriptable", order = 0)]
    public class UIConfigScriptable : ScriptableObject {

        [SerializeField] float reservedCanvasTopPercentage = 10f;

        public float ReservedCanvasTopPercentage => reservedCanvasTopPercentage;
    }
}