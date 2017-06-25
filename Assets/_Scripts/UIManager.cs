using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager> {
    protected UIManager() { }

    private const string levelSelectorName = "LevelSelector";
    private const string mainMenuName = "Main";
    private const string settinguName = "Setting";

    private bool isPause;
    private Setting setting;
    private float timeScale;
    private Scene gameScene;
    public SavedProgression savedProgression;

    private LevelWorld currentWorld;
    private int currentLevel;

    public LevelData levelCretion;

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

    void Awake() {
        if (alreadyCreateInstance()) {
            Destroy(gameObject);
        }
        else {
            initUIManager();
            DontDestroyOnLoad(gameObject);
        }
    }

    private void initUIManager() {
        LoadSetting();
        savedProgression = GetComponent<SavedProgression>();
        loadSave();
        levelCretion = GetComponent<LevelData>();
        if (levelCretion == null) {
            Debug.Log("You must add implemtion of LevelData to ");
        }
    }

    private void loadSave() {
        savedProgression.load();
    } 

    public void Startlevel(LevelWorld world, int level) {
        levelCretion.Startlevel(world, level);
        currentLevel = level;
        currentWorld = world;
    }

    public List<LevelWorld> GetWorldLevel() {
        return levelCretion.GetWorldLevel();
    }

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

        LoadMainMenu(PauseGame());
    }

    public void ResumeGameAndReturnToGameScreen() {
        isPause = false;
        if (ResuemGame()) {
            SceneManager.LoadScene(gameScene.name);
        }
    }


    public bool PauseGame() {
        if (levelCretion.PauseGame()) {
            return false;
        }
        timeScale = Time.timeScale;
        Time.timeScale = 0;
        return true;
    }

    public bool ResuemGame() {
        if (levelCretion.ResuemGame()) {
            return false;
        }
        Time.timeScale = timeScale;
        return true;
    }

    public void LoadSetting() {
        setting = new Setting();
    }

    public void levelComplete(LevelWorld world, int level) {
        savedProgression.LevelCompleted(world, level);
        savedProgression.Save();
    }

    public void CurrentLevelCompleted() {
        savedProgression.LevelCompleted(currentWorld, currentLevel);
        savedProgression.Save();
    }

    public bool isLevelCompleted(LevelWorld world, int level) {
        return savedProgression.IsLevelCompleted(world, level);
    }

    public bool isLevelOpen(LevelWorld world, int level) {
        return levelCretion.isLevelOpen(world, level);
    }

}
