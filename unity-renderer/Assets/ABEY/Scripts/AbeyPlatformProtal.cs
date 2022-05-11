using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbeyPlatformProtal : MonoBehaviour
{
    
    void Update() {
        if(!Physics.autoSimulation)
            Physics.autoSimulation=true;
    }
    void OnTriggerEnter(Collider other) {
        Debug.Log(other);
        Debug.Log(other.name);
        Debug.Log(other.tag);
        
        if(other.transform.parent.tag=="Player"){
            DCLCharacterController.i.Teleport("{\"x\":0,\"y\":0,\"z\":0} ");
        }
    }
}
