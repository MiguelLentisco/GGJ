using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    [SerializeField]
    GameObject pauseMenuUI;


    bool canPause = true;

    // Update is called once per frame
    void Update()
    {
        if (canPause && Input.GetKeyDown(KeyCode.Escape))
            stopPlaying();
    }

    public void stopPlaying()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        canPause = false;
    }
    public void resumePlaying()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        canPause = true;
    }

    public void quitPlaying()
    {
        Application.Quit();
    }
}
