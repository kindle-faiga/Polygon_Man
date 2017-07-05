using UnityEngine;

namespace PolygonMan
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(LineRenderer))]
    public class BridgeConnector : MonoBehaviour
    {
        LineRenderer lineRenderer;

        void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
    }
}
