using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    string actualNivel;

    [SerializeField]
    Animator transition;

    private void Start()
    {
        // Cargar el primer nivel (el main menu)
        //SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
        // actualNivel = "MainMenu"
    }

    public void cargarNivel(string pNombreNivel)
    {
        StartCoroutine(cargarTransicion());
        //SceneManager.UnloadSceneAsync(actualNivel);
        //SceneManager.LoadScene(pNombreNivel, LoadSceneMode.Additive);
        //actualNivel = pNombreNivel;
    }

    public void salirNivel()
    {
        Application.Quit();
    }

    public void ajustarVolumen(float volumen)
    {
        Debug.Log(volumen);
    }

    public void ajustarCalidad(int calidad)
    {
        QualitySettings.SetQualityLevel(calidad);
    }

    public void ajustarFullScreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }

    IEnumerator cargarTransicion()
    {
        // Cargar animación
        transition.SetTrigger("Start");

        // Esperar un poco
        yield return new WaitForSeconds(1);
    }
}
