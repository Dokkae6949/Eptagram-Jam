using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrackeysJam.Events
{
    public interface IGameEventListener<T>
    {
        void OnEventRaised(T _item);
    }
}
