using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    string actualLevel;

    [SerializeField]
    bool canPause = false;
    [SerializeField]
    Animator transition;
    [SerializeField]
    GameObject pauseMenuUI;

    private void Start()
    {
        // Cargar el primer nivel (el main menu)
        //SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
        // actualNivel = "MainMenu"
    }

    private void Update()
    {
        if (canPause && Input.GetKeyDown(KeyCode.Escape))
            stopPlaying();
    }

    public void loadLevel(string pLevelName)
    {
        StartCoroutine(loadTransitionAnimation());
        //SceneManager.UnloadSceneAsync(actualLevel);
        //SceneManager.LoadScene(pLevelName, LoadSceneMode.Additive);
        //actualLevel = pLevelName;
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void manageVolume(float pVolume)
    {
        Debug.Log(pVolume);
    }

    public void manageQuality(int pQuality)
    {
        QualitySettings.SetQualityLevel(pQuality);
    }

    public void manageFullScreen(bool pFullscreen)
    {
        Screen.fullScreen = pFullscreen;
    }

    public void stopPlaying()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }
    public void resumePlaying()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void setCanPause(bool pausable)
    {
        canPause = pausable;
    }

    IEnumerator loadTransitionAnimation()
    {
        // Cargar animación
        transition.SetTrigger("Start");

        // Esperar un poco
        yield return new WaitForSeconds(1);
    }
}
