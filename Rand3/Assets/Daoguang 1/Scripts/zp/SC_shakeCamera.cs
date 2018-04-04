using UnityEngine;
using System.Collections;
public class SC_shakeCamera : MonoBehaviour
{

    private float shakeTime = 0.0f;
    private float fps = 60.0f;
    private float frameTime = 0.0f;
    private float shakeDelta = 0.005f;
    public Camera cam;
    public static bool isshakeCamera = false;
    // Use this for initialization
    void Start()
    {
        shakeTime = 0.1f;
        fps = 60.0f;
        frameTime = 0.01f;
        shakeDelta = 0.005f;
		cam = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();

    }

    // Update is called once per frame
    void Update()
    {
        if (isshakeCamera)
        {
            if (shakeTime > 0)
            {
                shakeTime -= Time.deltaTime;
                if (shakeTime <= 0)
                {
                    cam.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
                    isshakeCamera = false;
                    shakeTime = 0.1f;
                    fps = 60.0f;
                    frameTime = 0.01f;
                    shakeDelta = 0.005f;
                }
                else
                {
                    frameTime += Time.deltaTime;

                    if (frameTime > 1.0 / fps)
                    {
                        frameTime = 0;
                        cam.rect = new Rect(shakeDelta * (-1.0f + 2.0f * Random.value), shakeDelta * (-1.0f + 2.0f * Random.value), 1.0f, 1.0f);

                    }
                }
            }
        }
        //else if (Input.GetMouseButtonDown(0))
        //{
        //    shakeCamera();
        //}

    }

    public static void shakeCamera()
    {
        isshakeCamera = true;
    }
}