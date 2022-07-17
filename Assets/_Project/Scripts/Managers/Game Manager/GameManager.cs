using Game.Player;
using Game.WeaponSystem;
using Game.Health;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameManager : StaticInstance<GameManager>
    {
        [Header("PleyrReferences")]
        [SerializeField]
        private PlayerControls _playerControls;
        //[SerializeField]
        //private DiceOverlay _diceOverlay;
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
            _waveInfoManager.OnCountdownFinished += OnCountdownOver;
        }
        #endregion

        #region Public Methods ======================================================================================================================
        public void OnWaveDone()
        {
            _currentWaveNr++;
            if (_currentWaveNr >= _waves.Count)
            { 
                OnGameWon();
                return;
            }

        }

        public void OnPlayerDead()
        {
            _state = GameState.Lost;
        }
        public void OnGameWon()
        {
            _state = GameState.Win;
        }
        public void OnCharacteristicsDistributed()
        {
            //Hide overlay
            _state = GameState.Countdown;
            _waveInfoManager.CountDown(10);
        }

        public void OnCountdownOver() 
        {
            _state = GameState.Wave;
            _spawnController.Spawn(_waves[_currentWaveNr]);
        }
        #endregion

    }
}
