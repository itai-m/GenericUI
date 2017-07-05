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


    public float buttonSpaceRelHight = 0.8F;
    public float buttonSpaceRelWide = 0.8F;

    public GameObject levelButton;
    public GameObject moveWorldLeftButton;
    public GameObject moveWorldRightButton;
    public GameObject goBackButton;
    public Canvas levelsCanvas;
    public GameObject panel;
    public Text titleText;

    private GridLayoutGroup grid;


    private List<LevelWorld> allWorld;

	// Use this for initialization
	void Start () {
        isPortrait = Screen.orientation == ScreenOrientation.Portrait;
        currentWorldLevel = 0;
        allWorld = UIManager.Instance.GetWorldLevel();
        grid = panel.GetComponent<GridLayoutGroup>();
        if (allWorld.Count > 0) {
            buildWorldWithSideButton(0);
        } else {
            ///ERROR: must be one world level to load the level selctor
        }
    }

    private void buildWorldWithSideButton(int levelNumber) {
        cleanObjectChilren(panel);
        BuildWorld(allWorld[levelNumber]);
        moveWorldRightButton.SetActive(levelNumber < allWorld.Count - 1);
        moveWorldLeftButton.SetActive(levelNumber > 0);
    }

    private void setTitleText(string text, float size) {
        titleText.GetComponent<Text>().text = text;
    }

    private void BuildWorld(LevelWorld levelWolrld) {
        initGridLayout();        
        setTitleText(levelWolrld.worldName, 0);
        for (int i = 0; i < levelWolrld.levelCount ; i++) {
            PlaceLevelButton(levelWolrld, i);
        }
        //Set the UI of the level selection
    }

    private void initGridLayout() {
        RectTransform levelsPanelTransform = panel.GetComponent<RectTransform>();
        float newPanelHight = levelsPanelTransform.rect.height;
        float buttonHight = newPanelHight * buttonSpaceRelHight / maxLevelInCol;
        float offsetBetweenButtonHight = (newPanelHight - (buttonHight * maxLevelInCol)) / (maxLevelInCol - 1);
        float newPanelWidth = levelsPanelTransform.rect.width;
        float buttonWide = newPanelWidth * buttonSpaceRelWide / maxLevelInRow;
        float offsetBetweenButtonWide = (newPanelWidth - (buttonWide * maxLevelInRow)) / (maxLevelInRow - 1);
        grid.spacing = new Vector2(offsetBetweenButtonWide, offsetBetweenButtonHight);
        grid.cellSize = new Vector2(buttonWide, buttonHight);
    }

    private void PlaceLevelButton(LevelWorld levelWorld, int levelnumber) {
        GameObject button = GameObjectUtil.Instantiate(levelButton, Vector2.zero, levelsCanvas.transform);
        Button b = button.GetComponent<Button>();
        button.GetComponentInChildren<Text>().text = levelnumber.ToString();

        if (UIManager.Instance.isLevelCompleted(levelWorld, levelnumber)) {
            button.transform.GetChild(2).gameObject.SetActive(true);
            b.onClick.AddListener(() => clickLevel(levelWorld, levelnumber));
            ///TODO: need to add mark that level is complete
        } else if (!UIManager.Instance.isLevelOpen(levelWorld, levelnumber)) {
            button.transform.GetChild(1).gameObject.SetActive(true);
        } else {
            b.onClick.AddListener(() => clickLevel(levelWorld, levelnumber));
        }

        button.transform.SetParent(panel.transform);
    }

    public void clickLevel(LevelWorld world, int level) {
        UIManager.Instance.Startlevel(world, level);
    }

    public void nextWorld() {
        buildWorldWithSideButton(++currentWorldLevel);
    }

    public void previsWorld() {
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

    private void cleanObjectChilren(GameObject obj) {
        foreach (Transform child in obj.transform) {
            GameObjectUtil.Destroy(child.gameObject);
        }
    }

}
