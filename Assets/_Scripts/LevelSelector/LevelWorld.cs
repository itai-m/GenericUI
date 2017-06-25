using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelWorld {

    private string worldKey;

    public string worldName;
    public int levelCount;

    /// <summary>
    /// In this constractor the name of the level need to be uniq,
    /// else need to add to each cretion of the instans a uniqKey.
    /// </summary>
    /// <param name="worldName"></param>
    /// <param name="levelCount"></param>
    /// <param name="uniqKey"></param>
    public LevelWorld(string worldName, int levelCount, string uniqKey = "") {
        this.worldName = worldName;
        this.levelCount = levelCount;
        if (uniqKey.Equals("")) {
            worldKey = worldName + levelCount;
        } else {
            this.worldKey = uniqKey;
        }
    }

    public string WorldKey {
        get {
            return worldKey;
        }
    }

    public string toString() {
        return worldName;
    }

    public string getKey() {
        return worldKey;
    }
}
