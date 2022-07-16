using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.WeaponSystem
{
    public abstract class WeaponBasic : MonoBehaviour
    {
        virtual public float GetMaxShootingRange() { return 0f; }

        virtual public void Shoot() { }
    }
}
