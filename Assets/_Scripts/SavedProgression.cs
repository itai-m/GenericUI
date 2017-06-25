using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;


public class SavedProgression : MonoBehaviour {

    private string filePath = "";
    public Setting setting;
    private Dictionary<string, bool []> levelComplete;

    private string GetFilePath() {
        if (filePath.CompareTo("") == 0) {
            filePath = Application.persistentDataPath + "/levelProg.dat";
        }
        return filePath;
    }

    public void Save() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.OpenWrite(GetFilePath());

        SaveData data = new SaveData();
        data.setting = this.setting;
        data.levelComplete = this.levelComplete;

        bf.Serialize(file, data);
        file.Close();
    }

    public void load() {
        if (File.Exists(GetFilePath())) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenRead(GetFilePath());
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            this.setting = data.setting;
            this.levelComplete = data.levelComplete;
        } else {
            this.setting = new Setting();
            this.levelComplete = new Dictionary<string, bool[]>();
        }
    }

    public void LevelCompleted(LevelWorld world, int levelNumber){
        if (!levelComplete.ContainsKey(world.getKey())) {
            levelComplete.Add(world.getKey(), new bool[world.levelCount]);
        }
        levelComplete[world.getKey()][levelNumber] = true;
    }

    public bool IsLevelCompleted(LevelWorld world, int levelNumber) {
        if (levelComplete.ContainsKey(world.getKey())) {
            return levelComplete[world.getKey()][levelNumber];
        }
        return false;
    }
}

[Serializable]
class SaveData {
    public Setting setting;
    public Dictionary<string, bool []> levelComplete;
}

[Serializable]
public class Setting {

    public float volume;

    public Setting() {
        volume = 1;
    }
}

