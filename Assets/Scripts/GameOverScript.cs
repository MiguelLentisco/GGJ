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
        if (!finalizar && Input.GetMouseButtonDown(0))
        {
            GameOverUI.SetActive(false);
            CreditsUI.SetActive(true);
            StartCoroutine(waitThenFinalizar());
        }

        if (finalizar && Input.GetMouseButtonDown(0))
        {
            GameOverUI.SetActive(false);
            CreditsUI.SetActive(false);
            Application.Quit();
        }
    }

    IEnumerator waitThenFinalizar()
    {
        yield return new WaitForSeconds(2);
        finalizar = true;
    }
}
