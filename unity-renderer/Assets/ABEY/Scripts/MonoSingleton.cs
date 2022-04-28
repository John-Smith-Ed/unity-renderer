/**
    !!
    This is the right way to create a singleton
    the code based in DCL is mixed with ok singletons and alot of really bad singletons that can lead to memory leaks and unexpected behavior
    Do not use DCL code without knowing what is doing 

    This is safe to use for any and all new singltons we write
**/

namespace ABEY {

    using UnityEngine;

    /// <summary>
    ///  Easy singleton helper
    ///  to use, extend script with your scripts type : MonoSingleton<MyClassName> {
    ///  then be sure to call the base awake in your script's awake like so
    ///
    ///  private void Awake(){
    ///     base.Awake();
    ///     your code ...
    ///  }
    /// Also if you dont want the instance to destroy pass true in awake like so base.Awake(true);
    /// </summary>


	public class MonoSingleton<T> : MonoBehaviour where T : Object {

		protected static T instance;

		/// <summary>
		/// The instance
		/// </summary>
		/// <value></value>
		public static T i {
			get {
				if (instance == null) {
					// This is by design, there is two ways your coding wrong and one other possible issue that brought you here
					// 1. your tring to access some object that was never added or created in the game
					// 2. your trying to access this object from an Awake,
					//    - first off you should never try to access other objects in the awake
					//    - this object is being created in the same scene your accessing it, "order of operations" again move from Awake to Start
					// 3. maybe your playing a scene directly which does not have this object in it - same as 1 but for different reason
					throw new System.Exception($"Missing instance on singleton type: {typeof(T).FullName} \n Are you using Awake incorrectly?");
				}
				return instance;
			}
		}
		
		protected void Awake(bool dontDestroy = false) {
			if (instance == null) {
				instance = this as T;
			} else if (instance != this) {
				Destroy(gameObject);	// this way you have to add a gameobject to house the component in the scene, also not this destorys the whole gameobject
				//Destroy(this);		// this allows for adding the component to exsiting objects in the scenes already, also not this DOES NOT destory the whole gameobject, only this script on it
				return;
			}

			if (dontDestroy) {
				DontDestroyOnLoad(gameObject);
			}
		}
	}
}