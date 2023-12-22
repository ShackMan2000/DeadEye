using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using TMPro;
using Sirenix.OdinInspector;


[RequireComponent(typeof(RectTransform))]
public class Graph : MonoBehaviour
{
    [SerializeField] Sprite circleSprite;


    // take in a list of accuracy entries and create a graph.
    // for now the y axis is always going to be fixed. Use values 0 to 1
    // so create the chart, then create multiple graphs. Each enemy must have an icon color
    // so that needs to be passed in too, by getting retrieved from the enemy settings

    // so pass in the higher levels, the enemy save data class with the 2 lists.
    // or maybe a bit more data but there is simply a list connected to an enemy setting
    // so for the stats window shouldn't really matter.

    // stats window could show the graph for past games, at least the time trial.


    // and save data has a list of entries for the modes.

    //  [SerializeField] ColorScheme colorScheme;


    // this is the list that will be passed in, could be all time trial games, all wave games, or all waves during a wave game
    // contains a list for each enemy
    public List<AccuracyPerEnemy> AccuraciesAllEnemies;
    public List<AccuracyEntry> testEntries;

    [SerializeField] RectTransform labelXprefab;
    [SerializeField] RectTransform yLabelPrefab;
    [SerializeField] RectTransform accuracyYLabelYprefab;

    [SerializeField] float bufferZoneOnTopRelative = 0.1f;

    [SerializeField] int showEveryXLabel = 5;


    [SerializeField] float lineThicknessRelativeToArea;
    [SerializeField] float circleRadiusRelativeToArea;


    [SerializeField] RectTransform graphArea;

    [SerializeField] List<int> showXMarkersAtValues;

[SerializeField]    float lineThickness;
[SerializeField]    float circleRadius;

    List<RectTransform> activeCircles;

    // [SerializeField] ScoreTracker scoreTracker;


    [Button]
    void CreateTestAccuracyEntries(int entries)
    {
        testEntries = new List<AccuracyEntry>();

        for (int i = 0; i < entries; i++)
        {
            AccuracyEntry entry = new AccuracyEntry();
            entry.Index = i;
            entry.Accuracy = Random.Range(0f, 1f);
            testEntries.Add(entry);
        }
    }


    void Awake()
    {
        labelXprefab.gameObject.SetActive(false);
        yLabelPrefab.gameObject.SetActive(false);
        accuracyYLabelYprefab.gameObject.SetActive(false);
    }


    [Button]
    public void CreateGraphFromTestValues()
    {
        List<float> floatList = new List<float>();

        foreach (var entry in testEntries)
        {
            floatList.Add(entry.Accuracy);
        }

        CreateCirclesAndConnections(floatList, Color.red);
    }


    
    [Button]
    public void CreateLabels()
    {
        // float totalGraphArea = graphArea.rect.width * graphArea.rect.height;
        //
        // lineThickness = totalGraphArea * lineThicknessRelativeToArea * 0.00001f;
        // circleRadius = totalGraphArea * circleRadiusRelativeToArea * 0.00001f;


        activeCircles = new List<RectTransform>();

        CreateYlabels(accuracyYLabelYprefab, 100f);

      // CreateXMarkers();

        foreach (var circle in activeCircles)
        {
            circle.SetAsLastSibling();
        }
    }

    
    // should use some kind of max value
    // e.g. show most 10, if more than that split...
    // polish later....
    
    
    
    // for wave game, need to pass in the highest values
    // actually need to pass in the number of waves
    // for the stats per game
    // again, polish later... for now it will just show a label for every game...   if there is time, make a 3D graph and shit
    
    [Button]
    void CreateXMarkers(int count)
    {
        float widthBetweenCircles = graphArea.rect.width / count;

        for (int i = 0; i < count; i++)
        {
            float xPosition = (i + 1) * widthBetweenCircles;

            //change this so it shows every x label with an exception of the first, so it shows 1 in any case

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


    
    // pass in higher level class
    void CreateCirclesAndConnections(List<float> values, Color color)
    {
        float yMaximum = 1f;

        float graphHeight = graphArea.rect.height;
        float widthBetweenCircles = graphArea.rect.width / (values.Count);


        // so 100% isn't a flat line on top of the graph
        yMaximum += yMaximum * bufferZoneOnTopRelative;

          GameObject lastCircleGameObject = null;


        // todo add padding after last entry
        
        for (int i = 0; i < values.Count; i++)
        {
            float xPosition = widthBetweenCircles + i * widthBetweenCircles;

            float yPosition = (values[i] / yMaximum) * graphHeight;

            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition), color);
            RectTransform circleRectTransform = circleGameObject.GetComponent<RectTransform>();

            activeCircles.Add(circleRectTransform);

             if (lastCircleGameObject != null)
             {
                 CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition,
                     circleRectTransform.anchoredPosition, color);
             }

            lastCircleGameObject = circleGameObject;
        }
    }


    void CreateYlabels(RectTransform labelPrefab, float yMaximum)
    {
        float graphHeight = graphArea.rect.height;

        int yCount = 6;

        for (int i = 0; i < yCount; i++)
        {
            RectTransform label = Instantiate(labelPrefab);
            label.SetParent(graphArea, false);

            float normalizedValue = (float)i / (yCount - 1);

            float yPositionWithBuffer = normalizedValue * (graphHeight * (1f / (1f + bufferZoneOnTopRelative)));

            label.anchoredPosition = new Vector2(labelPrefab.anchoredPosition.x, yPositionWithBuffer);

            float yLabelValue = Mathf.FloorToInt(yMaximum * normalizedValue);
            label.GetComponent<TextMeshProUGUI>().text = yLabelValue.ToString();
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