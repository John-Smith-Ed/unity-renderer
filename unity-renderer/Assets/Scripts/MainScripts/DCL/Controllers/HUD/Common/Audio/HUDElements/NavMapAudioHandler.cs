using UnityEngine;
using UnityEngine.EventSystems;

public class NavMapAudioHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData) {
        ABEYController.i.AudioEvents.buttonClick.Play(true);
        //AudioScriptableObjects.buttonClick.Play(true);
    }

    public void OnPointerUp(PointerEventData eventData) {
        ABEYController.i.AudioEvents.buttonRelease.Play(true);
        //AudioScriptableObjects.buttonRelease.Play(true);
    }
}