using System.Reflection;
using LOONACIA.Unity.Annotations;
using UnityEditor;
using UnityEngine;

namespace LOONACIA.Unity.Editor
{
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class ButtonEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            base.OnInspectorGUI();
            
            var methods = target.GetType().GetMethods(bindingFlags);
            foreach (var method in methods)
            {
                var buttonAttribute = method.GetCustomAttribute<ButtonAttribute>();
                if (buttonAttribute == null)
                {
                    continue;
                }
                
                string methodName = ObjectNames.NicifyVariableName(method.Name);
                string tooltip = string.Empty;
                var tooltipAttribute = method.GetCustomAttribute<TooltipAttribute>();
                if (tooltipAttribute != null)
                {
                    tooltip = tooltipAttribute.tooltip;
                }
                    
                if (GUILayout.Button(new GUIContent(methodName, tooltip)))
                {
                    var obj = method.IsStatic ? null : target;
                    method.Invoke(obj, null);
                }
            }
        }
    }
}