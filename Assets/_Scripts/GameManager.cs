using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class GameManager : Singleton<GameManager> {
    protected GameManager() { }

    private const string levelSelectorName = "LevelSelector";
    private const string MainMenuName = "";

    public void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }

    public abstract void Startlevel(int level);

    public abstract List<LevelWorld> GetWorldLevel();

    public bool isOneLevelGame = false;

    public void LoadLevelSelector() {
        if (isOneLevelGame) {
            Startlevel(0);
        }
        SceneManager.LoadScene(levelSelectorName);
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene(MainMenuName);
    }

   

}
