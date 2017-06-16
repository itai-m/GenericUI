using UnityEngine;

public class Buttons : MonoBehaviour {

    public void LoadLevelSelector() {
        UIManager.Instance.LoadLevelSelector();
    }

    public void LoadSettingScreen() {
        UIManager.Instance.LoadSettingScreen();
    }

    public void LoadMainScreen() {
        UIManager.Instance.LoadMainMenu();
    }

    public void PauseAndGoMenu() {
        UIManager.Instance.PauseGameAndReturnToMain();
    }

    public void ResumeAndGoGame() {
        UIManager.Instance.ResumeGameAndReturnToGameScreen();
    }

    public void LevelSelect() {
        
    }

    public void nextWorld() {
        LevelSelector.Instance.nextWorld();
    }

    public void prevWorld() {
        LevelSelector.Instance.previsWorld();
    }

}
