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
        [SerializeField]
        private MonsterTracker _monsterTracker;
        [Header("WaveInfo References")]
        [SerializeField]
        WIS_WaveInfoScreenManager _waveInfoManager;

        [Header("Canvases")]
        [SerializeField]
        private CanvasGroup _winCanvas;
        [SerializeField]
        private CanvasGroup _looseCanvas;




        private GameState _state;

        #region Initialization Methods ==============================================================================================================

        private void Start()
        {
            _winCanvas.alpha = 0f;
            _winCanvas.interactable = false;
            _looseCanvas.alpha = 0f;
            _looseCanvas.interactable = false;

            _state = GameState.Beginning;
            _currentWaveNr = 0;
            _inventorySystem.EnableInventorySystem();
            _playerControls.isActive = false;
            Cursor.lockState = CursorLockMode.None;
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
            _monsterTracker.SpawnNewWave(_waves[_currentWaveNr].GetEnemyNumber());
        }
        public void OnWaveDone()
        {
            _currentWaveNr++;
            if (_currentWaveNr >= _waves.Count)
            { 
                OnGameWon();
                return;
            }
            Cursor.lockState = CursorLockMode.None;
            _playerControls.isActive = false;
            _inventorySystem.EnableInventorySystem();            


        }

        public void OnPlayerDead()
        {
            _playerControls.isActive = false;
            Cursor.lockState = CursorLockMode.None;
            _state = GameState.Lost;
            _looseCanvas.alpha = 1f;
            _looseCanvas.interactable = true;
        }
        public void OnGameWon()
        {
            _playerControls.isActive = false;
            Cursor.lockState = CursorLockMode.None;
            _state = GameState.Win;
            Debug.Log("You won!");
            _winCanvas.alpha = 1f;
            _winCanvas.interactable = true;
        }


        public void SetCharacteristics(List<int> characteristics)
        {
            _weaponScript.SetDamageMultiplier(Mathf.Clamp(characteristics[0] * 0.5f, 1f, 1000f));
            _healthSystem.SetDefence(Mathf.Clamp(characteristics[1] * 0.5f, 1f, 1000f));
            _playerControls.SetAccelerationMultiplier(Mathf.Clamp(characteristics[2] * 0.5f, 1f, 1000f));
            _weaponScript.SetFireRate(Mathf.Clamp(characteristics[3] * 0.5f, 1f, 1000f));
        }


        #endregion

    }
}
