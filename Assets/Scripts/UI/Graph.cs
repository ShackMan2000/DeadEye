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


    public List<AccuracyPerEnemy> AccuraciesAllEnemies;

    public List<AccuracyEntry> testEntries;

    [SerializeField] TextMeshProUGUI xLabelPrefab;
    [SerializeField] TextMeshProUGUI yLabelPrefab;

    [SerializeField] List<TextMeshProUGUI> xLabels;
    [SerializeField] List<TextMeshProUGUI> yLabels;

    [SerializeField] float bufferZoneOnTopRelative = 0.1f;

    //   [SerializeField] int showEveryXLabel = 1;


    [SerializeField] GameSettings gameSettings;

    [SerializeField] RectTransform graphArea;


    [SerializeField] float lineThickness;
    [SerializeField] float circleRadius;

    List<RectTransform> activeCircles;

    float widthBetweenXEntries;


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
        CreateGraphs(saveData.StatsForWaveGames);
    }


    [Button]
    public void CreateAndShowTestData(int count, float fillRate)
    {
        
        
        
    }
    
    
    public void CreateGraphs(List<StatsSummaryPerGame> statsSummaries)
    {
        if (statsSummaries.Count == 0)
        {
            Debug.Log("Error, trying to show a graph but passed in empty list of stats summaries");
            return;
        }

        foreach (var summary in statsSummaries)
        {
            if (summary.AccuracyPerEnemy.Count == 0)
            {
                //  Debug.Log("Warning, trying to show a graph but passed in empty list of accuracy per enemy for stat summary with index " + summary.Index);
            }
        }


        //  widthBetweenXEntries = graphArea.rect.width / xLabelCountAdjusted;


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
            CreateCirclesAndConnections(enemyStats.Key, enemyStats.Value);
        }
    }


    [Button]
    public void CreateGraphFromTestValues()
    {
        List<float> floatList = new List<float>();

        foreach (var entry in testEntries)
        {
            floatList.Add(entry.Accuracy);
        }

        //  CreateCirclesAndConnections(floatList, Color.red);
    }


    [Button]
    public void CreateLabels()
    {
        activeCircles = new List<RectTransform>();

        foreach (var circle in activeCircles)
        {
            circle.SetAsLastSibling();
        }
    }


    // make a button that can just create random entries, use that for testing skipped entries too
    // make x entries with a 20% chance that an entry is skipped, as in that enemy was not present in the wave
    // for now only do accuracy


    int GetLabelCount(int highestGameNumber)
    {
        int labelStep = Mathf.CeilToInt(highestGameNumber / (float)xLabels.Count);

        int numberOfLabels = Mathf.CeilToInt(highestGameNumber / (float)labelStep);

        return numberOfLabels;
    }

    [Button]
    int AdjustXLabels(int highestGameNumber)
    {
        int allLabelsCount = xLabels.Count;
        int labelStep = Mathf.CeilToInt(highestGameNumber / (float)xLabels.Count);


        int numberOfLabels = GetLabelCount(highestGameNumber);

        widthBetweenXEntries = graphArea.rect.width / (numberOfLabels - 1);

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


        return numberOfLabels;
    }


// need one for accuracy too.
// might be best to just create a place holder for accuracy, an enemy setting
// so that can be used for the 
    void CreateCirclesAndConnections(EnemySettings enemySettings, List<AccuracyEntry> values)
    {
        float yMaximum = 1f;


        activeCircles = new List<RectTransform>();
        float graphHeight = graphArea.rect.height;

        // so 100% isn't a flat line on top of the graph
        //  yMaximum += yMaximum * bufferZoneOnTopRelative;

        GameObject lastCircleGameObject = null;


        // todo add padding after last entry

        for (int i = 0; i < values.Count; i++)
        {
            float xPosition = (i + 1) * widthBetweenXEntries;

            float yPosition = (values[i].Accuracy / yMaximum) * graphHeight;

            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition), enemySettings.IconColor);
            RectTransform circleRectTransform = circleGameObject.GetComponent<RectTransform>();

            activeCircles.Add(circleRectTransform);

            if (lastCircleGameObject != null)
            {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition,
                    circleRectTransform.anchoredPosition, enemySettings.IconColor);
            }

            lastCircleGameObject = circleGameObject;
        }
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
        connection.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 0.8f);

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