using System;
using System.Collections.Generic;
using Backend;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using TMPro;
using Sirenix.OdinInspector;
using Random = UnityEngine.Random;


[RequireComponent(typeof(RectTransform))]
public class Graph : MonoBehaviour
{
    [SerializeField] Sprite circleSprite;

    [SerializeField] EnemySettings accuracyAsEnemySettings;


    //  public List<AccuracyPerEnemy> AccuraciesAllEnemies;


    [SerializeField] TextMeshProUGUI xLabelPrefab;
    [SerializeField] TextMeshProUGUI yLabelPrefab;

    [SerializeField] List<TextMeshProUGUI> xLabels;
    [SerializeField] List<TextMeshProUGUI> yLabels;

    [SerializeField] float bufferZoneOnTopRelative = 0.1f;

    //   [SerializeField] int showEveryXLabel = 1;


    [SerializeField] List<GraphPerEnemy> graphsPerEnemy;

    [SerializeField] GameSettings gameSettings;

    [SerializeField] RectTransform graphArea;


    [SerializeField] float lineThickness;
    [SerializeField] float circleRadius;


    float widthBetweenXEntries;
    float widthXperUnit;


    Dictionary<EnemySettings, List<AccuracyEntry>> accuraciesPerEnemy;


    // need to use struct because some enemies won't have entries for some indexes, so can't just loop through list
    [Serializable]
    public struct AccuracyEntry
    {
        public int GameIndex;
        public float Accuracy;

        public AccuracyEntry(int gameIndex, float accuracy)
        {
            GameIndex = gameIndex;
            Accuracy = accuracy;
        }
    }


    void Awake()
    {
        xLabelPrefab.gameObject.SetActive(false);
        yLabelPrefab.gameObject.SetActive(false);
    }


    [Button]
    public void CreateGraphsForWaveGames()
    {
        SaveData saveData = SaveManager.Instance.GetSaveData();
        CreateGraphsForGames(saveData.StatsForWaveGames);
    }


    public EnemySettings TESTEnemySettings;
    [SerializeField] List<StatsSummaryPerGame> testStatsSummaries;

    [Button]
    public void CreateAndShowTestData(int count, float fillRate)
    {
        testStatsSummaries = new List<StatsSummaryPerGame>();

        for (int i = 0; i < count; i++)
        {
            // this loop creates games. Each game does not need an entry for the enemy, that's the whole point
            StatsSummaryPerGame statsSummaryPerGame = new StatsSummaryPerGame();
            statsSummaryPerGame.AccuracyPerEnemy = new List<AccuracyPerEnemy>();
            testStatsSummaries.Add(statsSummaryPerGame);

            if (Random.Range(0f, 1f) < fillRate)
            {
                AccuracyPerEnemy accuracyPerEnemy = new AccuracyPerEnemy();
                accuracyPerEnemy.GUID = TESTEnemySettings.GUID;
                accuracyPerEnemy.EnemySettings = TESTEnemySettings;
                accuracyPerEnemy.Accuracy = Random.Range(0f, 1f);

                statsSummaryPerGame.AccuracyPerEnemy.Add(accuracyPerEnemy);
            }
        }

        CreateGraphsForGames(testStatsSummaries);
    }


    public void CreateGraphsForGames(List<StatsSummaryPerGame> statsSummaries)
    {
        if (statsSummaries.Count == 0)
        {
            Debug.Log("Error, trying to show a graph but passed in empty list of stats summaries");
            return;
        }

   
        
        graphsPerEnemy = new List<GraphPerEnemy>();


        // todo convert accuracy into an enemy setting

        accuraciesPerEnemy = new Dictionary<EnemySettings, List<AccuracyEntry>>();

        for (int i = 0; i < statsSummaries.Count; i++)
        {
            foreach (var accuracyPerEnemy in statsSummaries[i].AccuracyPerEnemy)
            {
                accuracyPerEnemy.EnemySettings = gameSettings.GetEnemySettingsFromGUID(accuracyPerEnemy.GUID);
                if (accuracyPerEnemy.EnemySettings == null)
                {
                    Debug.LogError("Error, couldn't find enemy settings for GUID " + accuracyPerEnemy.GUID);
                    continue;
                }

                if (!accuraciesPerEnemy.ContainsKey(accuracyPerEnemy.EnemySettings))
                {
                    accuraciesPerEnemy.Add(accuracyPerEnemy.EnemySettings, new List<AccuracyEntry>());
                }

                AccuracyEntry accuracyEntry = new AccuracyEntry(i, accuracyPerEnemy.Accuracy);
                accuraciesPerEnemy[accuracyPerEnemy.EnemySettings].Add(accuracyEntry);
            }
        }

        AdjustXLabels(statsSummaries.Count);

        foreach (var enemyStats in accuraciesPerEnemy)
        {
            CreateGraphForEnemy(enemyStats.Key, enemyStats.Value);
        }
    }


