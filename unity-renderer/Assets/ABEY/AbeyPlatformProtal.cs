using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbeyPlatformProtal : MonoBehaviour
{
    
    void OnCollisionEnter(Collision other) {
        Debug.Log(other);
        Debug.Log(other.gameObject.name);
        Debug.Log(other.gameObject.tag);
    }
    void OnTriggerEnter(Collider other) {
        Debug.Log(other);
        Debug.Log(other.name);
        Debug.Log(other.tag);
        
        if(other.tag=="Player"){
            DCLCharacterController.i.Teleport("{\"x\":0,\"y\":0,\"z\":0} ");
        }
    }
}
