using System;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using DCL.Helpers;
using DCL.Interface;
using DCL;
using UnityEngine;

namespace DCL.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        internal new UnityEngine.Camera camera;

        private Transform cameraTransform;

        [SerializeField]
        internal CinemachineFreeLook thirdPersonCamera; 

        [Header("Virtual Cameras")]
        [SerializeField]
        internal CinemachineBrain cameraBrain;

        [SerializeField]
        internal CameraStateBase[] cameraModes;

        [Header("InputActions")]
        [SerializeField]
        internal InputAction_Trigger cameraChangeAction;
        [SerializeField]
        internal InputAction_Measurable mouseWheelAction;

        internal Dictionary<CameraMode.ModeId, CameraStateBase> cachedModeToVirtualCamera;

        public delegate void CameraBlendStarted();

        public event CameraBlendStarted onCameraBlendStarted;

        public delegate void CameraBlendFinished();

        public event CameraBlendFinished onCameraBlendFinished;

        private bool wasBlendingLastFrame;

        private float mouseWheelThreshold = 0.04f;

        private Vector3Variable cameraForward => ABEYController.i.CommonScriptables.cameraForward;
        private Vector3Variable cameraRight => ABEYController.i.CommonScriptables.cameraRight;
        private Vector3Variable cameraPosition => ABEYController.i.CommonScriptables.cameraPosition;
        private Vector3Variable worldOffset => ABEYController.i.CommonScriptables.worldOffset;
        private BooleanVariable cameraIsBlending => ABEYController.i.CommonScriptables.cameraIsBlending;

        public CameraStateBase currentCameraState => cachedModeToVirtualCamera[ABEYController.i.CommonScriptables.cameraMode];

        [HideInInspector]
        public Action<CameraMode.ModeId> onSetCameraMode;

        private void Awake()
        {
            cameraTransform = this.camera.transform;

            ABEYController.i.CommonScriptables.rendererState.OnChange += OnRenderingStateChanged;
            OnRenderingStateChanged(ABEYController.i.CommonScriptables.rendererState.Get(), false);

            ABEYController.i.CommonScriptables.cameraBlocked.OnChange += CameraBlocked_OnChange;

            cachedModeToVirtualCamera = cameraModes.ToDictionary(x => x.cameraModeId, x => x);

            using (var iterator = cachedModeToVirtualCamera.GetEnumerator())
            {
                while (iterator.MoveNext())
                {
                    iterator.Current.Value.Initialize(camera);
                }
            }

            cameraChangeAction.OnTriggered += OnCameraChangeAction;
            mouseWheelAction.OnValueChanged += OnMouseWheelChangeValue;
            worldOffset.OnChange += OnWorldReposition;
            ABEYController.i.CommonScriptables.cameraMode.OnChange += OnCameraModeChange;

            OnCameraModeChange(ABEYController.i.CommonScriptables.cameraMode, CameraMode.ModeId.FirstPerson);

            if (ABEYController.i.CommonScriptables.isFullscreenHUDOpen)
                OnFullscreenUIVisibilityChange(ABEYController.i.CommonScriptables.isFullscreenHUDOpen.Get(), !ABEYController.i.CommonScriptables.isFullscreenHUDOpen.Get());

            ABEYController.i.CommonScriptables.isFullscreenHUDOpen.OnChange += OnFullscreenUIVisibilityChange;

            DataStore.i.camera.outputTexture.OnChange += OnOutputTextureChange;
            OnOutputTextureChange(DataStore.i.camera.outputTexture.Get(), null);

            DataStore.i.camera.invertYAxis.OnChange += SetInvertYAxis;
            SetInvertYAxis(DataStore.i.camera.invertYAxis.Get(), false);

            wasBlendingLastFrame = false;
          
            
        }

        void OnFullscreenUIVisibilityChange(bool visibleState, bool prevVisibleState)
        {
            //if (visibleState == prevVisibleState)
                //return;
            //TODO:A.B    
            // THIS IS JUST STUPID! - one camera should always be enabled
            //camera.enabled = !visibleState && ABEYController.i.CommonScriptables.rendererState.Get();
            camera.enabled = true;
        }

        void OnOutputTextureChange(RenderTexture current, RenderTexture previous)
        {
            camera.targetTexture = current;
        }

        public bool TryGetCameraStateByType<T>(out CameraStateBase searchedCameraState)
        {
            foreach (CameraStateBase cameraMode in cameraModes)
            {
                if (cameraMode.GetType() == typeof(T))
                {
                    searchedCameraState = cameraMode;
                    return true;
                }
            }

            searchedCameraState = null;
            return false;
        }

        //TODO:A.B - why would they disable the one camera??? noobs!
        private void OnRenderingStateChanged(bool enabled, bool prevState) {  
            camera.enabled =true; 
            //camera.enabled = enabled && !ABEYController.i.CommonScriptables.isFullscreenHUDOpen;
        }

        private void CameraBlocked_OnChange(bool current, bool previous)
        {
            foreach (CameraStateBase cam in cameraModes)
            {
                cam.OnBlock(current);
            }
        }

        private void OnMouseWheelChangeValue(DCLAction_Measurable action, float value)
        {
            if ((value > -mouseWheelThreshold && value < mouseWheelThreshold) || ABEYController.i.CommonScriptables.cameraModeInputLocked.Get()) return;
            if (Utils.IsPointerOverUIElement()) return;

            if (ABEYController.i.CommonScriptables.cameraMode == CameraMode.ModeId.FirstPerson && value < -mouseWheelThreshold)
                SetCameraMode(CameraMode.ModeId.ThirdPerson);   

            if (ABEYController.i.CommonScriptables.cameraMode == CameraMode.ModeId.ThirdPerson && value > mouseWheelThreshold)
                SetCameraMode(CameraMode.ModeId.FirstPerson);
        }

        private void OnCameraChangeAction(DCLAction_Trigger action)
        {
            if (ABEYController.i.CommonScriptables.cameraMode == CameraMode.ModeId.FirstPerson)
            {
                SetCameraMode(CameraMode.ModeId.ThirdPerson);
            }
            else
            {
                SetCameraMode(CameraMode.ModeId.FirstPerson);
            }
        }

        public LayerMask GetCulling() => camera.cullingMask;

        public void SetCulling(LayerMask mask) => camera.cullingMask = mask;

        public void SetCameraMode(CameraMode.ModeId newMode) { ABEYController.i.CommonScriptables.cameraMode.Set(newMode); }

        private void OnCameraModeChange(CameraMode.ModeId current, CameraMode.ModeId previous)
        {
            cachedModeToVirtualCamera[previous].OnUnselect();
            cachedModeToVirtualCamera[current].OnSelect();

            WebInterface.ReportCameraChanged(current);

            onSetCameraMode?.Invoke(current);
        }

        public CameraStateBase GetCameraMode(CameraMode.ModeId mode) { return cameraModes.FirstOrDefault(x => x.cameraModeId == mode); }

        private void OnWorldReposition(Vector3 newValue, Vector3 oldValue) { transform.position += newValue - oldValue; }

        private void Update()
        {
            cameraForward.Set(cameraTransform.forward);
            cameraRight.Set(cameraTransform.right);
            DataStore.i.camera.rotation.Set(cameraTransform.rotation);
            DataStore.i.camera.transform.Set(cameraTransform);
            cameraPosition.Set(cameraTransform.position);
            cameraIsBlending.Set(cameraBrain.IsBlending);

            if (cameraBrain.IsBlending)
            {
                if (!wasBlendingLastFrame)
                    onCameraBlendStarted?.Invoke();

                wasBlendingLastFrame = true;
            }
            else if (wasBlendingLastFrame)
            {
                onCameraBlendFinished?.Invoke();

                wasBlendingLastFrame = false;
            }

            currentCameraState?.OnUpdate();
        }

        public void SetRotation(string setRotationPayload)
        {
            var payload = Utils.FromJsonWithNulls<SetRotationPayload>(setRotationPayload);
            currentCameraState?.OnSetRotation(payload);
        }

        public void SetRotation(float x, float y, float z, Vector3? cameraTarget = null) { currentCameraState?.OnSetRotation(new SetRotationPayload() { x = x, y = y, z = z, cameraTarget = cameraTarget }); }

        public Vector3 GetRotation()
        {
            if (currentCameraState != null)
                return currentCameraState.OnGetRotation();

            return Vector3.zero;
        }

        public Vector3 GetPosition() { return CinemachineCore.Instance.GetActiveBrain(0).ActiveVirtualCamera.State.FinalPosition; }

        public UnityEngine.Camera GetCamera() { return camera; }

        private void SetInvertYAxis(bool current, bool previous)
        {
            thirdPersonCamera.m_YAxis.m_InvertInput = !current;
        }

        private void OnDestroy()
        {
            if (cachedModeToVirtualCamera != null)
            {
                using (var iterator = cachedModeToVirtualCamera.GetEnumerator())
                {
                    while (iterator.MoveNext())
                    {
                        iterator.Current.Value.Cleanup();
                    }
                }
            }

            worldOffset.OnChange -= OnWorldReposition;
            cameraChangeAction.OnTriggered -= OnCameraChangeAction;
            mouseWheelAction.OnValueChanged -= OnMouseWheelChangeValue;
            ABEYController.i.CommonScriptables.rendererState.OnChange -= OnRenderingStateChanged;
            ABEYController.i.CommonScriptables.cameraBlocked.OnChange -= CameraBlocked_OnChange;
            ABEYController.i.CommonScriptables.isFullscreenHUDOpen.OnChange -= OnFullscreenUIVisibilityChange;
            ABEYController.i.CommonScriptables.cameraMode.OnChange -= OnCameraModeChange;
            DataStore.i.camera.outputTexture.OnChange -= OnOutputTextureChange;
            DataStore.i.camera.invertYAxis.OnChange -= SetInvertYAxis;
        }

        [Serializable]
        public class SetRotationPayload
        {
            public float x;
            public float y;
            public float z;
            public Vector3? cameraTarget;
        }
    }
}