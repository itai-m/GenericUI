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

    public void PauseGame() {
        UIManager.Instance.PauseGame();
    }

    public void ResumeGame() {
        UIManager.Instance.ResuemGame();
    }

    public void LevelSelect() {
        
    }

    public void nextWorld() {
        LevelSelector.Instance.nextWorld();
    }

    public void prevWorld() {
        LevelSelector.Instance.previsWorld();
    }

    public void OpenPauseMenu() {

    }

}
