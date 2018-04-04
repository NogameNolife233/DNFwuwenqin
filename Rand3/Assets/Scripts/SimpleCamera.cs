using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCamera : MonoBehaviour {

    Transform player;
    Vector3 offsetPos;

	void Start () {
        player = GameObject.Find("Player").transform;
        offsetPos = transform.position - player.position;
	}
	

	void Update () {
        Vector3 v = player.position + offsetPos;
        transform.position = new Vector3(v.x, transform.position.y, v.z);
	}
}
