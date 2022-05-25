using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToggleAudioHandler : MonoBehaviour, IPointerDownHandler
{
    Toggle toggle;

    void Awake()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnValueChanged);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (toggle != null)
        {
            if (toggle.interactable){
                ABEYController.i.AudioEvents.buttonClick.Play(true);
               // AudioScriptableObjects.buttonClick.Play(true);
            }
        }
    }

    public void OnValueChanged(bool isOn)
    {
        if (toggle != null)
        {
            if (toggle.interactable)
            {
                if (isOn){
                    ABEYController.i.AudioEvents.enable.Play(true);
                   // AudioScriptableObjects.enable.Play(true);
                }else{
                    ABEYController.i.AudioEvents.disable.Play(true);
                   // AudioScriptableObjects.disable.Play(true);
                }
            }
        }
    }
}