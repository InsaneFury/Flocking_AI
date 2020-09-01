using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;

[CustomEditor(typeof(CompositeBehavior))]
public class CompositeBehaviorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //Setup of inspector
        CompositeBehavior compositeBehavior = (CompositeBehavior)target;

        if (compositeBehavior.behaviors == null || compositeBehavior.behaviors.Length == 0)
        {
            EditorGUILayout.HelpBox("No Behaviors in array.", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Number", GUILayout.MinWidth(60f), GUILayout.MaxWidth(60f));
            EditorGUILayout.LabelField("Behaviors", GUILayout.MinWidth(60f));
            EditorGUILayout.LabelField("Weights", GUILayout.MinWidth(60f), GUILayout.MaxWidth(60f));
            EditorGUILayout.EndHorizontal();

            EditorGUI.BeginChangeCheck();
            for (int i = 0; i < compositeBehavior.behaviors.Length; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(i.ToString(), GUILayout.MinWidth(60f), GUILayout.MaxWidth(60f));
                compositeBehavior.behaviors[i] = (FlockBehavior)EditorGUILayout.ObjectField(compositeBehavior.behaviors[i], typeof(FlockBehavior), false, GUILayout.MinWidth(60f));
                compositeBehavior.weights[i] = EditorGUILayout.FloatField(compositeBehavior.weights[i], GUILayout.MinWidth(60f), GUILayout.MaxWidth(60f));
                EditorGUILayout.EndHorizontal();
            }

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(compositeBehavior);
            }
        }

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Behavior"))
        {
            AddBehavior(compositeBehavior);
            EditorUtility.SetDirty(compositeBehavior);
        }
        EditorGUILayout.EndHorizontal();

        if (compositeBehavior.behaviors != null && compositeBehavior.behaviors.Length > 0)
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Remove Behavior"))
            {
                RemoveBehavior(compositeBehavior);
                EditorUtility.SetDirty(compositeBehavior);
            }
            EditorGUILayout.EndHorizontal();
        }
        
    }

    void AddBehavior(CompositeBehavior cb)
    {
        int oldCount = (cb.behaviors!=null) ? cb.behaviors.Length : 0;

        FlockBehavior[] newBehaviors = new FlockBehavior[oldCount + 1];
        float[] newWeights = new float[oldCount + 1];

        for (int i = 0; i < oldCount; i++)
        {
            newBehaviors[i] = cb.behaviors[i];
            newWeights[i] = cb.weights[i];
        }

        newWeights[oldCount] = 1f;
        cb.behaviors = newBehaviors;
        cb.weights = newWeights;
    }

    void RemoveBehavior(CompositeBehavior cb)
    {
        int oldCount = cb.behaviors.Length;

        if(oldCount == 1)
        {
            cb.behaviors = null;
            cb.weights = null;
            return;
        }

        FlockBehavior[] newBehaviors = new FlockBehavior[oldCount - 1];
        float[] newWeights = new float[oldCount - 1];

        for (int i = 0; i < oldCount - 1; i++)
        {
            newBehaviors[i] = cb.behaviors[i];
            newWeights[i] = cb.weights[i];
        }
        cb.behaviors = newBehaviors;
        cb.weights = newWeights;
    }
}
