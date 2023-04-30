using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : MonoBehaviour
{
    private AnimationScript anim;
    private Movement move;
    public GameObject Player;
    public Vector2 respawn_p;
    public bool isDead;
    public float timer;
    public float respawn_h;
    [SerializeField] private AudioSource hurtSoundEffect;
    AudioSource hurtreally;
    // Start is called before the first frame update
    void Start()
    {
        hurtreally = GetComponent<AudioSource>();
        anim = GetComponentInChildren<AnimationScript>();
        respawn_p = Player.transform.position;
       // move =GetComponent<Movement>();
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead == true && (!hurtreally.isPlaying))
          
      /*  if (isDead == false)
            hurtSoundEffect.Stop();*/

        if (isDead)
        {
                //move.canMove = false;
                hurtSoundEffect.Play();
                //anim.SetTrigger("toDie");
                timer += Time.deltaTime;
            if(timer >= 0.5f)
            {
                    hurtSoundEffect.Stop();
                    Deading();
                timer = 0f;
            }
        }
    }
    void Deading()
    {
        Player.transform.position = respawn_p + new Vector2(0, respawn_h);
        Debug.Log(isDead);
        isDead = false;
        Debug.Log(isDead);
    }
}
