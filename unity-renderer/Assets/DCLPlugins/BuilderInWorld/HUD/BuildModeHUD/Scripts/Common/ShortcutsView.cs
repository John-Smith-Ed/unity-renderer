using System;
using UnityEngine;
using UnityEngine.UI;

public interface IShortcutsView
{
    event Action OnCloseButtonClick;

    void OnCloseClick();
    void SetActive(bool isActive);
}

public class ShortcutsView : MonoBehaviour, IShortcutsView
{
    public event Action OnCloseButtonClick;

    [SerializeField] internal Button closeButton;

    private const string VIEW_PATH = "Common/ShortcutsView";

    internal static ShortcutsView Create()
    {
        var view = Instantiate(ABEYController.i.GetPrefab(VIEW_PATH)).GetComponent<ShortcutsView>();
        //var view = Instantiate(ABEYController.i.GetPrefab(VIEW_PATH)).GetComponent<ShortcutsView>();
        view.gameObject.name = "_ShortcutsView";

        return view;
    }

    private void Awake() { closeButton.onClick.AddListener(OnCloseClick); }

    private void OnEnable() {
        ABEYController.i.AudioEvents.dialogOpen.Play();
        // AudioScriptableObjects.dialogOpen.Play(); 
    }

    private void OnDisable() { 
        ABEYController.i.AudioEvents.dialogClose.Play();
        // AudioScriptableObjects.dialogClose.Play(); 
    }

    public void SetActive(bool isActive) { gameObject.SetActive(isActive); }

    public void OnCloseClick() { OnCloseButtonClick?.Invoke(); }
}