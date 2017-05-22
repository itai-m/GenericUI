using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour {

    private int level;

    public void setLevel(int level) {
        this.level = level;
        this.GetComponentInChildren<Text>().text = level.ToString();
    }

    public void OnClick() {
        LevelSelector.Instance.clickLevel(level);
    }

}
