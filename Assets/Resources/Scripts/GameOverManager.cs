using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    
    public void GameStart () {
        Game.qtdVidas = 5;
        Game.qtdGems = 0;
        Game.qtdCherries = 0;
        SceneManagerScript.sceneManagerInstance = null;
        SceneManager.LoadScene("Fase_01");
    }

    public void GameQuit () {
        Application.Quit();
    }

}
