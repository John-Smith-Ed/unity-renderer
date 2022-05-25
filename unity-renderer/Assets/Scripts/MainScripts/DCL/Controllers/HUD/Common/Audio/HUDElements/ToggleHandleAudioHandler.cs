using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToggleHandleAudioHandler : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    Toggle toggle;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (toggle != null && !Input.GetMouseButton(0))
        {
            if (toggle.interactable) {
                ABEYController.i.AudioEvents.buttonHover.Play(true);
               // AudioScriptableObjects.buttonHover.Play(true);
            }
        }
    }
}