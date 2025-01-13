using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Hx.Editor
{
    public class EditorUtils
    {
        [InitializeOnLoad]
        public static class HierarchyObjectIconDrawer
        {
            static HierarchyObjectIconDrawer()
            {
                EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemGUI;
            }

            static void OnHierarchyWindowItemGUI(int instanceID, Rect selectionRect)
            {
                GameObject go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
                if (go == null) return;

                List<string> labels = new List<string>();

                if (go.GetComponent<Animator>() != null)
                {
                    labels.Add("AC");
                }

                if (labels.Count > 0)
                {
                    string labelStr = string.Join("_", labels);
                    float labelWidth = EditorStyles.label.CalcSize(new GUIContent(labelStr)).x;
                    
                    Rect labelRect = new Rect(selectionRect.xMax - labelWidth, selectionRect.y, labelWidth, selectionRect.height);
                    EditorGUI.LabelField(labelRect, labelStr);
                }
            }
        }
    }
}