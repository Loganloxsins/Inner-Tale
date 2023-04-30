using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class FinalSignerStay : MonoBehaviour
{
    public GameObject signBox;
    public GameObject signBoxStay;
    public GameObject Player_hp, Signer_hp;
    public Text nameText;
    public TMP_Text signBoxText;
    [TextArea(1, 3)]
    public string[] texts;
    private bool isInsign;
    public AudioSource goalBgm;
    public float goalVolume;
    public AudioSource nowbgm;

    private int count = 0;
    private int flag;
    private bool pressK = false;
    [SerializeField] private AudioSource catSoundEffect;
    private GameObject currentObject;
    // Start is called before the first frame update
    void Start()
    {
        signBox.SetActive(false);
        flag = 0;


    }

    void Update()
    {

        if (!isInsign)
        {
            signBox.SetActive(false);
            Player_hp.SetActive(false);
            Signer_hp.SetActive(false);
        }
        if (isInsign && !pressK)
        {
            string real_text = "回家？";

            signBoxText.text = real_text;
            Debug.Log("1");
            signBox.SetActive(true);
            Debug.Log("2");
            catSoundEffect.Play();
            Debug.Log(real_text);

        }
        Debug.Log("3");
        Debug.Log(isInsign);
        if (isInsign && Input.GetKeyDown(KeyCode.K))
        {
            /*  Debug.Log("4");
              if (count == 0)
              {
                  pressK = true;
              }
              else {
                  pressK = false;
              }*/
            //Debug.Log(count);
            pressK = true;
            if (count < texts.Length)
            {
                Debug.Log("5");
                // Debug.Log(count);
                FindObjectOfType<Movement>().isTalking = true;
                Debug.Log("istalking=" + FindObjectOfType<Movement>().isTalking);
                signBox.SetActive(true);
                String text = texts[count];

                if (text[0] == 'a')
                {

                    string real_text = text.Substring(2);
                    Debug.Log(real_text);
                    Player_hp.SetActive(true);
                    Signer_hp.SetActive(false);
                    signBoxText.text = real_text;
                }
                if (text[0] == 'b')
                {
                    string real_text = text.Substring(2);
                    Debug.Log(real_text);
                    Player_hp.SetActive(false);
                    Signer_hp.SetActive(true);
                    signBoxText.text = real_text;
                }
                count++;
            }
            else
            {
                //FindObjectOfType<Movement>().isTalking = false;
                signBox.SetActive(false);

                Player_hp.SetActive(false);
                Signer_hp.SetActive(false);
                count = texts.Length - 1;
                flag = 1;
                //this.gameObject.SetActive(false);
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                nowbgm.Stop();
                goalBgm.Play();
               // FindObjectOfType<Movement>().isTalking = false;
                signBoxStay.SetActive(true);
            }


        }


    }
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            //catSoundEffect.Play();
            Debug.Log("Enter2D");
            isInsign = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("out");
        if (other.gameObject.CompareTag("Player"))
        {
            isInsign = false;
        }
    }
}


//stay和leave纯粹反了，从现在起，stay的含义为离开