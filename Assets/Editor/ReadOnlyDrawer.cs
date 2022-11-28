using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="position">Position.</param>
    /// <param name="property">Property.</param>
    /// <param name="label">Label.</param>
    // Start is called before the first frame update
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //Saving previous GUI enable value
        var previousGUIState = GUI.enabled;
        //Disabling edit for property
        GUI.enabled = false;
        //Drawing property
        EditorGUI.PropertyField(position, property, label);
        //Setting old GUI enabled value
        GUI.enabled = previousGUIState;
    }
}
