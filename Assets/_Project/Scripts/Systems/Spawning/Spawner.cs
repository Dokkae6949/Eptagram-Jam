using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Spawner : MonoBehaviour
    {
        public string tagg;
        private void Start()
        {
            SpawnerController.spawnMap.Add(tagg, transform.position);
        }
    }
}
