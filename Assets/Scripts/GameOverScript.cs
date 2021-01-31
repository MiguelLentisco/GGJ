using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    [SerializeField]
    GameObject GameOverUI;
    [SerializeField]
    GameObject CreditsUI;

    bool finalizar = false;

    private void Start()
    {
        GameOverUI.SetActive(true);
        CreditsUI.SetActive(false);
    }

    void Update()
    {
    }


    public IEnumerator showCredits()
    {
        yield return new WaitForSeconds(4.0f);
        GameOverUI.SetActive(false);
        CreditsUI.SetActive(true);
        yield return new WaitForSeconds(6.0f);
        GameOverUI.SetActive(false);
        CreditsUI.SetActive(false);
        Application.Quit();
        finalizar = true;
    }
}
