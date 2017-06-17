using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestGameMangert : MonoBehaviour , LevelData  {

    public List<LevelWorld> GetWorldLevel() {
        List<LevelWorld> allLevels = new List<LevelWorld>();
        allLevels.Add(new LevelWorld("test", 5));
        allLevels.Add(new LevelWorld("test2", 10));
        allLevels.Add(new LevelWorld("test3", 12));
        return allLevels;
    }

    public void Startlevel(LevelWorld world, int level) {
        Debug.Log("Start World: " + world + ", Level: " + level);
        SceneManager.LoadScene("TestScene");
    }

}
