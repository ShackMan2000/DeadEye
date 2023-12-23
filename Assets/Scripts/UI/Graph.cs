using System;
using System.Collections.Generic;
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

    public List<AccuracyPerEnemy> AccuraciesAllEnemies;

    public List<AccuracyEntry> testEntries;

    [SerializeField] RectTransform labelXprefab;
    [SerializeField] RectTransform yLabelPrefab;

    [SerializeField] float bufferZoneOnTopRelative = 0.1f;

    [SerializeField] int showEveryXLabel = 1;


    [SerializeField] GameSettings gameSettings;

    [SerializeField] RectTransform graphArea;

    [SerializeField] SaveData saveData;

    [SerializeField] float lineThickness;
    [SerializeField] float circleRadius;

    List<RectTransform> activeCircles;

    float widthBetweenXEntries;


    Dictionary<EnemySettings, List<AccuracyEntry>> accuraciesPerEnemy;


    // [SerializeField] ScoreTracker scoreTracker;

    // need to use struct because some enemies won't have entries for some indexes, so can't just loop through list
    [Serializable]
    public struct AccuracyEntry
    {
        public int Index;
        public float Accuracy;

        public AccuracyEntry(int index, float accuracy)
        {
            Index = index;
            Accuracy = accuracy;
        }
    }
 

    void Awake()
    {
        labelXprefab.gameObject.SetActive(false);
        yLabelPrefab.gameObject.SetActive(false);
    }


    [Button]
    public void CreateGraphsForWaveGames()
    {
        CreateGraphs(saveData.StatsForWaveGames);
    }

    public void CreateGraphs(List<StatsSummary> statsSummaries)
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
                Debug.Log("Warning, trying to show a graph but passed in empty list of accuracy per enemy for stat summary with index " + summary.index);
            }
        }


        // +1 so it creates padding in the end
        widthBetweenXEntries = graphArea.rect.width / (statsSummaries.Count + 1);

        CreateXMarkers(widthBetweenXEntries, statsSummaries.Count);


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
        // float totalGraphArea = graphArea.rect.width * graphArea.rect.height;
        //
        // lineThickness = totalGraphArea * lineThicknessRelativeToArea * 0.00001f;
        // circleRadius = totalGraphArea * circleRadiusRelativeToArea * 0.00001f;

        activeCircles = new List<RectTransform>();

        foreach (var circle in activeCircles)
        {
            circle.SetAsLastSibling();
        }
    }


    // later use last x waves etc, but might have time for 3D graph so just move on...
    [Button]
    void CreateXMarkers(float width, int count)
    {
        for (int i = 0; i < count; i++)
        {
            float xPosition = (i + 1) * width;


            if (i == 0 || (i + 1) % showEveryXLabel == 0)
            {
                RectTransform labelX = Instantiate(labelXprefab);
                labelX.SetParent(graphArea, false);
                labelX.anchoredPosition = new Vector2(xPosition, labelXprefab.anchoredPosition.y);
                labelX.GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();
                labelX.gameObject.SetActive(true);
            }
        }

        labelXprefab.gameObject.SetActive(false);
    }


    void CreateCirclesAndConnections(EnemySettings enemySettings, List<AccuracyEntry> values)
    {
        float yMaximum = 1f;

        
        activeCircles = new List<RectTransform>();
        float graphHeight = graphArea.rect.height;

        // so 100% isn't a flat line on top of the graph
        yMaximum += yMaximum * bufferZoneOnTopRelative;

        GameObject lastCircleGameObject = null;


        // todo add padding after last entry

        for (int i = 0; i < values.Count; i++)
        {
            float xPosition =  (i + 1) * widthBetweenXEntries;

            float yPosition = (values[i].Accuracy / yMaximum) * graphHeight;

            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition), enemySettings.IconColor);
            RectTransform circleRectTransform = circleGameObject.GetComponent<RectTransform>();

            activeCircles.Add(circleRectTransform);

            if (lastCircleGameObject != null)
            {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition,
                    circleRectTransform.anchoredPosition,   enemySettings.IconColor); 
            }

            lastCircleGameObject = circleGameObject;
        }
    }


    [Button]
    void CreateYlabels()
    {
        float graphHeight = graphArea.rect.height;

        int yCount = 10;

        for (int i = 0; i < yCount + 1; i++)
        {
            RectTransform label = Instantiate(yLabelPrefab);
            label.SetParent(graphArea, false);

            float normalizedValue = (float)i / yCount;

            float yPositionWithBuffer = normalizedValue * (graphHeight * (1f / (1f + bufferZoneOnTopRelative)));

            label.anchoredPosition = new Vector2(yLabelPrefab.anchoredPosition.x, yPositionWithBuffer);

            label.GetComponent<TextMeshProUGUI>().text = (normalizedValue * 100f).ToString("F0");
            label.gameObject.SetActive(true);
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


    void OnDisable()
    {
        //  SinglePlayer.EvtGameFinished -= CreateGraphs;
    }
}