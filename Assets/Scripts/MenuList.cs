using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuList : MonoBehaviour
{
    // Start is called before the first frame update


    public GameObject menuList;

    [SerializeField] private bool menuKey = true;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (menuKey)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                menuList.SetActive(true);
                menuKey = false;
                Time.timeScale = (0);
            }
        }
        else if (!menuKey)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                menuList.SetActive(false);
                menuKey = true;
                Time.timeScale = (1);
            }
        }


    }

    public void Return()
    {
        menuList.SetActive(false);
        menuKey = true;
        Time.timeScale = (1);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void Exit()
    {
        Application.Quit();
    }

}
