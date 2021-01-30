using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraWork : MonoBehaviour
{
    [SerializeField]
    Transform menuMain;
    [SerializeField]
    Transform menuPosition;
    [SerializeField]
    float transitionSpeed;
    [SerializeField]
    Canvas menuCanvas;
    [SerializeField]
    Canvas optionCanvas;

    // 0 == no change, 1 == goToOptions, 2 == returnToMainMenu
    int states = 0;

    // Start is called before the first frame update
    void Start()
    {
        optionCanvas.gameObject.SetActive(false);
        menuCanvas.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        if (states != 0) { 
            if (states == 1)
            {
                transform.position = Vector3.Lerp(transform.position, menuPosition.position, Time.deltaTime * transitionSpeed);
                transform.rotation = Quaternion.Lerp(transform.rotation, menuPosition.rotation, Time.deltaTime * transitionSpeed);
            } else
            {
                transform.position = Vector3.Lerp(transform.position, menuMain.position, Time.deltaTime * transitionSpeed);
                transform.rotation = Quaternion.Lerp(transform.rotation, menuMain.rotation, Time.deltaTime * transitionSpeed);
            }
        }
    }

    public void goToOptions()
    {
        states = 1;
        menuCanvas.gameObject.SetActive(false);
        optionCanvas.gameObject.SetActive(true);
    }

    public void returnToMenu()
    {
        states = 2;
        menuCanvas.gameObject.SetActive(true);
        optionCanvas.gameObject.SetActive(false);
    }
}
