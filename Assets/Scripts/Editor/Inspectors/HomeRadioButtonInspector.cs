using UnityEngine;
using UnityEngine.UI;
using UnityEditor.UI;

namespace UnityEditor.UI
{
    //
    // 概要:
    //     Custom Editor for the Home Radio Button Component.
    [CustomEditor(typeof(HomeRadioButton), true)]
    public class HomeRadioButtonInspector : RadioButtonInspector
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            HomeRadioButton component = (HomeRadioButton)target;

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PrefixLabel("OnSelected Mark");
            component.selectedMark = (GameObject)EditorGUILayout.ObjectField(component.selectedMark, typeof(GameObject), allowSceneObjects: true);

            EditorGUILayout.EndHorizontal();
        }
    }
}
