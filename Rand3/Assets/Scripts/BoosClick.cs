using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoosClick : MonoBehaviour {

    public Button boos;
    public GateWay cube;
	void Start ()
    {
        boos = GetComponent<Button>();
        boos.onClick.AddListener(CreatePlan);
        cube = GameObject.FindObjectOfType<GateWay>();

    }
	
	
	void Update () {
		
	}

    public void CreatePlan()
    {
       GameObject game = cube.CreatRoom();
        cube = game.GetComponentInChildren<GateWay>();
        game.transform.parent = null;
        game.transform.localScale = new Vector3(1, 1, 1);
    }
}
