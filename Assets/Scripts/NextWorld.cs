using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NextWorld : MonoBehaviour
{
    public GameObject signBox;
    public GameObject Player_hp, Signer_hp;
    public Text nameText;
    public TMP_Text signBoxText;
    [TextArea(1, 3)]
    public string[] texts;
    private bool isInsign;


    private int count = 0;
    private int flag;
    // Start is called before the first frame update
    void Start()
    {
        signBox.SetActive(false);
        flag = 0;
        //dialogBoxText.text = dialogText;

    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(FindObjectOfType<Movement>().isTalking);
        if (!isInsign)
        {
            Player_hp.SetActive(false);
            Signer_hp.SetActive(false);
        }

        if (isInsign && Input.GetKeyDown(KeyCode.K))
        {

            if (count < texts.Length)
            {
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
                this.gameObject.SetActive(false);
               
                FindObjectOfType<Movement>().isTalking = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }


        }


    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInsign = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInsign = false;
        }
    }
}
