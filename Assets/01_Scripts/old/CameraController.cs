using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("追従対象物")]
    public GameObject targetObj;

    // 追従する対象の位置情報を格納用
    Vector3 targetPos;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        //  取得したオブジェクトのtransform.positionを取得
        targetPos = targetObj.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetObj == null)
        {
            return;
        }

        //  targetの移動量分、自分（カメラ）も移動する
        transform.position += targetObj.transform.position - targetPos;
        targetPos = targetObj.transform.position;
    }
}
