using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Movable))]
public class MyGridDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Movable myGridData = (Movable)target;
        DrawDefaultInspector();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Grid Editor", EditorStyles.boldLabel);
        for (int y = 0; y < myGridData.gridHeight; y++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int x = 0; x < myGridData.gridWidth; x++)
            {
                int index = y * myGridData.gridWidth + x;
                if (index < myGridData.gridCells.Count)
                {
                    bool currentValue = myGridData.gridCells[index];
                    bool newValue = EditorGUILayout.Toggle(currentValue, GUILayout.Width(20));
                    if (newValue != currentValue)
                    {
                        myGridData.gridCells[index] = newValue;
                        EditorUtility.SetDirty(myGridData);
                    }
                }
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}

