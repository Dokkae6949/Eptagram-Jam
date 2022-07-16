using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemies
{
    [CreateAssetMenu(fileName = "Enemy Brain", menuName = "Enemies/Brain")]
    public class SOEnemyBrain : ScriptableObject
    {
        public float movementSpeed;
    }
}
