using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusStateBridge : MonoBehaviour
{
    public void Awake() { ABEYController.i.CommonScriptables.focusState.Set(true); }

    public void ReportFocusOn() { ABEYController.i.CommonScriptables.focusState.Set(true); }

    public void ReportFocusOff() { ABEYController.i.CommonScriptables.focusState.Set(false); }
}