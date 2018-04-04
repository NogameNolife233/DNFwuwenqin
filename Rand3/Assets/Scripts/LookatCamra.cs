using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookatCamra : MonoBehaviour {

    public Camera mCamera;
	void Start ()
    {
		
	}
	

	void Update ()
    {
        Vector3 v = mCamera.transform.position - transform.position;
        v.x = v.z = 0;
        transform.LookAt(mCamera.transform.position- v);
        transform.Rotate(0, 180, 0);
	}
}
