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

			if (Input.GetMouseButton(0))
			{
				Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                gimmickManager.GetParts()[1].GetComponent<SpriteRenderer>().size = new Vector2(Vector2.Distance(new Vector2(mousePos.x,mousePos.y), new Vector2(gimmickManager.GetParts()[1].position.x, gimmickManager.GetParts()[1].position.y)) * 2,1);

                Vector3 worldMousePos = Camera.main.WorldToScreenPoint(gimmickManager.GetParts()[1].position);
                Quaternion rotation = Quaternion.LookRotation(Vector3.forward, Input.mousePosition - worldMousePos);
				gimmickManager.GetParts()[1].localRotation = rotation;
                gimmickManager.GetParts()[1].localEulerAngles = new Vector3(0, 0, gimmickManager.GetParts()[1].localEulerAngles.z + 90.0f);
			}
        }
    }
}