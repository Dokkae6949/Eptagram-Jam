using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Generic
{
    public static class Helper
    {
        public static bool InRange(Vector3 origin, Vector3 target, float maxDistance)
        {
            Vector3 difference = new Vector3(
              target.x - origin.x,
              target.y - origin.y,
              target.z - origin.z);
            float sqrDistance =
              Mathf.Pow(difference.x, 2f) +
              Mathf.Pow(difference.y, 2f) +
              Mathf.Pow(difference.z, 2f);

            return sqrDistance <= Mathf.Pow(maxDistance, 2);
        }

        public static bool CanSee(Vector3 origin, Vector3 target, float maxDistance, string target_tag)
        {
            RaycastHit rayResult;

            bool hasHit = Physics.Raycast(origin, (target - origin), out rayResult, maxDistance);

            return hasHit && rayResult.transform.tag == target_tag;
        }
    }
}
