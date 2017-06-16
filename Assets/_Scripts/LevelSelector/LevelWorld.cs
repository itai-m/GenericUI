using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelWorld {

    private int worldIndex;
    private static int nextIndex = 0;

    public string worldName;
    public int levelCount;

    public LevelWorld(string worldName, int levelCount) {
        worldIndex = getGlobalIndex();
        this.worldName = worldName;
        this.levelCount = levelCount;
    }

    public int WorldIndex {
        get {
            return worldIndex;
        }
    }

    private static int getGlobalIndex() {
        return(nextIndex++);
    }

    public string toString() {
        return worldName;
    }
}
