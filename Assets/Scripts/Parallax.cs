using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform cameraTransform;
    public float speedx;
    public float speedy;
    private float startPositionX;
    private float startPositionY;
    // Start is called before the first frame update
    void Start()
    {
        startPositionX = transform.position.x;
        startPositionY= transform.position.y ;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(startPositionX + cameraTransform.position.x * speedx, startPositionY + cameraTransform.position.y * speedy);
    }
}
