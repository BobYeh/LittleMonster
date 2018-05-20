using UnityEngine;
using UnityEngine.UI;
using UnityEditor.UI;

namespace UnityEditor.UI
{
    //
    // 概要:
    //     Custom Editor for the Radio Button Component.
    [CustomEditor(typeof(RadioButton), true)]
    public class RadioButtonInspector : ButtonEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            RadioButton component = (RadioButton)target;

            component.RadioButtonKey = EditorGUILayout.TextField("Radio Button Key", component.RadioButtonKey);
        }
    }
}
