using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class RotateTowardsPlayer : MonoBehaviour
    {
        private Transform _target;


        private void Start()
        {
            _target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        private void Update()
        {
            Vector3 targetPos = new Vector3(_target.position.x, transform.position.y, _target.position.z);

            transform.LookAt(targetPos, Vector3.up);
        }
    }
}
