using System.Collections.Generic;
using UnityEngine;

namespace PolygonMan
{
    public class GimmickManager : MonoBehaviour
    {
        [SerializeField]
        List<Transform> parts = new List<Transform>();

		public List<Transform> GetParts()
		{
			return parts;
		}

        void Start()
        {
            foreach (Transform p in transform.GetComponentsInChildren<Transform>())
            {
                parts.Add(p.transform);
            }
        }
    }
}