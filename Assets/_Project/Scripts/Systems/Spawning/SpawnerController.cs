using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class SpawnerController : MonoBehaviour
    {
        [SerializeField]GameObject spawnedSpawnerPre;
        public static Dictionary<string, Vector3> spawnMap;
        public static Dictionary<string, int> spawnNums;
        public Dictionary<EnemyType, GameObject> enemyMap;

        public WaveData testWave;

        private void Awake()
        {
            spawnMap = new Dictionary<string, Vector3>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Spawn(testWave);
            }
        }
        public void Spawn(WaveData wave)
        {
            foreach (var group in wave.groups)
            {
                var freq = (group.end - group.start) / (group.num);
                var spawnerPos = new List<Vector3>();
                foreach (string s in group.spawners)
                {
                    spawnerPos.Add(spawnMap[s]);
                }
                GameObject inst = Instantiate(spawnedSpawnerPre);
                var comp = inst.GetComponent<SpawnedSpawner>();
                StartCoroutine(comp.SpawningRoutine(group, freq, spawnerPos));
            }
        }
    }
}
