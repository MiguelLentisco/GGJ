using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    string actualLevel;

    [SerializeField]
    bool canPause = false;
    [SerializeField]
    bool canHUD = false;
    [SerializeField]
    Animator transition;
    [SerializeField]
    GameObject pauseMenuUI;
    [SerializeField]
    AudioMixer audioMixer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

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
        audioMixer.SetFloat("MainVolume", pVolume);
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

    public void setCanHUD(bool pausable)
    {
        canHUD = pausable;
    }

    IEnumerator loadTransitionAnimation()
    {
        // Cargar animación
        transition.SetTrigger("Start");

        // Esperar un poco
        yield return new WaitForSeconds(1);
    }
}
