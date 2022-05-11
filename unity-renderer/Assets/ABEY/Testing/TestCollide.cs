using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollide : MonoBehaviour{

   
    void OnTriggerEnter(Collider other) {
        Debug.Log(other.name);
    }

    void OnCollisionEnter(Collision other){
        Debug.Log(other.gameObject.name);
    }
    
    
}
