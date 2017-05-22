using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelWorld {

    public string worldName;
    public int levelCount;

    public LevelWorld(string worldName, int levelCount) {
        this.worldName = worldName;
        this.levelCount = levelCount;
    }
}
