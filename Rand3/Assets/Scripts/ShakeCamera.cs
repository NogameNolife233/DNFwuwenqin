using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    //是否开始抖动
    public static bool IsStartShaking = false;
    //抖动时间
    public static float ShakeTime = 0f;
    //抖动系数
    public static float ShakeAmount = 0f;
    //玩家的Transform
    public Transform PlayerTransform;
    //距离差
    private Vector3 offset;
    private void Start()
    {
        offset = transform.position - PlayerTransform.position;
    }
    private void Update()
    {
        transform.position = PlayerTransform.position + offset;
        if (IsStartShaking)
        {
            transform.position = PlayerTransform.position + offset + Random.insideUnitSphere * ShakeAmount;
            StartCoroutine(WaitForSeconds(ShakeTime));
        }
    }
    IEnumerator  WaitForSeconds(float a)
    {
        yield return new WaitForSeconds(a);
        IsStartShaking = false;
        
    }
    //外部需要调用相机抖动时直接使用该方法
    public void ShakeFor(float a, float b)
    {
        ShakeTime = a;
        ShakeAmount = b;
        IsStartShaking = true;

    }
}
