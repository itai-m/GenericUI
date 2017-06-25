using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : Singleton<LevelSelector> {
    protected LevelSelector() { }

    private enum Side {Left, Right };
    private int currentWorldLevel;
    private bool isPortrait;

    public int maxLevelInRow = 3;
    public int maxLevelInCol = 5;

    public float offsetHight = 15;
    public float offsetWide = 15;

    public float buttonSpaceRelHight = 0.8F;
    public float buttonSpaceRelWide = 0.8F;

    public GameObject levelButton;
    public GameObject moveWorldLeftButton;
    public GameObject moveWorldRightButton;
    public GameObject goBackButton;
    public Canvas levelsCanvas;
    public Text titleText;


    private List<LevelWorld> allWorld;

	// Use this for initialization
	void Start () {
        isPortrait = Screen.orientation == ScreenOrientation.Portrait;
        currentWorldLevel = 0;
        allWorld = UIManager.Instance.GetWorldLevel();
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
            setRectTransform(button.GetComponent<RectTransform>(), new Vector2(levelsCanvasTransform.rect.width - offsetWide, -midPanelHight), new Vector2(offsetWide, offsetWide));
            
        }
        
    }

    private void buildWorldWithSideButton(int levelNumber) {
        cleanCanvas();
        BuildWorld(allWorld[levelNumber]);
        if (levelNumber < allWorld.Count -1) {
            placingSideButton(Side.Right);
        }
        if (levelNumber > 0) {
            placingSideButton(Side.Left);
        }
        AddGoBackButton();
    }
    
    private void AddGoBackButton() {
        GameObject title = GameObjectUtil.Instantiate(goBackButton.gameObject, Vector2.zero, levelsCanvas.transform);
        setRectTransform(title.GetComponent<RectTransform>(), Vector2.zero, new Vector2(0, offsetHight));
    }

    private void setTitleText(string text, float size) {
        GameObject title = GameObjectUtil.Instantiate(titleText.gameObject, Vector2.zero, levelsCanvas.transform);
        title.GetComponent<Text>().text = text;
        setRectTransform(title.GetComponent<RectTransform>(), Vector2.zero, new Vector2(0, offsetHight));
        //title.GetComponent<RectTransform>().sizeDelta = new Vector2(0, offsetHight);
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
        setTitleText(levelWolrld.worldName, 0);
        for (int i = 0; i < maxLevel; i++) {
            PlaceLevelButton(
                new Vector2(
                    offsetWide + ((i % maxLevelInRow) * (offsetBetweenButtonWide + buttonWide)), 
                    -1 * (offsetHight + (Mathf.Floor( i / maxLevelInRow) * (offsetBetweenButtonHight + buttonHight)))), 
                    buttonSize, levelWolrld, i);
        }
        //Set the UI of the level selection
    }

    private void PlaceLevelButton(Vector2 pos, Vector2 size,LevelWorld levelWorld, int levelnumber) {
        GameObject button = GameObjectUtil.Instantiate(levelButton, pos, levelsCanvas.transform);
        setRectTransform(button.GetComponent<RectTransform>(), pos, size);

        if (UIManager.Instance.isLevelCompleted(levelWorld, levelnumber)) {
            button.GetComponentInChildren<Text>().text = levelnumber.ToString();
            button.transform.GetChild(1).gameObject.SetActive(false);
        } else if (UIManager.Instance.isLevelOpen(levelWorld, levelnumber)) {
            button.GetComponentInChildren<Text>().text = levelnumber.ToString();
            button.transform.GetChild(1).gameObject.SetActive(false);
        }
        else {
            button.GetComponentInChildren<Text>().text = "X";
        }

        Button b = button.GetComponent<Button>();
        b.onClick.AddListener(() => clickLevel(levelWorld, levelnumber));
    }

    private void setRectTransform(RectTransform rect, Vector2 pos, Vector2 size) {
        rect.position = Vector3.zero;
        rect.anchoredPosition = pos;
        rect.sizeDelta = size;
    }

    public void clickLevel(LevelWorld world, int level) {
        UIManager.Instance.Startlevel(world, level);
    }

    public void nextWorld() {
        Debug.Log("nextWorld");
        buildWorldWithSideButton(++currentWorldLevel);
    }

    public void previsWorld() {
        Debug.Log("previsWorld");
        buildWorldWithSideButton(--currentWorldLevel);
    }

    void Update() {
        if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft) {
            Debug.Log("LandscapeLeft");
        }
        else if (Input.deviceOrientation == DeviceOrientation.Portrait) {
            Debug.Log("Portrait");
        }
        if (!isPortrait && Screen.orientation == ScreenOrientation.Portrait) {
            Debug.Log("Portrait");
        }
        else if (isPortrait && Screen.orientation == ScreenOrientation.Landscape) {
            Debug.Log("LandscapeLeft");
        }
    }

    private void cleanCanvas() {
        foreach (Transform child in levelsCanvas.transform) {
            GameObjectUtil.Destroy(child.gameObject);
        }
    }

}
