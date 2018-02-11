using UnityEngine;
using UnityEditor;

namespace Utilities
{
    // Original Script: http://wiki.unity3d.com/index.php?title=TransformInspector&oldid=19215

    [CanEditMultipleObjects, CustomEditor(typeof(Transform))]
    public class TransformInspector : Editor
    {

        // private const float FIELD_WIDTH = 212.0f;
        private const bool WIDE_MODE = true;

        private const float POSITION_MAX = 100000.0f;

        private static GUIContent positionGUIContent = new GUIContent();
        private static GUIContent rotationGUIContent = new GUIContent();
        private static GUIContent scaleGUIContent = new GUIContent();

        private static string positionWarningText = LocalString("Due to floating-point precision limitations, it is recommended to bring the world coordinates of the GameObject within a smaller range.");

        private SerializedProperty positionProperty;
        private SerializedProperty rotationProperty;
        private SerializedProperty scaleProperty;

        private static string LocalString(string text)
        {
            return text;
            //return new LocalizationDatabase.GetLocalizedString(text);
        }

        public void OnEnable()
        {
            this.positionProperty = this.serializedObject.FindProperty("m_LocalPosition");
            this.rotationProperty = this.serializedObject.FindProperty("m_LocalRotation");
            this.scaleProperty = this.serializedObject.FindProperty("m_LocalScale");
        }

        public override void OnInspectorGUI()
        {
            EditorGUIUtility.wideMode = TransformInspector.WIDE_MODE;
            // EditorGUIUtility.labelWidth = EditorGUIUtility.currentViewWidth - TransformInspector.FIELD_WIDTH; // align field to right of inspector
            EditorGUIUtility.labelWidth = EditorGUIUtility.currentViewWidth - (Screen.width);

            this.serializedObject.Update();

            {
                Transform t = (Transform)target;

                // Position
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("P", GUILayout.Width(20)))
                {
                    t.localPosition = new Vector3(0f, 0f, 0f);
                }
                EditorGUILayout.PropertyField(this.positionProperty, positionGUIContent);
                GUILayout.EndHorizontal();
                // Rotation
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("R", GUILayout.Width(20)))
                {
                    t.eulerAngles = new Vector3(0f, 0f, 0f);
                }
                this.RotationPropertyField(this.rotationProperty, rotationGUIContent);
                GUILayout.EndHorizontal();
                // Scale
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("S", GUILayout.Width(20)))
                {
                    t.localScale = new Vector3(1f, 1f, 1f);
                }
                EditorGUILayout.PropertyField(this.scaleProperty, scaleGUIContent);
                GUILayout.EndHorizontal();
            }


            if (!ValidatePosition(((Transform)this.target).position))
            {
                EditorGUILayout.HelpBox(positionWarningText, MessageType.Warning);
            }

            this.serializedObject.ApplyModifiedProperties();
        }

        private bool ValidatePosition(Vector3 position)
        {
            if (Mathf.Abs(position.x) > POSITION_MAX) return false;
            if (Mathf.Abs(position.y) > POSITION_MAX) return false;
            if (Mathf.Abs(position.z) > POSITION_MAX) return false;
            return true;
        }

        private void RotationPropertyField(SerializedProperty rotationProperty, GUIContent content)
        {
            Transform transform = (Transform)this.targets[0];
            Quaternion localRotation = transform.localRotation;
            foreach (UnityEngine.Object t in (UnityEngine.Object[])this.targets)
            {
                if (!SameRotation(localRotation, ((Transform)t).localRotation))
                {
                    EditorGUI.showMixedValue = true;
                    break;
                }
            }

            EditorGUI.BeginChangeCheck();

            Vector3 eulerAngles = EditorGUILayout.Vector3Field(content, localRotation.eulerAngles);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObjects(this.targets, "Rotation Changed");
                foreach (UnityEngine.Object obj in this.targets)
                {
                    Transform t = (Transform)obj;
                    t.localEulerAngles = eulerAngles;
                }
                rotationProperty.serializedObject.SetIsDifferentCacheDirty();
            }

            EditorGUI.showMixedValue = false;
        }

        private bool SameRotation(Quaternion rot1, Quaternion rot2)
        {
            if (rot1.x != rot2.x) return false;
            if (rot1.y != rot2.y) return false;
            if (rot1.z != rot2.z) return false;
            if (rot1.w != rot2.w) return false;
            return true;
        }
    }

}