using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSwordout : MonoBehaviour
{
    public GameObject FireSword;
    public GameObject portal;
    GameObject LongSword;

	
	void Start ()
    {
        LongSword = GameObject.Find("LongSword");
	}
	
	
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            int id = 666;
            KnapsackPanel.Instance.StoreItem(id);
            FireSword.SetActive(true);
            LongSword.SetActive(false);
            Destroy(gameObject);
            other.gameObject.transform.position = portal.transform.position;
            Destroy(portal);

        }
    }
}
