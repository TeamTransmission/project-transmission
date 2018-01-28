using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserSettings : MonoBehaviour {

    public Slider VolumeSlider;
    public GameObject pausePanel;
    public GameObject helpPanel;
    public GameObject audioPanel;

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this);
    }

    public void Restartdude()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void AdjustVolume()
    {
        AudioListener.volume = VolumeSlider.value;
    }

    public void Unpause()
    {
        helpPanel.SetActive(false);
        audioPanel.SetActive(false);
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                // Paused
                pausePanel.SetActive(true);
                Time.timeScale = 0;
            }
            else if (Time.timeScale == 0)
            {
                // not paused
                Unpause();
            }
        }
    }
}
