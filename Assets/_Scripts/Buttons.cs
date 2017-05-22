using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {
    public string levelSelectName = "LevelSelector";
    public string firstLevelName = "level001";

    public void StartGame() {
        //if (state exist)
        //LoadGame();
        //else
        SceneManager.LoadScene(firstLevelName, LoadSceneMode.Additive);
    }

    public void LoadGame() {
        //load from saved state
    }

    public void LevelSelect() {
        SceneManager.LoadScene(levelSelectName, LoadSceneMode.Single);
    }

}
