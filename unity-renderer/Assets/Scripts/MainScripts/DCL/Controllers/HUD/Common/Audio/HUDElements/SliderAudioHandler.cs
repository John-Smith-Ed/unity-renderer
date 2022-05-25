using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderAudioHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(OnValueChanged);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (slider != null)
        {
            if (slider.interactable){
                ABEYController.i.AudioEvents.buttonClick.Play(true);
                //AudioScriptableObjects.buttonClick.Play(true);
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (slider != null){
            if (slider.interactable){
                ABEYController.i.AudioEvents.buttonRelease.Play(true);
                //AudioScriptableObjects.buttonRelease.Play(true);
            }
        }
    }

    void OnValueChanged(float value){
        ABEYController.i.AudioEvents.sliderValueChange.SetPitch(1f + ((slider.value - slider.minValue) / (slider.maxValue - slider.minValue)) * 1.5f);
        ABEYController.i.AudioEvents.sliderValueChange.Play(true);

        //AudioScriptableObjects.sliderValueChange.SetPitch(1f + ((slider.value - slider.minValue) / (slider.maxValue - slider.minValue)) * 1.5f);
        //AudioScriptableObjects.sliderValueChange.Play(true);
    }
}