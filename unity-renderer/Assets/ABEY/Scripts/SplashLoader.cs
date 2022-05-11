using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashLoader : MonoBehaviour {

    [SerializeField] CanvasGroup group;

    IEnumerator Start(){
        group.alpha=0f;
        yield return new WaitForSeconds(1f);
        while(group.alpha<1){
            group.alpha += Time.deltaTime*2f;
            yield return null;
        }

        yield return new WaitForSeconds(2f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);

    }
}
