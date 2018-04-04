using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoom : MonoBehaviour {

    public GameObject Room;
    public List<CreateChildRoom> rooms = new List<CreateChildRoom>();
    public List<GameObject> rangerooms;
    private bool isCreateBoos2 = false;
    private bool isCreateCaster = false;
    private bool isCreateStore = false;


    void Start ()
    {
        for (int i = 0; i < 18; i++)
        {
            var go = Instantiate(Room, transform).GetComponent<CreateChildRoom>();
            go.index = i;
            go.GetComponent<Image>().raycastTarget = false;
            rooms.Add(go);
            int a = Random.Range(0, rangerooms.Count);
            while (a == 1 && isCreateBoos2 == true)
            {
                a = Random.Range(0, rangerooms.Count);
            }
            if (a == 1)
            {
                Instantiate(rangerooms[a], go.transform).SetActive(false);
                isCreateBoos2 = true;
            }


            while (a == 2 && isCreateCaster == true)
            {
                a = Random.Range(0, rangerooms.Count);
            }
            if (a == 2)
            {
                Instantiate(rangerooms[a], go.transform).SetActive(false);
                isCreateCaster = true;
            }


            while (a == 3 && isCreateStore == true)
            {
                a = Random.Range(0, rangerooms.Count);
            }
            if (a == 3)
            {
                Instantiate(rangerooms[a], go.transform).SetActive(false);
                isCreateStore = true;
            }
            Instantiate(rangerooms[0], go.transform).SetActive(false);
        }

        var fristgo = rooms[Random.Range(0, 18)].GetComponent<Image>();
        fristgo.raycastTarget = true;
        fristgo.color = Color.gray;
    }
	

	void Update ()
    {
		
	}
}
