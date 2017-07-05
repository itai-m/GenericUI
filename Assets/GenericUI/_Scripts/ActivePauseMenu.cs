using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePauseMenu : MonoBehaviour {

    public GameObject pausePanel;

    public void setActivePanel(bool active) {
        pausePanel.SetActive(!pausePanel.activeSelf);
    }
}
