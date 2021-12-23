using UnityEngine;
using UnityEditor;

namespace ROSUnityCore
{
    [CustomEditor(typeof(ROS))]
    public class EditorROSUnity : Editor
    {
        public override void OnInspectorGUI()
        {
            
            if (GUILayout.Button("Re-Conect"))
            {
                ((ROS)target).Disconnect();
                ((ROS)target).Connect();
            }

            base.OnInspectorGUI();
        }
    }
}