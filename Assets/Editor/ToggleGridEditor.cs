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
            GUILayout.Space(0);
            EditorGUILayout.LabelField("Toggle Grid", EditorStyles.boldLabel);

            for (int y = 0; y < data.height; y++)
            {
                
                EditorGUILayout.BeginHorizontal();

                for (int x = 0; x < data.width; x++)
                {
                    if(x % 2 == 0)
                    {
                        bool value = data.Get(x, y);

                        value = GUILayout.Toggle(value, "", GUILayout.Width(25), GUILayout.Height(25));
                        data.Set(x, y, value);
                    }
                    else
                    {
                       EditorGUILayout.LabelField(" ", GUILayout.MaxWidth(25));
                    }
                }
                EditorGUILayout.EndHorizontal();
                GUILayout.Space(0);
                EditorGUILayout.BeginHorizontal();
                for (int x = 0; x < data.width; x++)
                {
                    if (x % 2 == 1)
                    {
                        bool value = data.Get(x, y);

                        value = GUILayout.Toggle(value, "", GUILayout.Width(25), GUILayout.Height(25));
                        data.Set(x, y, value);
                    }
                    else
                    {
                        EditorGUILayout.LabelField(" ", GUILayout.MaxWidth(25));
                    }
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
