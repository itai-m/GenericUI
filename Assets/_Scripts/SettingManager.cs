using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour {

    public Slider volumeSlider;

    // Use this for initialization
    void Start () {
        LoadSettingToScreen(UIManager.Instance.Settings);

    }

    private void LoadSettingToScreen(Setting setting) {
        volumeSlider.value = setting.volume;
    }

    public void Save() {
        Setting setting = new Setting();
        setting.volume = volumeSlider.value;

        UIManager.Instance.Settings = setting;
        UIManager.Instance.LoadMainMenu();
    }

}
