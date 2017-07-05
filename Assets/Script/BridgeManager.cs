using UnityEngine;

namespace PolygonMan
{
    [RequireComponent(typeof(GimmickManager))]
    public class BridgeManager : MonoBehaviour
    {
        GimmickManager gimmickManager;

        void Start()
        {
            gimmickManager = GetComponent<GimmickManager>();
        }
    }
}