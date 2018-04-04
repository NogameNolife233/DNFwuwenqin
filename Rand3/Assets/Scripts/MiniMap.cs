using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour {

    Camera mimiMapCamera;
	void Start ()
    {
        mimiMapCamera = GameObject.Find("MinimapCamera").GetComponent<Camera>();
	}
	
	
	void Update ()
    {
		
	}
    //放大
    public void OnZoomInClick()
    {
        mimiMapCamera.fieldOfView -= 10f; 
    }
    //缩小
    public void OnZoomOutClick()
    {
        mimiMapCamera.fieldOfView += 10f;
    }
}
