#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;
using static UnityEngine.UI.Button;
using UnityEngine.UI;


namespace ButtonSpace
{
    [CustomEditor(typeof(ButtonAnimation))]
    public class ButtonAnimationEditor : ButtonEditor
    {
        private SerializedProperty _onClick;

        protected override void OnEnable()
        {
            base.OnEnable();

            _onClick = serializedObject.FindProperty("onClickEvent");
        }

        public override void OnInspectorGUI()
        {
            ButtonAnimation component = (ButtonAnimation)target;

            component.rectTransform = (RectTransform)EditorGUILayout.ObjectField("Rect transozyfrm", component.rectTransform, typeof(RectTransform), true);
            component.buttonImage = (Image)EditorGUILayout.ObjectField("buttonImage", component.buttonImage, typeof(Image), true);
            component.settings = (ButtonScriptableObject)EditorGUILayout.ObjectField("ButtonSettings", component.settings, typeof(ButtonScriptableObject), true);

            serializedObject.Update();

            EditorGUILayout.PropertyField(_onClick, true);

            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif