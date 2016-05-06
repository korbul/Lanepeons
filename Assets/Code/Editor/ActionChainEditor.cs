using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(ActionChain))]
public class ActionChainEditor : Editor {

    MonoScript[][] monoScripts;

    void OnEnable()
    {
        SerializedProperty actions = serializedObject.FindProperty("actions");

        monoScripts = new MonoScript[actions.arraySize][];

        for (int i = 0; i < actions.arraySize; i++)
        {
            SerializedProperty element = actions.GetArrayElementAtIndex(i);
            SerializedProperty behaviours = element.FindPropertyRelative("behaviours");
            monoScripts[i] = new MonoScript[behaviours.arraySize];
            for (int j = 0; j < behaviours.arraySize; j++)
            {
                SerializedProperty behaviour = behaviours.GetArrayElementAtIndex(j);
                MonoScriptFromString(ref monoScripts[i][j], behaviour.stringValue);
            }
        }
    }

    private void MonoScriptFromString(ref MonoScript script, string behaviourName)
    {
        if (!string.IsNullOrEmpty(behaviourName))
        {
            string[] scripts = AssetDatabase.FindAssets(string.Format("t:Script {0}", behaviourName));
            if (scripts.Length > 0)
            {
                string path = AssetDatabase.GUIDToAssetPath(scripts[0]);
                script = (MonoScript)AssetDatabase.LoadAssetAtPath(path, typeof(MonoScript));
            }
        }
    }

    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector();

        //EditorGUI.BeginChangeCheck();
        //serializedObject.Update();
        //SerializedProperty iterator = serializedObject.GetIterator();
        //bool enterChildren = true;
        //while (iterator.NextVisible(enterChildren))
        //{
        //    EditorGUILayout.PropertyField(iterator, true, new GUILayoutOption[0]);
        //    enterChildren = false;
        //}

        ////SerializedProperty prop = serializedObject.FindProperty("behaviours");
        ////EditorGUILayout.PropertyField(prop, true, new GUILayoutOption[0]);

        //serializedObject.ApplyModifiedProperties();
        //EditorGUI.EndChangeCheck();

        serializedObject.Update();
        var list = serializedObject.FindProperty("actions");
        EditorGUILayout.PropertyField(list);
        EditorGUI.indentLevel += 1;
        if (list.isExpanded)
        {
            EditorGUILayout.PropertyField(list.FindPropertyRelative("Array.size"));

            if (list.arraySize != monoScripts.Length)
            {
                System.Array.Resize(ref monoScripts, list.arraySize);
            }

            for (int i = 0; i < list.arraySize; i++)
            {
                SerializedProperty element = list.GetArrayElementAtIndex(i);
                EditorGUILayout.PropertyField(element);

                EditorGUI.indentLevel += 1;
                if (element.isExpanded)
                {
                    bool enterChildren = true;
                    while (element.Next(enterChildren))
                    {
                        EditorGUILayout.PropertyField(element);
                        if(element.name == "behaviours")
                        {
                            DisplayBehaviours(element,i);
                            break;
                        }
                        enterChildren = false;
                    }
                }
                EditorGUI.indentLevel -= 1;
                //DisplayBehaviours(element);
            }
        }
        EditorGUI.indentLevel -= 1;
        serializedObject.ApplyModifiedProperties();
    }

    void DisplayBehaviours(SerializedProperty list, int idx)
    {
        EditorGUI.indentLevel += 1;
        if (list.isExpanded)
        {
            EditorGUILayout.PropertyField(list.FindPropertyRelative("Array.size"));

            if(null == monoScripts[idx])
            {
                monoScripts[idx] = new MonoScript[list.arraySize];
            }

            if(list.arraySize != monoScripts[idx].Length)
            {
                System.Array.Resize(ref monoScripts[idx], list.arraySize);
            }

            for (int i = 0; i < list.arraySize; i++)
            {
                SerializedProperty element = list.GetArrayElementAtIndex(i);
                monoScripts[idx][i] = EditorGUILayout.ObjectField(monoScripts[idx][i], typeof(MonoScript), false) as MonoScript;
                if (null != monoScripts[idx][i])
                {
                    if (!typeof(IBehaviour).IsAssignableFrom(monoScripts[idx][i].GetClass()))
                    {
                        EditorGUILayout.HelpBox("Must be of type " + typeof(IBehaviour).Name + ". Type found " + monoScripts[idx][i].GetClass().Name, MessageType.Error, true);
                    }
                    else
                    {
                        element.stringValue = monoScripts[idx][i].GetClass().Name;
                    }
                }
                else if (!string.IsNullOrEmpty(element.stringValue))
                {
                    MonoScriptFromString(ref monoScripts[idx][i], element.stringValue);
                }
            }
        }
        EditorGUI.indentLevel -= 1;
    }
}
