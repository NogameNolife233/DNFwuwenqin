using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateWay : MonoBehaviour
{
    CanvasGroup Panel;
    public GameObject doorRoom;

    void Start ()
    {
        Panel = GameObject.Find("RoomPanel").GetComponent<CanvasGroup>();
        Panel.alpha =0;
        Panel.blocksRaycasts = false;
	}
	
	
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Panel.alpha = 1;
            Panel.blocksRaycasts = true;

        }
    }

    public GameObject CreatRoom()
    {
        GameObject go = Instantiate(doorRoom, transform);
        go.transform.localPosition = new Vector3(-4.86f, 0, 0);
        Panel.alpha = 0;
        Panel.blocksRaycasts = false;
        return go;
    }
}
