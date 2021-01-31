using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Dictionary<string, string> levelDictionary = new Dictionary<string, string>();

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
    GameObject gameHUD;
    [SerializeField]
    GameObject tutorialUI;
    [SerializeField]
    AudioMixer audioMixer;
    [SerializeField]
    Text rounds;

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

        initializeDictionary();
    }

    private void Start()
    {
        tutorialUI.SetActive(false);
        rounds.text = "ROUND " + GameManager.instance.GetRounds().ToString();
    }

    private void initializeDictionary()
    {
        levelDictionary.Add("Scene1", "Menu");
        levelDictionary.Add("Scene2", "DefinitiveScene");
    }

    private void Update()
    {
        if (canPause && Input.GetKeyDown(KeyCode.Escape))
            stopPlaying();

        if (tutorialUI.activeSelf && Input.GetMouseButtonDown(0)) { 
            loadLevel("Scene2");
            tutorialUI.SetActive(false);
            GameManager.instance.IniciarJuego();
        }
    }

    public void loadTutorial(string pLevelName)
    {
        tutorialUI.SetActive(true);
    }

    public void loadLevel(string pLevelName)
    {
        StartCoroutine(loadTransitionAnimation(pLevelName));
        //SceneManager.UnloadSceneAsync(actualLevel);
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
        pauseMenuUI.SetActive(pausable);
    }

    public void setCanHUD(bool pausable)
    {
        canHUD = pausable;
        gameHUD.SetActive(pausable);
    }

    IEnumerator loadTransitionAnimation(string pLevelName)
    {
        // Cargar animación
        transition.SetTrigger("Start");

        // Esperar un poco
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(levelDictionary[pLevelName]);
    }

    public void updateRounds()
    {
        rounds.text = "ROUND " + GameManager.instance.GetRounds().ToString();
    }
}
