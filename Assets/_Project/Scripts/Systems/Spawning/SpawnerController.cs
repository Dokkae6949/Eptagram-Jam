using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class SpawnerController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        MonsterTracker _monsterTracker;
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
            _monsterTracker.SpawnNewWave(wave.GetEnemyNumber());

            foreach (var group in wave.groups)
            {
                var freq = (group.end - group.start) / (group.num);
                var spawnerPos = new List<Vector3>();
                foreach (string s in group.spawners)
                {
                    spawnerPos.Add(spawnMap[s]);
                }
                
                StartCoroutine(SpawningRoutine(group, freq, spawnerPos));
            }
        }
        public IEnumerator SpawningRoutine(SpawnGroup group, float freq, List<Vector3> positions)
        {
            for (int i = 0; i < group.num; i++)
            {
                GameObject nmy = Instantiate(group.enemy);
                nmy.transform.position = positions[i % positions.Count];
                yield return new WaitForSeconds(freq);
            }
            //Destroy(this);
        }
    }
}
