using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource gamesound;
    public AudioSource BgMusic;
    public AudioClip playerJumpSound;
    public AudioClip playerFallSound;
    public AudioClip ButtonClickSound;


    // Start is called before the first frame update
    private void Start()
    {
       
        
    }
    private void Awake()
    {
        instance = this;
       /* if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
            return;
        }*/
    }
    public void bgsound()
    {

    }
    public void playsound(AudioClip sound)
    {
        gamesound.PlayOneShot(sound);
    }

    public void buttonsound()
    {
        playsound(ButtonClickSound);
    }
    public void fallingsound()
    {
        playsound(playerFallSound);
    }
    public void jumpsound()
    {
        playsound(playerJumpSound);
    }

}
