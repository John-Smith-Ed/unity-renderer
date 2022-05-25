namespace ABEY {
    //Replace CommonScriptableObjects
    // the "Resouces API Should not be user - They abused it with improper use of scriptables"

    using UnityEngine;

    [CreateAssetMenu(fileName = "AudioEventsScriptable", menuName = "ABEY/AudioEventsScriptable", order = 0)]
    public class AudioEventsScriptable : ScriptableObject {
        [Header("Common UI")]
        [SerializeField] AudioEvent cameraFadeInEvent;
        [SerializeField] AudioEvent cameraFadeOutEvent;
        [SerializeField] AudioEvent buttonHoverEvent;
        [SerializeField] AudioEvent buttonClickEvent;
        [SerializeField] AudioEvent buttonReleaseEvent;
        [SerializeField] AudioEvent cancelEvent;
        [SerializeField] AudioEvent confirmEvent;
        [SerializeField] AudioEvent dialogOpenEvent;
        [SerializeField] AudioEvent dialogCloseEvent;
        [SerializeField] AudioEvent enableEvent;
        [SerializeField] AudioEvent errorEvent;
        [SerializeField] AudioEvent disableEvent;
        [SerializeField] AudioEvent fadeInEvent;
        [SerializeField] AudioEvent fadeOutEvent;
        [SerializeField] AudioEvent chatReceiveGlobalEvent;
        [SerializeField] AudioEvent chatReceivePrivateEvent;
        [SerializeField] AudioEvent chatSendEvent;
        [SerializeField] AudioEvent notificationEvent;
        [SerializeField] AudioEvent sliderValueChangeEvent;
        [SerializeField] AudioEvent inputFieldFocusEvent;
        [SerializeField] AudioEvent inputFieldUnfocusEvent;
        [SerializeField] AudioEvent UIHideEvent;
        [SerializeField] AudioEvent UIShowEvent;
        [SerializeField] AudioEvent tooltipPopupEvent;        
        [SerializeField] AudioEvent_WithPitchIncrement listItemAppearEvent;

        [Header("Builder")]
        [SerializeField] AudioEvent builderEnterEvent;
        [SerializeField] AudioEvent builderReadyEvent;


        public AudioEvent cameraFadeIn          => cameraFadeInEvent;       //CommonScriptableObjects.GetOrLoad(ref cameraFadeInEvent, "ScriptableObjects/AudioEvents/HUDCommon/CameraFadeIn");
        public AudioEvent cameraFadeOut         => cameraFadeOutEvent;      //CommonScriptableObjects.GetOrLoad(ref cameraFadeOutEvent, "ScriptableObjects/AudioEvents/HUDCommon/CameraFadeOut");
        public AudioEvent buttonHover           => buttonHoverEvent;        //CommonScriptableObjects.GetOrLoad(ref buttonHoverEvent, "ScriptableObjects/AudioEvents/HUDCommon/ButtonHover");    
        public AudioEvent buttonClick           => buttonClickEvent;        //CommonScriptableObjects.GetOrLoad(ref buttonClickEvent, "ScriptableObjects/AudioEvents/HUDCommon/ButtonClick");    
        public AudioEvent buttonRelease         => buttonReleaseEvent;      //CommonScriptableObjects.GetOrLoad(ref buttonReleaseEvent, "ScriptableObjects/AudioEvents/HUDCommon/ButtonRelease");    
        public AudioEvent cancel                => cancelEvent;             //CommonScriptableObjects.GetOrLoad(ref cancelEvent, "ScriptableObjects/AudioEvents/HUDCommon/Cancel");    
        public AudioEvent confirm               => confirmEvent;            //CommonScriptableObjects.GetOrLoad(ref confirmEvent, "ScriptableObjects/AudioEvents/HUDCommon/Confirm");    
        public AudioEvent dialogOpen            => dialogOpenEvent;         //CommonScriptableObjects.GetOrLoad(ref dialogOpenEvent, "ScriptableObjects/AudioEvents/HUDCommon/DialogOpen");    
        public AudioEvent dialogClose           => dialogCloseEvent;        //CommonScriptableObjects.GetOrLoad(ref dialogCloseEvent, "ScriptableObjects/AudioEvents/HUDCommon/DialogClose");    
        public AudioEvent enable                => enableEvent;             //CommonScriptableObjects.GetOrLoad(ref enableEvent, "ScriptableObjects/AudioEvents/HUDCommon/Enable");    
        public AudioEvent error                 => errorEvent;              //CommonScriptableObjects.GetOrLoad(ref errorEvent, "ScriptableObjects/AudioEvents/HUDCommon/Error");    
        public AudioEvent disable               => disableEvent;            //CommonScriptableObjects.GetOrLoad(ref disableEvent, "ScriptableObjects/AudioEvents/HUDCommon/Disable");    
        public AudioEvent fadeIn                => fadeInEvent;             //CommonScriptableObjects.GetOrLoad(ref fadeInEvent, "ScriptableObjects/AudioEvents/HUDCommon/FadeIn");    
        public AudioEvent fadeOut               => fadeOutEvent;            //CommonScriptableObjects.GetOrLoad(ref fadeOutEvent, "ScriptableObjects/AudioEvents/HUDCommon/FadeOut");    
        public AudioEvent chatReceiveGlobal     => chatReceiveGlobalEvent;  //CommonScriptableObjects.GetOrLoad(ref chatReceiveGlobalEvent, "ScriptableObjects/AudioEvents/HUDCommon/ChatReceiveGlobal");
        public AudioEvent chatReceivePrivate    => chatReceivePrivateEvent; //CommonScriptableObjects.GetOrLoad(ref chatReceivePrivateEvent, "ScriptableObjects/AudioEvents/HUDCommon/ChatReceivePrivate");
        public AudioEvent chatSend              => chatSendEvent;           //CommonScriptableObjects.GetOrLoad(ref chatSendEvent, "ScriptableObjects/AudioEvents/HUDCommon/ChatSend");
        public AudioEvent notification          => notificationEvent;       //CommonScriptableObjects.GetOrLoad(ref notificationEvent, "ScriptableObjects/AudioEvents/HUDCommon/Notification");
        public AudioEvent sliderValueChange     => sliderValueChangeEvent;  //CommonScriptableObjects.GetOrLoad(ref sliderValueChangeEvent, "ScriptableObjects/AudioEvents/HUDCommon/SliderValueChange");
        public AudioEvent inputFieldFocus       => inputFieldFocusEvent;    //CommonScriptableObjects.GetOrLoad(ref inputFieldFocusEvent, "ScriptableObjects/AudioEvents/HUDCommon/InputFieldFocus");
        public AudioEvent inputFieldUnfocus     => inputFieldUnfocusEvent;  //CommonScriptableObjects.GetOrLoad(ref inputFieldUnfocusEvent, "ScriptableObjects/AudioEvents/HUDCommon/InputFieldUnfocus");
        public AudioEvent UIHide                => UIHideEvent;             //CommonScriptableObjects.GetOrLoad(ref UIHideEvent, "ScriptableObjects/AudioEvents/HUDCommon/UIHide");
        public AudioEvent UIShow                => UIShowEvent;             //CommonScriptableObjects.GetOrLoad(ref UIShowEvent, "ScriptableObjects/AudioEvents/HUDCommon/UIUnhide");
        public AudioEvent tooltipPopup          => tooltipPopupEvent;       //CommonScriptableObjects.GetOrLoad(ref tooltipPopupEvent, "ScriptableObjects/AudioEvents/HUDCommon/TooltipPopup");
        public AudioEvent_WithPitchIncrement listItemAppear => listItemAppearEvent;//CommonScriptableObjects.GetOrLoad(ref listItemAppearEvent, "ScriptableObjects/AudioEvents/HUDCommon/ListItemAppear");
        
        public AudioEvent builderEnter          => builderEnterEvent;
        public AudioEvent builderReady          => builderReadyEvent;

        //TODO:DELETE AWAKE - used just for the bootstraping to rewrite bad code
        // Just incase you dont know how scriptables work, Awake is called when you create a new copy - i.e in the editor
 /*       void Awake() {
            cameraFadeInEvent           = (AudioEvent)Resources.Load<AudioEvent>("ScriptableObjects/AudioEvents/HUDCommon/CameraFadeIn");
            cameraFadeOutEvent          = (AudioEvent)Resources.Load<AudioEvent>("ScriptableObjects/AudioEvents/HUDCommon/CameraFadeOut");
            buttonHoverEvent            = (AudioEvent)Resources.Load<AudioEvent>("ScriptableObjects/AudioEvents/HUDCommon/ButtonHover");    
            buttonClickEvent            = (AudioEvent)Resources.Load<AudioEvent>("ScriptableObjects/AudioEvents/HUDCommon/ButtonClick");    
            buttonReleaseEvent          = (AudioEvent)Resources.Load<AudioEvent>("ScriptableObjects/AudioEvents/HUDCommon/ButtonRelease");    
            cancelEvent                 = (AudioEvent)Resources.Load<AudioEvent>("ScriptableObjects/AudioEvents/HUDCommon/Cancel");    
            confirmEvent                = (AudioEvent)Resources.Load<AudioEvent>("ScriptableObjects/AudioEvents/HUDCommon/Confirm");    
            dialogOpenEvent             = (AudioEvent)Resources.Load<AudioEvent>("ScriptableObjects/AudioEvents/HUDCommon/DialogOpen");    
            dialogCloseEvent            = (AudioEvent)Resources.Load<AudioEvent>("ScriptableObjects/AudioEvents/HUDCommon/DialogClose");    
            enableEvent                 = (AudioEvent)Resources.Load<AudioEvent>("ScriptableObjects/AudioEvents/HUDCommon/Enable");    
            errorEvent                  = (AudioEvent)Resources.Load<AudioEvent>("ScriptableObjects/AudioEvents/HUDCommon/Error");    
            disableEvent                = (AudioEvent)Resources.Load<AudioEvent>("ScriptableObjects/AudioEvents/HUDCommon/Disable");    
            fadeInEvent                 = (AudioEvent)Resources.Load<AudioEvent>("ScriptableObjects/AudioEvents/HUDCommon/FadeIn");    
            fadeOutEvent                = (AudioEvent)Resources.Load<AudioEvent>("ScriptableObjects/AudioEvents/HUDCommon/FadeOut");    
            chatReceiveGlobalEvent      = (AudioEvent)Resources.Load<AudioEvent>("ScriptableObjects/AudioEvents/HUDCommon/ChatReceiveGlobal");
            chatReceivePrivateEvent     = (AudioEvent)Resources.Load<AudioEvent>("ScriptableObjects/AudioEvents/HUDCommon/ChatReceivePrivate");
            chatSendEvent               = (AudioEvent)Resources.Load<AudioEvent>("ScriptableObjects/AudioEvents/HUDCommon/ChatSend");
            notificationEvent           = (AudioEvent)Resources.Load<AudioEvent>("ScriptableObjects/AudioEvents/HUDCommon/Notification");
            sliderValueChangeEvent      = (AudioEvent)Resources.Load<AudioEvent>("ScriptableObjects/AudioEvents/HUDCommon/SliderValueChange");
            inputFieldFocusEvent        = (AudioEvent)Resources.Load<AudioEvent>("ScriptableObjects/AudioEvents/HUDCommon/InputFieldFocus");
            inputFieldUnfocusEvent      = (AudioEvent)Resources.Load<AudioEvent>("ScriptableObjects/AudioEvents/HUDCommon/InputFieldUnfocus");
            UIHideEvent                 = (AudioEvent)Resources.Load<AudioEvent>("ScriptableObjects/AudioEvents/HUDCommon/UIHide");
            UIShowEvent                 = (AudioEvent)Resources.Load<AudioEvent>("ScriptableObjects/AudioEvents/HUDCommon/UIUnhide");
            tooltipPopupEvent           = (AudioEvent)Resources.Load<AudioEvent>("ScriptableObjects/AudioEvents/HUDCommon/TooltipPopup");
            listItemAppearEvent         = (AudioEvent_WithPitchIncrement)Resources.Load<AudioEvent_WithPitchIncrement>("ScriptableObjects/AudioEvents/HUDCommon/ListItemAppear");
        }
    */
    }

}
