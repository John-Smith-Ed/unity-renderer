using Cinemachine;
using DCL.Camera;
using UnityEngine;

namespace DCL {

    // Kind of a wast of a mono
    // set up the awake so we can remove from the weired factory and 
    // edit the scene vs all loadinging from Resoures, this will help boot time
    // FYI dont use resourses unless you know why


    public class PlayerReferences : MonoBehaviour {

        [SerializeField] GameObject biwCameraRoot;
        [SerializeField] InputController inputController;
        [SerializeField] GameObject cursorCanvas;
        [SerializeField] PlayerAvatarController avatarController;
        [SerializeField] CameraController cameraController;
        [SerializeField] UnityEngine.Camera mainCamera;
        [SerializeField] CinemachineFreeLook thirdPersonCamera;
        [SerializeField] CinemachineVirtualCamera firstPersonCamera;


        void Start() {
            SceneReferences.i.playerAvatarController    = avatarController;
            SceneReferences.i.biwCameraParent           = biwCameraRoot;
            SceneReferences.i.inputController           = inputController;
            SceneReferences.i.cursorCanvas              = cursorCanvas;
            SceneReferences.i.cameraController          = cameraController;
            SceneReferences.i.mainCamera                = mainCamera;
            SceneReferences.i.thirdPersonCamera         = thirdPersonCamera;
            SceneReferences.i.firstPersonCamera         = firstPersonCamera;
        }

    }
}