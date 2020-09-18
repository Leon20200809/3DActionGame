using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUICamera : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
    }
}
