using UnityEngine;
using UnityEngine.Events;

namespace BrackeysJam.Events
{
    public abstract class BaseGameEventListener<T, E, UER> : MonoBehaviour,
        IGameEventListener<T> where E : BaseGameEvent<T> where UER : UnityEvent<T>
    {
        [SerializeField] private E _gameEvent;
        [SerializeField] private UER _unityEventResponse;

        public E GameEvent { get { return _gameEvent; } set { _gameEvent = value; } }


        private void OnEnable()
        {
            if (_gameEvent == null) { Debug.Log("Doing the Funny"); return; }

            GameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            if (gameObject == null) return;

            GameEvent.UnregisterListener(this);
        }

        public void OnEventRaised(T _item)
        {
            if (_unityEventResponse == null) return;

            _unityEventResponse.Invoke(_item);
        }
    }
}