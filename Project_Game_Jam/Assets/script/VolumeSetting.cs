using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class VolumeSetting : MonoBehaviour
{

    public AudioMixer myMixer;
    public Slider musicSlider;
    private void Start()
    {
        SetMusicVolume();
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("music", Mathf.Log10(volume)*20);
    }
    


}
