using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class MonsterTracker : StaticInstance<MonsterTracker>
    {

        int _monstersAlive = 0;

        #region Public Methods=======================================================================================================================
        public void SpawnNewWave(int numOfMonsters)
        {
            _monstersAlive = numOfMonsters;
        }

        public void MonsterDied(MonsterRegistrator monster)
        {
            _monstersAlive--;
            Debug.Log(_monstersAlive);
            if(_monstersAlive<=0)
            {
                GameManager.Instance.OnWaveDone();
            }
        }

        #endregion
    }
}
