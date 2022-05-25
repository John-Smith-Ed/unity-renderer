namespace ABEY {
    using UnityEngine;

    [CreateAssetMenu(fileName = "InputConfigScriptable", menuName = "ABEY/InputConfigScriptable", order = 0)]
    public class InputConfigScriptable : ScriptableObject {
        
        [SerializeField] KeyCode primaryButtonKeyCode       = KeyCode.E;
        [SerializeField] KeyCode secondaryButtonKeyCode     = KeyCode.F;
        [SerializeField] KeyCode forwardButtonKeyCode       = KeyCode.W;
        [SerializeField] KeyCode backwardButtonKeyCode      = KeyCode.S;
        [SerializeField] KeyCode leftButtonKeyCode          = KeyCode.A;
        [SerializeField] KeyCode rightButtonKeyCode         = KeyCode.D;
        [SerializeField] KeyCode forwardButtonKeyCodeAlt    = KeyCode.UpArrow;
        [SerializeField] KeyCode backwardButtonKeyCodeAlt   = KeyCode.DownArrow;
        [SerializeField] KeyCode leftButtonKeyCodeAlt       = KeyCode.LeftArrow;
        [SerializeField] KeyCode rightButtonKeyCodeAlt      = KeyCode.RightArrow;
        [SerializeField] KeyCode jumpButtonKeyCode          = KeyCode.Space;
        [SerializeField] KeyCode walkButtonKeyCode          = KeyCode.LeftShift;
        [SerializeField] KeyCode plusKeyCode                = KeyCode.KeypadPlus;
        [SerializeField] KeyCode minusKeyCode               = KeyCode.KeypadMinus;
        [SerializeField] KeyCode actionButton3Keycode       = KeyCode.Alpha1;
        [SerializeField] KeyCode actionButton4Keycode       = KeyCode.Alpha2;
        [SerializeField] KeyCode actionButton5Keycode       = KeyCode.Alpha3;
        [SerializeField] KeyCode actionButton6Keycode       = KeyCode.Alpha4;

        public KeyCode PrimaryButtonKeyCode       => primaryButtonKeyCode;
        public KeyCode SecondaryButtonKeyCode     => secondaryButtonKeyCode;
        public KeyCode ForwardButtonKeyCode       => forwardButtonKeyCode;
        public KeyCode BackwardButtonKeyCode      => backwardButtonKeyCode;
        public KeyCode LeftButtonKeyCode          => leftButtonKeyCode;
        public KeyCode RightButtonKeyCode         => rightButtonKeyCode;
        public KeyCode ForwardButtonKeyCodeAlt    => forwardButtonKeyCodeAlt;
        public KeyCode BackwardButtonKeyCodeAlt   => backwardButtonKeyCodeAlt;
        public KeyCode LeftButtonKeyCodeAlt       => leftButtonKeyCodeAlt;
        public KeyCode RightButtonKeyCodeAlt      => rightButtonKeyCodeAlt;
        public KeyCode JumpButtonKeyCode          => jumpButtonKeyCode;
        public KeyCode WalkButtonKeyCode          => walkButtonKeyCode;
        public KeyCode PlusKeyCode                => plusKeyCode;
        public KeyCode MinusKeyCode               => minusKeyCode;
        public KeyCode ActionButton3Keycode       => actionButton3Keycode;
        public KeyCode ActionButton4Keycode       => actionButton4Keycode;
        public KeyCode ActionButton5Keycode       => actionButton5Keycode;
        public KeyCode ActionButton6Keycode       => actionButton6Keycode;
    }
}