using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserSettings : MonoBehaviour {

    public Slider VolumeSlider;

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this);
    }

    public void AdjustVolume()
    {
        AudioListener.volume = VolumeSlider.value;
    }
}
