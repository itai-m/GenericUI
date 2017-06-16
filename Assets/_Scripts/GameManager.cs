using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class UIManager : Singleton<UIManager> {
    protected UIManager() { }

    private const string levelSelectorName = "LevelSelector";
    private const string mainMenuName = "Main";
    private const string settinguName = "Setting";

    private bool isPause;
    private Setting setting;
    private float timeScale;
    private Scene gameScene;

    public LevelCretion levelCretion;

    public Setting Settings {
        get { return setting; }
        set { setting = value; }
    }

    public bool IsPause {
        get {
            return isPause;
        }

        set {
            isPause = value;
        }
    }

    public void Awake() {
        DontDestroyOnLoad(transform.gameObject);
        LoadSetting();
    }

    public abstract void Startlevel(LevelWorld world, int level);

    public abstract List<LevelWorld> GetWorldLevel();

    public bool isOneLevelGame = false;

    public void LoadLevelSelector() {
        if (isOneLevelGame) {
            Startlevel(null, 0);
        }
        else {
            SceneManager.LoadScene(levelSelectorName);
        }
    }

    public void LoadMainMenu(bool isAdditive = false) {
        SceneManager.LoadScene(mainMenuName, isAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single);
    }

   public void LoadSettingScreen() {
        SceneManager.LoadScene(settinguName);
    }

    public void PauseGameAndReturnToMain() {
        isPause = true;
        gameScene = SceneManager.GetActiveScene();
        PauseGame();
        LoadMainMenu(true);
    }

    public void ResumeGameAndReturnToGameScreen() {
        isPause = false;
        ResuemGame();
        SceneManager.LoadScene(gameScene.name);
    }

    protected void PauseGame() {
        foreach (Camera c in Camera.allCameras) {
            Debug.Log(c);
            c.gameObject.SetActive(false);
        }
        

        timeScale = Time.timeScale;
        Time.timeScale = 0;
    }

    protected void ResuemGame() {
        Time.timeScale = timeScale;
    }

    public void LoadSetting() {
        setting = new Setting();
    }

}
