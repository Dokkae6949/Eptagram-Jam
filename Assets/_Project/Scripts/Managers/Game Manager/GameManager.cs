using Game.Player;
using Game.WeaponSystem;
using Game.Health;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Inventory;

namespace Game
{
    public class GameManager : StaticInstance<GameManager>
    {
        [Header("PleyrReferences")]
        [SerializeField]
        private PlayerControls _playerControls;
        [SerializeField]
        private InventorySystem _inventorySystem;
        [SerializeField]
        private Weapon _weaponScript;
        [SerializeField]
        private HealthSystem _healthSystem;
        [Header("Wave Data & References")]
        [SerializeField]
        private SpawnerController _spawnController;
        [SerializeField]
        private List<WaveData> _waves;
        private int _currentWaveNr;
        [Header("WaveInfo References")]
        [SerializeField]
        WIS_WaveInfoScreenManager _waveInfoManager;



        private GameState _state;

        #region Initialization Methods ==============================================================================================================

        private void Start()
        {
            _state = GameState.Beginning;
            _currentWaveNr = 0;
            _inventorySystem.EnableInventorySystem();
            _playerControls.isActive = false;
            Cursor.lockState = CursorLockMode.Confined;
        }
        #endregion

        #region Public Methods ======================================================================================================================
        public void OnCharacteristicsDistributed()
        {
            _playerControls.isActive = true;
            Cursor.lockState = CursorLockMode.Locked;
            _state = GameState.Countdown;
            _waveInfoManager.CountDown(3);
            _waveInfoManager.ShowWaveNr(_currentWaveNr);

        }
        public void OnCountdownOver()
        {

            _state = GameState.Wave;
            _spawnController.Spawn(_waves[_currentWaveNr]);
        }
        public void OnWaveDone()
        {
            _currentWaveNr++;
            if (_currentWaveNr >= _waves.Count)
            { 
                OnGameWon();
                return;
            }
            Cursor.lockState = CursorLockMode.Confined;
            _playerControls.isActive = false;
            _inventorySystem.EnableInventorySystem();            


        }

        public void OnPlayerDead()
        {
            _state = GameState.Lost;
        }
        public void OnGameWon()
        {
            _state = GameState.Win;
        }


        public void SetCharacteristics(List<int> characteristics)
        {

        }


        #endregion

    }
}
