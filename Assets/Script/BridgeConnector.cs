using System.Collections.Generic;
using UnityEngine;

namespace PolygonMan
{
    enum BridgeState {Release, Select, Connect};

[RequireComponent(typeof(SpriteRenderer))]
    public class BridgeConnector : MonoBehaviour
    {
		List<SquareManager> squareManagers = new List<SquareManager>();
        SpriteRenderer spriteRenderer;
		BoxCollider2D col;
        Vector2 defaultColSize;
        Vector2 defaultColOffset;
        float depth = 10.0f;
        BridgeState state;

        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            col = GetComponent<BoxCollider2D>();
            defaultColSize = col.size;
            defaultColOffset = col.offset;
        }

        public void AddPlayer(SquareManager sqr)
        {
            squareManagers.Add(sqr);
        }

		public void DeletePlayer(SquareManager _sqr)
		{
            foreach (SquareManager sqr in squareManagers.ToArray())
            {
                if (sqr.transform.Equals(_sqr.transform))
                {
                    squareManagers.Remove(sqr);
                }
            }
		}

        RaycastHit2D IsSelected()
        {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            return Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, depth, 1 << LayerMask.NameToLayer("Bridge"));
        }

        void SetScale(Vector3 target)
        {
            Vector2 targetPos = new Vector2(target.x, target.y);
			Vector2 position = new Vector2(transform.position.x, transform.position.y);
			spriteRenderer.size = new Vector2(Vector2.Distance(targetPos, position) * 2, 1);
        }

        void SetAngle(Vector3 crum, Vector3 target)
        {
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, target - crum);
			transform.localRotation = rotation;
			transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z + 90.0f);
        }

        void SetCollider(Vector2 size, Vector2 offset, string tagName)
        {
            col.offset = offset;
            col.size = size;
            transform.tag = tagName;
        }

        void Release()
        {
            state = BridgeState.Release;
			spriteRenderer.size = Vector2.one;
			transform.localEulerAngles = Vector3.one;

			foreach (SquareManager sqr in squareManagers.ToArray())
			{
                sqr.transform.GetComponent<PlayerManager>().ResetIsGround();
			}

            squareManagers.Clear();
        }

        void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
                RaycastHit2D hit = IsSelected();

                if(hit && hit.collider.gameObject.Equals(gameObject))
                {
                    switch(state)
                    {
                        case BridgeState.Release:
                            state = BridgeState.Select;
                            break;
                        case BridgeState.Select:
							break;
                        case BridgeState.Connect:
                            Release();
                            SetCollider(col.size, col.offset, "Untagged");
							break;
                        default:
                            break;
                    }
                }
			}

			if (Input.GetMouseButtonUp(0))
			{
                if (state.Equals(BridgeState.Select))
				{
                    RaycastHit2D hit = IsSelected();
                    if (hit && !hit.collider.gameObject.Equals(gameObject))
                    {
                        state = BridgeState.Connect;

                        Vector3 target = hit.collider.transform.position;
                        SetScale(target);
                        SetAngle(transform.position, target);

                        Vector2 size = new Vector2(spriteRenderer.size.x + defaultColSize.x, defaultColSize.y);
                        Vector2 offset = new Vector2(defaultColOffset.x+spriteRenderer.size.x/2, defaultColOffset.y);
                        SetCollider(size, offset, "Bridge");
					}
                    else
                    {
                        Release();
                    }
				}
			}

			if (Input.GetMouseButton(0))
			{
                if (state.Equals(BridgeState.Select))
                {
                    Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    SetScale(mouse);
                    Vector3 worldMouse = Camera.main.WorldToScreenPoint(transform.position);
                    SetAngle(worldMouse, Input.mousePosition);
                }
			}
		}
    }
}
