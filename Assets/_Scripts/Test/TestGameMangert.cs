using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestGameMangert : GameManager {

    public override List<LevelWorld> GetWorldLevel() {
        List<LevelWorld> allLevels = new List<LevelWorld>();
        allLevels.Add(new LevelWorld("test", 5));
        allLevels.Add(new LevelWorld("test2", 10));
        return allLevels;
    }

    public override void Startlevel(int level) {
        Debug.Log("Start Level: " + level);
    }

}
