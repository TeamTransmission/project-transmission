using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class LevelComplete : MonoBehaviour {

    private GameObject canvas;
    private GameObject music;
    private GameObject levelCompleteSound;
     
    // Use this for initialization
    void Start () {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        music = GameObject.FindGameObjectWithTag("Music");
        levelCompleteSound = GameObject.FindGameObjectWithTag("LevelCompleteSound");
    }
		
	public void ActivateLevelComplete ()
    {
        canvas.GetComponentInChildren<Text>().enabled = true;

        music.GetComponent<AudioSource>().Stop();
        levelCompleteSound.GetComponent<AudioSource>().Play();

        StartCoroutine(FadePause("Next", 4.0f));

    }

    IEnumerator FadePause(string SceneToChangeTo, float fadeTime)
    {
        //Debug.Log("Fade Started");
        yield return new WaitForSeconds(fadeTime);
        //Debug.Log("Countdown Done");

        if(SceneManager.sceneCountInBuildSettings> SceneManager.GetActiveScene().buildIndex + 1)      
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            canvas.GetComponentInChildren<Text>().text = "THANKS FOR PLAYING";
            yield return new WaitForSeconds(10.0f);
            SceneManager.LoadScene(0);
        }

    }

}
