using DCL.SettingsCommon.SettingsControllers.BaseControllers;
using UnityEngine;

namespace DCL.SettingsCommon.SettingsControllers.SpecificControllers
{
    [CreateAssetMenu(menuName = "Settings/Controllers/Controls/Hide UI", fileName = "HideUIControlController")]
    public class HideUIControlController : ToggleSettingsControlController
    {
        public override void Initialize()
        {
            base.Initialize();

            ABEYController.i.CommonScriptables.allUIHidden.OnChange += AllUIHiddenChanged;
            AllUIHiddenChanged(ABEYController.i.CommonScriptables.allUIHidden.Get(), false);
        }

        public override object GetStoredValue() { return currentGeneralSettings.hideUI; }

        public override void UpdateSetting(object newValue)
        {
            currentGeneralSettings.hideUI = (bool)newValue;
            ABEYController.i.CommonScriptables.allUIHidden.Set(currentGeneralSettings.hideUI);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            ABEYController.i.CommonScriptables.allUIHidden.OnChange -= AllUIHiddenChanged;
        }

        private void AllUIHiddenChanged(bool current, bool previous)
        {
            currentGeneralSettings.hideUI = current;
            ApplySettings();
        }
    }
}