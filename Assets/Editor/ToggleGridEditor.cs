using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ToggleGridData))]
public class ToggleGridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ToggleGridData data = (ToggleGridData)target;

        EditorGUI.BeginChangeCheck();

        data.width = EditorGUILayout.IntField("Width", data.width);
        data.height = EditorGUILayout.IntField("Height", data.height);

        if (GUILayout.Button("Initialize Grid"))
        {
            data.Init();
        }

        if (data.gridValues != null && data.gridValues.Length == data.width * data.height)
        {
            GUILayout.Space(10);
            EditorGUILayout.LabelField("Toggle Grid", EditorStyles.boldLabel);

            for (int y = 0; y < data.height; y++)
            {
                
                EditorGUILayout.BeginHorizontal();

                if(y % 2 == 0) EditorGUILayout.LabelField(" ",GUILayout.MaxWidth(10));
                for (int x = 0; x < data.width; x++)
                {
                    bool value = data.Get(x, y);

                    value = GUILayout.Toggle(value, "", GUILayout.Width(25), GUILayout.Height(25));
                    data.Set(x, y, value);
                }
                EditorGUILayout.EndHorizontal();
            }
        }

        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(data);
        }
    }
}
