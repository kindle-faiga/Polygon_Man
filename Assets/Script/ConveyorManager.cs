using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonMan
{
    [RequireComponent(typeof(GimmickManager))]
    public class ConveyorManager : MonoBehaviour
    {
        GimmickManager gimmickManager;

        private void Start()
        {
            gimmickManager = GetComponent<GimmickManager>();
        }
    }
}