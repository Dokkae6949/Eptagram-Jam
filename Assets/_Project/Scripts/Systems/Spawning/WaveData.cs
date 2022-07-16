using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "WaveData", menuName = "WaveInfo/CreateNewWave")]
    [System.Serializable]
    public class WaveData : ScriptableObject
    {
        public SpawnGroup[] groups;
        public int GetEnemyNumber(EnemyType type)
        {
            int num = 0;
            foreach (var group in groups)
            {
                num += group.num;
            }
            return num;
        }
    }
    [System.Serializable]
    public class SpawnGroup
    {
        public float start;
        public float end;
        public string[] spawners;
        public GameObject enemy;
        public int num;
    }
    public enum EnemyType
    {
        Snipy,
        Shooty,
        Biggy,
        Speedy
    }
}