    [Button]
    void AdjustXLabels(int highestGameNumber)
    {
        int allLabelsCount = xLabels.Count;

        int labelStep = Mathf.CeilToInt(highestGameNumber / (float)xLabels.Count);

        int numberOfLabels = Mathf.CeilToInt(highestGameNumber / (float)labelStep);

        widthBetweenXEntries = graphArea.rect.width / (numberOfLabels - 1);

        widthXperUnit = widthBetweenXEntries / labelStep;


        for (int i = 0; i < allLabelsCount; i++)
        {
            if (i < numberOfLabels)
            {
                xLabels[i].gameObject.SetActive(true);
                xLabels[i].rectTransform.anchoredPosition = new Vector2(i * widthBetweenXEntries, xLabels[i].rectTransform.anchoredPosition.y);
                xLabels[i].text = ((i + 1) * labelStep).ToString();
            }
            else
            {
                xLabels[i].gameObject.SetActive(false);
            }
        }
    }


    void CreateGraphForEnemy(EnemySettings enemySettings, List<AccuracyEntry> values)
    {
        // recycle later...

        float yMaximum = 1f;

        GraphPerEnemy graph = new GraphPerEnemy();
        graph.EnemySettings = enemySettings;
        graph.Circles = new List<RectTransform>();
        graphsPerEnemy.Add(graph);


        float graphHeight = graphArea.rect.height;


        GameObject lastCircleGameObject = null;



        for (int i = 0; i < values.Count; i++)
        {
            float xPosition = values[i].GameIndex * widthXperUnit;

            float yPosition = (values[i].Accuracy / yMaximum) * graphHeight;

            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition), enemySettings.IconColor);
            RectTransform circleRectTransform = circleGameObject.GetComponent<RectTransform>();

            graph.Circles.Add(circleRectTransform);

            if (lastCircleGameObject != null)
            {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition,
                    circleRectTransform.anchoredPosition, enemySettings.IconColor);
            }

            lastCircleGameObject = circleGameObject;
        }

        foreach (var circle in graph.Circles)
        {
            circle.SetAsLastSibling();
        }
    }

    class GraphPerEnemy
    {
        public EnemySettings EnemySettings;
        public List<RectTransform> Circles;
        public List<RectTransform> Lines;
    }


    GameObject CreateCircle(Vector2 anchoredPosition, Color color)
    {
        GameObject circle = new GameObject("circle", typeof(Image));
        circle.transform.SetParent(graphArea, false);
        circle.GetComponent<Image>().sprite = circleSprite;
        circle.GetComponent<Image>().color = color;
        RectTransform rectTransform = circle.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(circleRadius, circleRadius);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return circle;
    }


    void CreateDotConnection(Vector2 start, Vector2 end, Color color)
    {
        GameObject connection = new GameObject("dotConnection", typeof(Image));
        connection.transform.SetParent(graphArea, false);
        connection.GetComponent<Image>().color = color;

        RectTransform rectTransform = connection.GetComponent<RectTransform>();
        Vector2 direction = (end - start).normalized;
        float distance = Vector2.Distance(start, end);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, lineThickness);
        rectTransform.anchoredPosition = start + direction * distance * .5f;

        //method from code monkey utils, check git for method if graph is needed
        rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(direction));
    }


    [Button(ButtonStyle.Box), GUIColor("blue")]
    void CreateXLabels(int count)
    {
        xLabels = new List<TextMeshProUGUI>();

        float width = graphArea.rect.width / count;

        for (int i = 0; i < count; i++)
        {
            float xPosition = (i + 1) * width;


            TextMeshProUGUI labelX = Instantiate(xLabelPrefab);
            RectTransform labelTransform = labelX.GetComponent<RectTransform>();
            labelTransform.SetParent(graphArea, false);
            labelTransform.anchoredPosition = new Vector2(xPosition, xLabelPrefab.rectTransform.anchoredPosition.y);
            labelX.GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();
            labelX.gameObject.SetActive(true);

            xLabels.Add(labelX);
        }

        xLabelPrefab.gameObject.SetActive(false);
    }


    [Button]
    void CreateYlabels()
    {
        yLabels = new List<TextMeshProUGUI>();

        float graphHeight = graphArea.rect.height;


        int yCount = 10;

        for (int i = 0; i < yCount + 1; i++)
        {
            TextMeshProUGUI yLabel = Instantiate(yLabelPrefab);
            RectTransform labelTransform = yLabel.GetComponent<RectTransform>();
            labelTransform.SetParent(graphArea, false);

            float normalizedValue = (float)i / yCount;

            float yPositionWithBuffer = normalizedValue * (graphHeight * (1f / (1f + bufferZoneOnTopRelative)));

            labelTransform.anchoredPosition = new Vector2(yLabelPrefab.rectTransform.anchoredPosition.x, yPositionWithBuffer);

            yLabel.text = (normalizedValue * 100f).ToString("F0");
            labelTransform.gameObject.SetActive(true);

            yLabels.Add(yLabel);
        }

        yLabelPrefab.gameObject.SetActive(false);
    }
}