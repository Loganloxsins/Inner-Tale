using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    public Transform cameraTransform;
    public float speedx;
    public float speedy;
    private float startPositionX;
    private float startPositionY;

    private float offsetx;
    private float offsety;
    // Start is called before the first frame update
    void Start()
    {
        startPositionX = transform.position.x;
        startPositionY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        offsetx = cameraTransform.position.x - startPositionX;
        offsety = cameraTransform.position.y - startPositionY;

        transform.position = new Vector2(startPositionX + offsetx * speedx, startPositionY + offsety * speedy);
    }
}
