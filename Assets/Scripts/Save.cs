using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using UnityEditor.UI;
using DG.Tweening;

public class Save : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text save_text;
    private bool flag = false;
    public float timer;
    void Start()
    {
        save_text.gameObject.SetActive(false);
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * if (save_text.gameObject.activeInHierarchy)
        {
            timer += Time.deltaTime;
            if (timer >= 2f)
            {
                save_text.gameObject.SetActive(false);
                timer = 0f;
            }
        }
         */

    }
    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !flag)
        {
            Vector2 p = this.transform.position; 
            save_text.transform.position = p + new Vector2(0, 10);

            Debug.Log("saved");
            FindObjectOfType<Dead>().respawn_p= other.gameObject.transform.position;
            save_text.gameObject.SetActive(true);
            TMP_Text text= save_text.GetComponent<TMP_Text>();
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
            text.DOFade(1f, 2f);
            //文字停顿1秒
            yield return new WaitForSeconds(1f);
            //文字渐隐
            text.DOFade(0f, 0.5f);
            //渐隐结束后销毁物体
            yield return new WaitForSeconds(0.5f);
            save_text.gameObject.SetActive(false);
            flag = true;
            
        }
    
    }


  


}
