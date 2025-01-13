using UnityEditor;
using UnityEngine;

namespace Hx.Editor
{
    [CustomEditor(typeof(SkillSwordConfigOverall))]
    public class SkillSwordConfigEditor : UnityEditor.Editor
    {
         public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(10);
            
            var targetObj = (SkillSwordConfigOverall)target;
            
            // GUILayout.Toggle()
        }
    }
}