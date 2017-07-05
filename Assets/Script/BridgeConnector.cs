using UnityEngine;

namespace PolygonMan
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class BridgeConnector : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        private float depth = 10.0f;
        private bool isSelected = false;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit2D hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, depth, 1 << LayerMask.NameToLayer("Bridge"));

                if(hit && hit.collider.gameObject.Equals(gameObject))
                {
                    isSelected = true;
                }
			}

			if (Input.GetMouseButtonUp(0))
			{
                spriteRenderer.size = new Vector2(1,1);
                transform.localEulerAngles = Vector3.one;

                isSelected = false;
			}

			if (Input.GetMouseButton(0))
			{
                if (isSelected)
                {
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    spriteRenderer.size = new Vector2(Vector2.Distance(new Vector2(mousePos.x, mousePos.y), new Vector2(transform.position.x, transform.position.y)) * 2, 1);

                    Vector3 worldMousePos = Camera.main.WorldToScreenPoint(transform.position);
                    Quaternion rotation = Quaternion.LookRotation(Vector3.forward, Input.mousePosition - worldMousePos);
                    transform.localRotation = rotation;
                    transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z + 90.0f);
                }
			}
		}
    }
}
