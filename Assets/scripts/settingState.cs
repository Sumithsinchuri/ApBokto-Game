using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class settingState : MonoBehaviour
{

    public Slider Soundvolume;
    public GameObject audiosource;
    public GameObject settingUi;
    public Slider MusicVolume;
    public GameObject musicAudioSource;
    public static float SoundsliderVolume = 0.5f;
    public static float MusicSliderValue = 0.5f;

    public Sprite vibration;
    public Sprite vibrationOff;
    public GameObject vibrationButton;
    public static bool isVibrating = true;
    public static int vibrationState;



    // Start is called before the first frame update
    void Start()
    {

        Soundvolume.value = SoundsliderVolume;
         MusicVolume.value = MusicSliderValue;
        // audiosource.GetComponent<AudioSource>().volume = Soundvolume.value;
        //musicAudioSource.GetComponent<AudioSource>().volume = MusicVolume.value;
        if (vibrationState == 0)
        {
            vibrationButton.GetComponent<Image>().sprite = vibration;
        }
        else
        {
            vibrationButton.GetComponent<Image>().sprite = vibrationOff;
        }

    }

    // Update is called once per frame
    void Update()
    {
        audiosource.GetComponent<AudioSource>().volume = Soundvolume.value;
        musicAudioSource.GetComponent<AudioSource>().volume = MusicVolume.value;
        //Soundvolume.value = 0.5f;
        //MusicVolume.value = 0.5f;
    }
    public void volumechange()
    {
        //if (AudioManager.instance.gamesound.volume < 0f)
        
            audiosource.GetComponent<AudioSource>().volume = Soundvolume.value;
            SoundsliderVolume = Soundvolume.value;
        //  PlayerPrefs.SetFloat("SoundSliderValue",Soundvolume.value);
        
    }
    public void BGVolumeChange()
    {
       // AudioManager.instance.BgMusic.volume = MusicVolume.value

            musicAudioSource.GetComponent<AudioSource>().volume = MusicVolume.value;
            MusicSliderValue = MusicVolume.value;
        
    }
    public void back()
    {
        AudioManager.instance.buttonsound();
        settingUi.SetActive(false);
    }
    public void vibrationImage()
    {
        AudioManager.instance.buttonsound();
        if (vibrationButton.GetComponent<Image>().sprite == vibration)
        {
            vibrationState = 1;
            isVibrating = false;
            vibrationButton.GetComponent<Image>().sprite = vibrationOff;

        }
        else
        {
            vibrationState = 0;
            isVibrating = true;
            vibrationButton.GetComponent<Image>().sprite = vibration;
        }
    }
}
