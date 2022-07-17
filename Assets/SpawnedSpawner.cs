using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class SpawnedSpawner : MonoBehaviour
    {
        public IEnumerator SpawningRoutine(SpawnGroup group, float freq, List<Vector3> positions)
        {
            for (int i = 0; i < group.num; i++)
            {
                GameObject nmy = Instantiate(group.enemy);
                nmy.transform.position = positions[i % positions.Count];
                yield return new WaitForSeconds(freq);
            }
            Destroy(this);
        }
    }
}
