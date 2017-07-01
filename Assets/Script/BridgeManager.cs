using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonMan
{
    [RequireComponent(typeof(GimmickManager))]
    public class BridgeManager : MonoBehaviour
    {
        GimmickManager gimmickManager;

        private void Start()
        {
            gimmickManager = GetComponent<GimmickManager>();
        }
    }
}