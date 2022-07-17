using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class AudioSystem : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private float _panWaveScale;


        private void Update()
        {
            _audioSource.panStereo = Mathf.Sin(Time.time) * _panWaveScale;
        }
    }
}
