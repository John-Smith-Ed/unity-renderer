using DCL.Helpers;
using DCL.Interface;
using UnityEngine;

public class RenderingController : MonoBehaviour
{
    public static float firstActivationTime { get; private set; }
    private bool firstActivationTimeHasBeenSet = false;
    private bool VERBOSE = false;

    public CompositeLock renderingActivatedAckLock = new CompositeLock();

    private bool activatedRenderingBefore { get; set; } = false;

    void Awake()
    {
        ABEYController.i.CommonScriptables.rendererState.OnLockAdded += AddLock;
        ABEYController.i.CommonScriptables.rendererState.OnLockRemoved += RemoveLock;
        ABEYController.i.CommonScriptables.rendererState.Set(false);
    }

    void OnDestroy()
    {
        ABEYController.i.CommonScriptables.rendererState.OnLockAdded -= AddLock;
        ABEYController.i.CommonScriptables.rendererState.OnLockRemoved -= RemoveLock;
    }

    [ContextMenu("Disable Rendering")]
    public void DeactivateRendering()
    {
        if (!ABEYController.i.CommonScriptables.rendererState.Get())
            return;

        DeactivateRendering_Internal();
    }

    void DeactivateRendering_Internal()
    {
        DCL.Configuration.ParcelSettings.VISUAL_LOADING_ENABLED = false;
        ABEYController.i.CommonScriptables.rendererState.Set(false);
        WebInterface.ReportControlEvent(new WebInterface.DeactivateRenderingACK());
    }

    [ContextMenu("Enable Rendering")]
    public void ActivateRendering() { ActivateRendering(forceActivate: false); }

    public void ForceActivateRendering() { ActivateRendering(forceActivate: true); }

    public void ActivateRendering(bool forceActivate)
    {
        if (ABEYController.i.CommonScriptables.rendererState.Get())
            return;

        if (!firstActivationTimeHasBeenSet)
        {
            firstActivationTime = Time.realtimeSinceStartup;
            firstActivationTimeHasBeenSet = true;
        }

        if (!forceActivate && !renderingActivatedAckLock.isUnlocked)
        {
            renderingActivatedAckLock.OnAllLocksRemoved -= ActivateRendering_Internal;
            renderingActivatedAckLock.OnAllLocksRemoved += ActivateRendering_Internal;
            return;
        }

        ActivateRendering_Internal();
    }

    private void ActivateRendering_Internal()
    {
        renderingActivatedAckLock.OnAllLocksRemoved -= ActivateRendering_Internal;

        if (!activatedRenderingBefore)
        {
            Utils.UnlockCursor();
            activatedRenderingBefore = true;
        }

        DCL.Configuration.ParcelSettings.VISUAL_LOADING_ENABLED = true;
        ABEYController.i.CommonScriptables.rendererState.Set(true);

        WebInterface.ReportControlEvent(new WebInterface.ActivateRenderingACK());
    }

    private void AddLock(object id)
    {
        if (ABEYController.i.CommonScriptables.rendererState.Get())
            return;

        if (VERBOSE)
            Debug.Log("Add lock: " + id);

        renderingActivatedAckLock.AddLock(id);
    }

    private void RemoveLock(object id)
    {
        if (VERBOSE)
            Debug.Log("remove lock: " + id);

        renderingActivatedAckLock.RemoveLock(id);
    }
}