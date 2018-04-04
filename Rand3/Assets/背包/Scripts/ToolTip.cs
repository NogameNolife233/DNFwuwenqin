using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ToolTip : MonoBehaviour
{
    private Text toolTipText;
    private Text contentText;
    private CanvasGroup canvasGroup;
    private float Alpha;

	void Start ()
    {
        toolTipText = GetComponent<Text>();
        contentText = transform.Find("Content").GetComponent<Text>();
        canvasGroup = GetComponent<CanvasGroup>();
	}
	

	void Update ()
    {
        
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit, 2000f))
        //{
        //    Vector3 FinalPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        //    transform.position = FinalPos;
        //}
    }

    public void Show(string text)
    {
        toolTipText.text = text;
        contentText.text = text;
        canvasGroup.alpha = 1;
    }

    public void Hide()
    {
        canvasGroup.alpha = 0;
    }

    public void SetLocalPostion(Vector3 postion)
    {
        transform.localPosition = postion;
        //transform.localPosition = Input.mousePosition;

    }


}
