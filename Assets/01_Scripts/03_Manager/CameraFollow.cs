using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    GameObject targetObj;
    Vector3 targetPos;
    public float mouseSensitivity;

    void Start()
    {
        targetObj = GameObject.Find("Unitychan");
        targetPos = targetObj.transform.position;
    }

    void LateUpdate()
    {
        // 追従カメラ
        transform.position += targetObj.transform.position - targetPos;
        targetPos = targetObj.transform.position;

        // マウスの移動量
        float mouseInputX = Input.GetAxis("Mouse X");
        float mouseInputY = Input.GetAxis("Mouse Y");

        // targetの位置のY軸を中心に、回転（公転）する
        transform.RotateAround(targetPos, Vector3.up, mouseInputX * Time.deltaTime * mouseSensitivity);

        // カメラの垂直移動
        //transform.RotateAround(targetPos, transform.right, mouseInputY * Time.deltaTime * 200f);
       
    }
}
