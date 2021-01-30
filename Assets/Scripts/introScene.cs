using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introScene : MonoBehaviour
{
    void Start()
    {
        LevelManager LM = LevelManager.instance;
        LM.setCanHUD(false);
        LM.setCanPause(false);
        LM.loadLevel(LM.levelDictionary["Scene1"]);
    }
}
