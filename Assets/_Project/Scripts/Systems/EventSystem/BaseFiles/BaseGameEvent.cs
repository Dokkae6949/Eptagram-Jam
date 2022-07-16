using System.Collections.Generic;
using UnityEngine;

namespace BrackeysJam.Events
{
    public abstract class BaseGameEvent<T> : ScriptableObject
    {
        private readonly List<IGameEventListener<T>> _eventListeners = new List<IGameEventListener<T>>();

        public void Raise(T _item)
        {
            // trigger every listener :)
            for (int i = _eventListeners.Count - 1; i >= 0; i--)
            {
                _eventListeners[i].OnEventRaised(_item);
            }
        }

        public void RegisterListener(IGameEventListener<T> _listener)
        {
            if (_eventListeners.Contains(_listener)) return;

            _eventListeners.Add(_listener);
        }

        public void UnregisterListener(IGameEventListener<T> _listener)
        {
            if (!_eventListeners.Contains(_listener)) return;

            _eventListeners.Remove(_listener);
        }
    }
}