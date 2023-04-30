using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class StartMenu : MonoBehaviour
{
    public GameObject signBox;
    public Text nameText;
    public TMP_Text signBoxText;
    public void StartGame()
    {

        signBox.SetActive(true);
       
      /*  if (Input.GetKeyDown(KeyCode.K))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }*/
    }
    private void Update()
    {
    
        if (Input.GetKeyDown(KeyCode.K))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    public void Quitgame()
    {
        Application.Quit();
    }
}
