using UnityEngine;
using System.Collections;

public class shakecameraControl : MonoBehaviour {


    private float time=0f;
    public float StartTime=2f;
    private float Time0;
    public float shaketime=2f;
    public bool end = false;
	// Use this for initialization
	void Start () 
    {

        
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!end)
        {

            time += Time.deltaTime;
            if (time >= StartTime)
            {
                Time0+=Time.deltaTime;
                SC_shakeCamera.shakeCamera();
                if (Time0>=shaketime)
                {
                    end = true;
                }
            }
        }
	
	}
}
