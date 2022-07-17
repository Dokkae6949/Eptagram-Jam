using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class MonsterRegistrator : MonoBehaviour
    {
        bool _isAlive;
        #region InitializationMethods================================================================================================================
        private void Awake()
        {
            _isAlive = true;
        }
        #endregion
        #region Public Methods ======================================================================================================================

        public void OnMonsterDeath()
        {
            if (!_isAlive) return;
            _isAlive = false;
            MonsterTracker.Instance.MonsterDied(this);
        }
        #endregion
    }
}
