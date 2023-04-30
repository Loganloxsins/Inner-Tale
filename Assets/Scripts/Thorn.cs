using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn : MonoBehaviour
{
    //private Dead dead;
    // Start is called before the first frame update
    void Start()
    {
        //dead = FindObjectOfType<Dead>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<Dead>().isDead = true;
        }
       
    }
}
