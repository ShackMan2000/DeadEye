using UnityEngine;
using UnityEditor;

public class SliderValueEditor : MonoBehaviour
{
    /* private SerializedProperty sliderValue;

    private void OnEnable()
    {
        sliderValue = serializedObject.FindProperty("publicValue");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(sliderValue);

        float newValue = EditorGUILayout.Slider("Slider Value", sliderValue.floatValue, 0f, 1f);
        sliderValue.floatValue = newValue;

        serializedObject.ApplyModifiedProperties();
    } */
}