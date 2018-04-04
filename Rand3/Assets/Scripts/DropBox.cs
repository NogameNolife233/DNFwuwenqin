using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBox : MonoBehaviour {

	
	void Start ()
    {
		
	}
	
	
	void Update ()
    {
        transform.Rotate(new Vector3(0, 40, 0)*Time.deltaTime);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            int id = Random.Range(1, 15);
            KnapsackPanel.Instance.StoreItem(id);
            Destroy(gameObject);
        }
    }
}
