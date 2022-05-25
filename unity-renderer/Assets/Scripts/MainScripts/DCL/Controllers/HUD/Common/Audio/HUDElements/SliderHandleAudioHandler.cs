using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderHandleAudioHandler : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    Slider slider;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (slider != null && !Input.GetMouseButton(0))
        {
            if (slider.interactable){
                ABEYController.i.AudioEvents.buttonHover.Play(true);
               //AudioScriptableObjects.buttonHover.Play(true);
            }
        }
    }
}