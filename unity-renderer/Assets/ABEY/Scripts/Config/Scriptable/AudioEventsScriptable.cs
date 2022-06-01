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


        public AudioEvent cameraFadeIn          => cameraFadeInEvent;       
        public AudioEvent cameraFadeOut         => cameraFadeOutEvent;      
        public AudioEvent buttonHover           => buttonHoverEvent;        
        public AudioEvent buttonClick           => buttonClickEvent;        
        public AudioEvent buttonRelease         => buttonReleaseEvent;      
        public AudioEvent cancel                => cancelEvent;             
        public AudioEvent confirm               => confirmEvent;            
        public AudioEvent dialogOpen            => dialogOpenEvent;         
        public AudioEvent dialogClose           => dialogCloseEvent;        
        public AudioEvent enable                => enableEvent;             
        public AudioEvent error                 => errorEvent;              
        public AudioEvent disable               => disableEvent;            
        public AudioEvent fadeIn                => fadeInEvent;             
        public AudioEvent fadeOut               => fadeOutEvent;            
        public AudioEvent chatReceiveGlobal     => chatReceiveGlobalEvent;  
        public AudioEvent chatReceivePrivate    => chatReceivePrivateEvent; 
        public AudioEvent chatSend              => chatSendEvent;           
        public AudioEvent notification          => notificationEvent;       
        public AudioEvent sliderValueChange     => sliderValueChangeEvent;  
        public AudioEvent inputFieldFocus       => inputFieldFocusEvent;    
        public AudioEvent inputFieldUnfocus     => inputFieldUnfocusEvent;  
        public AudioEvent UIHide                => UIHideEvent;             
        public AudioEvent UIShow                => UIShowEvent;             
        public AudioEvent tooltipPopup          => tooltipPopupEvent;       
        public AudioEvent_WithPitchIncrement listItemAppear => listItemAppearEvent;
        
        public AudioEvent builderEnter          => builderEnterEvent;
        public AudioEvent builderReady          => builderReadyEvent;

    }

}
