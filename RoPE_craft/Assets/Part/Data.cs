using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 size;
    public float Mass;

    public bool coll = false;
    void OnCollisionEnter(Collision collision) {
        coll = true;
    }
	 

    void OnCollisionExit(Collision collision) {
	    coll = false;
    }
	 
    void OnCollisionStay(Collision collision) {
	    coll = true;
    }
}
