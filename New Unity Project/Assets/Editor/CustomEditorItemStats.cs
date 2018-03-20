/*using UnityEditor;
using UnityEngine;
using System.Collections;


[CustomEditor(typeof(ItemStats)), CanEditMultipleObjects]
public class CustomEditorItemStats : Editor
{
    public SerializedProperty
        itemGameObject_Prop,
         itemName_Prop,
        buffEffects_prop;

    void OnEnabled()
    {
        itemName_Prop = serializedObject.FindProperty("itemName");
        buffEffects_prop = serializedObject.FindProperty("buffEffects");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        

    }

}
*/
