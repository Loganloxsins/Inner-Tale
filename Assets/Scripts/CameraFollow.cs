using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; //跟随的对象
    public float smoothing;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    void LateUpdate()
    {
        if(target!= null){
            if(transform.position!= target.position){
                Vector3 targetPos = target.position;
                transform.position = Vector3.Lerp(transform.position,targetPos,smoothing);
            }
        }
    }
}
