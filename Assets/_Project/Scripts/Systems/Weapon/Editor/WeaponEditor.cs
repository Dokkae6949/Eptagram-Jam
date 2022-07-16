using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Game.WeaponSystem
{
    [CustomEditor(typeof(Weapon))]
    public class WeaponEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Weapon script = (Weapon)target;

            if (GUILayout.Button("Load Weapon Data")) script.CopySOData();
        }
    }
}
