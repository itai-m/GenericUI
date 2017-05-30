using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : Singleton<LevelSelector> {
    protected LevelSelector() { }

    private enum Side {Left, Right };
    private int currentWorldLevel;

    public int maxLevelInRow = 5;
    public int maxLevelInCol = 3;

    public float offsetHight = 15;
    public float offsetWide = 15;

    public float buttonSpaceRelHight = 0.8F;
    public float buttonSpaceRelWide = 0.8F;

    public GameObject levelButton;
    public GameObject moveWorldLeftButton;
    public GameObject moveWorldRightButton;
    public Canvas levelsCanvas;

    private List<LevelWorld> allWorld;

	// Use this for initialization
	void Start () {
        currentWorldLevel = 0;
        allWorld = GameManager.Instance.GetWorldLevel();
        if (allWorld.Count > 0) {
            buildWorldWithSideButton(0);
        } else {
            ///ERROR: must be one world level to load the level selctor
        }
    }

    private void placingSideButton(Side side) {
        RectTransform levelsCanvasTransform = levelsCanvas.GetComponent<RectTransform>();
        float midPanelHight = levelsCanvasTransform.rect.height / 2;
        if (side == Side.Left) {
            GameObject button = GameObjectUtil.Instantiate(moveWorldLeftButton, new Vector2(), levelsCanvas.transform);
            setRectTransform(button.GetComponent<RectTransform>(), new Vector2(0, -midPanelHight), new Vector2(offsetWide, offsetWide));
        } else {
            GameObject button = GameObjectUtil.Instantiate(moveWorldRightButton, new Vector2(), levelsCanvas.transform);
            setRectTransform(button.GetComponent<RectTransform>(), new Vector2(levelsCanvasTransform.rect.width, -midPanelHight), new Vector2(offsetWide, offsetWide));
            
        }
        
    }

    private void buildWorldWithSideButton(int levelNumber) {
        BuildWorld(allWorld[levelNumber]);
        if (levelNumber < allWorld.Count) {
            placingSideButton(Side.Right);
        }
        if (levelNumber > 0) {
            placingSideButton(Side.Left);
        }
        
    }

    private void BuildWorld(LevelWorld levelWolrld) {
        int maxLevel =levelWolrld.levelCount;
        RectTransform levelsCanvasTransform = levelsCanvas.GetComponent<RectTransform>();
        float newPanelHight = levelsCanvasTransform.rect.height - (offsetHight * 2);
        float buttonHight = newPanelHight * buttonSpaceRelHight / maxLevelInCol;
        float offsetBetweenButtonHight = (newPanelHight - (buttonHight * maxLevelInCol)) / (maxLevelInCol - 1);
        float newPanelWidth = levelsCanvasTransform.rect.width - (offsetWide * 2);
        float buttonWide = newPanelWidth * buttonSpaceRelWide / maxLevelInRow;
        float offsetBetweenButtonWide = (newPanelWidth - (buttonWide * maxLevelInRow)) / (maxLevelInRow - 1);
        Vector2 buttonSize = new Vector2(buttonWide, buttonHight);
        for (int i = 0; i < maxLevel; i++) {
            PlaceLevelButton(
                new Vector2(
                    offsetWide + ((i % maxLevelInRow) * (offsetBetweenButtonWide + buttonWide)), 
                    -1 * (offsetHight + (Mathf.Floor( i / maxLevelInRow) * (offsetBetweenButtonHight + buttonHight)))), 
                    buttonSize, i);
        }
        //Set the UI of the level selection
    }

    private void PlaceLevelButton(Vector2 pos, Vector2 size, int number) {
        GameObject button = GameObjectUtil.Instantiate(levelButton, pos, levelsCanvas.transform);
        setRectTransform(button.GetComponent<RectTransform>(), pos, size);
        button.GetComponentInChildren<Text>().text = number.ToString();
    }

    private void setRectTransform(RectTransform rect, Vector2 pos, Vector2 size) {
        rect.position = Vector3.zero;
        rect.anchoredPosition = pos;
        rect.sizeDelta = size;
    }

    public void clickLevel(int level) {
        GameManager.Instance.Startlevel(level);
    }

    public void nextWorld() {
        BuildWorld(allWorld[++currentWorldLevel]);
    }

    public void previsWorld() {
        BuildWorld(allWorld[--currentWorldLevel]);
    }

    void Update() {
        if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft) {
            Debug.Log("LandscapeLeft");
        }
        else if (Input.deviceOrientation == DeviceOrientation.Portrait) {
            Debug.Log("Portrait");
        }
    }

}
