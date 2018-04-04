using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateChildRoom : MonoBehaviour
{
    public int index;
    public GameObject go;
    public GameObject cube;
    //动态生成房间，获取地下的子房间的显示
	void Start ()
    {

        go = transform.GetChild(0).gameObject;
	}
	
	
	void Update ()
    {
		
	}
    //点击触发周围的房间的显示，触发未点开房间的射线检测
    public void OnClick()
    {
        var parnt = transform.parent.gameObject.GetComponent<CreateRoom>();
        go.gameObject.SetActive(true);

        if (index - 6 >= 0)
        {
            parnt.rooms[index - 6].GetComponent<Image>().raycastTarget = true;
            parnt.rooms[index - 6].GetComponent<Image>().color = Color.gray;
        }
        if (index + 6 <= 17)
        {
            parnt.rooms[index + 6].GetComponent<Image>().raycastTarget = true;
            parnt.rooms[index + 6].GetComponent<Image>().color = Color.gray;

        }
        if (index - 1 >= 0 && index % 6 != 0) 
        {
            parnt.rooms[index - 1].GetComponent<Image>().raycastTarget = true;
            parnt.rooms[index - 1].GetComponent<Image>().color = Color.gray;
        }
        if (index + 1 <= 17 && index % 6 != 5)
        {
            parnt.rooms[index + 1].GetComponent<Image>().raycastTarget = true;
            parnt.rooms[index + 1].GetComponent<Image>().color = Color.gray;
        }
    }
}
