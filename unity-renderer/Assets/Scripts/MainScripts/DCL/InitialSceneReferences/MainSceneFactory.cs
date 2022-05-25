using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace DCL
{
    // Read this if you dont know why this code sucks - we need to remove all Resources - this has been the standard sense what unity 4.1? like 100years agao....
    //https://learn.unity.com/tutorial/assets-resources-and-assetbundles#5c7f8528edbc2a002053b5a7

    public static class MainSceneFactory
    {

        //A.B Not called anymore, placed prefab in scene, and sence it had a mono on it we use that to set refs, processes were there but not
        // utilized so why have run extra code and also we can move out of Resources - better boot times
        public static List<GameObject> CreatePlayerSystems()
        {

            
            List<GameObject> result = new List<GameObject>();
/*
            GameObject playerGo = LoadAndInstantiate("Player");
            var playerReferences = playerGo.GetComponent<PlayerReferences>();
            SceneReferences.i.playerAvatarController = playerReferences.avatarController;
            SceneReferences.i.biwCameraParent = playerReferences.biwCameraRoot;
            SceneReferences.i.inputController = playerReferences.inputController;
            SceneReferences.i.cursorCanvas = playerReferences.cursorCanvas;
            SceneReferences.i.cameraController = playerReferences.cameraController;
            SceneReferences.i.mainCamera = playerReferences.mainCamera;
            SceneReferences.i.thirdPersonCamera = playerReferences.thirdPersonCamera;
            SceneReferences.i.firstPersonCamera = playerReferences.firstPersonCamera;
*/
// TODO: Delete JUNK
//A.B this does NOTHING - they add to a list scoped inside here only and its returned but the only one caller does not capture it so why?
// Commenting out this junk
            // Why we don't just add the playerGo?
            // This happens because the characterController reparents itself.
            // (and any of our systems may do this as well).
//            result.Add( playerReferences.cursorCanvas.gameObject );
 //           result.Add( playerReferences.cameraController.gameObject );
 //           result.Add( playerReferences.inputController.gameObject );
   //         result.Add( playerReferences.avatarController.gameObject );
  //          result.Add( playerGo );

            return result;
        }

        public static GameObject CreateMouseCatcher()
        {
            GameObject result = LoadAndInstantiate("MouseCatcher");
            MouseCatcher mouseCatcher = result.GetComponent<MouseCatcher>();
            SceneReferences.i.mouseCatcher = mouseCatcher;
            return result;
        }

        public static GameObject CreateHudController() => LoadAndInstantiate("HUDController");

        public static GameObject CreateAudioHandler() => LoadAndInstantiate("HUDAudioHandler");

        public static GameObject CreateNavMap() => LoadAndInstantiate("NavMap");

        public static GameObject CreateSettingsController() => LoadAndInstantiate("SettingsController");

        public static GameObject CreateEnvironment(string prefabPath = "Environment")
        {
            GameObject result = LoadAndInstantiate(prefabPath);
            var env = result.GetComponent<EnvironmentReferences>();
            SceneReferences.i.environmentLight = env.environmentLight;
            SceneReferences.i.postProcessVolume = env.postProcessVolume;
            SceneReferences.i.groundVisual = env.ground;
            return result;
        }

        public static GameObject CreateBridges()
        {
            if (SceneReferences.i.bridgeGameObject == null)
            {
                var bridges = LoadAndInstantiate("Bridges");
                SceneReferences.i.bridgeGameObject = bridges;
                return bridges;
            }

            return SceneReferences.i.bridgeGameObject;
        }

        public static GameObject CreateEventSystem() => LoadAndInstantiate("EventSystem");

        public static GameObject CreateInteractionHoverCanvas() => LoadAndInstantiate("InteractionHoverCanvas");

        public static BuilderInWorldBridge CreateBuilderInWorldBridge(GameObject gameObject = null)
        {
            if (gameObject == null)
                gameObject = new GameObject("BuilderInWorldBridge");

            var instance = gameObject.AddComponent<BuilderInWorldBridge>();
            SceneReferences.i.biwBridgeGameObject = instance.gameObject;
            return instance;
        }

        private static GameObject LoadAndInstantiate(string name)
        {
            GameObject instance = UnityEngine.Object.Instantiate(Resources.Load(name)) as GameObject;
            instance.name = name;
            return instance;
        }
    }
}