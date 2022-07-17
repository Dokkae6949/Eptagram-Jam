using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Game.Inventory
{
    [CustomEditor(typeof(Dice))]
    public class DiceEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Dice script = (Dice)target;

            if (GUILayout.Button("Update Dice Data"))
                script.UpdateDiceData();
        }
    }
}