using UnityEngine;

namespace PolygonMan
{
    [RequireComponent(typeof(GimmickManager))]
    public class BridgeManager : MonoBehaviour
    {
        GimmickManager gimmickManager;
        private float depth = 10.0f;

        void Start()
        {
            gimmickManager = GetComponent<GimmickManager>();
        }

        void Update()
        {
			if (Input.GetMouseButtonDown(0))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, depth, 1 << LayerMask.NameToLayer("Bridge"));

				if (hit.collider)
				{
					Debug.Log(hit.collider.gameObject.name);
				}
			}
        }
    }
}