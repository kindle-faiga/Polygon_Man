using System.Collections.Generic;
using UnityEngine;

namespace PolygonMan
{
    public class SpeedManager : MonoBehaviour
    {
        [SerializeField]
        List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();
        int speedCount = 2;
        float depth = 10.0f;

        void Start()
        {
            spriteRenderers[2].enabled = false;
        }

        public int GetSpeedCount() { return speedCount; }

        RaycastHit2D IsSelected()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            return Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, depth, 1 << LayerMask.NameToLayer("Option"));
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = IsSelected();

                if (hit && hit.collider.gameObject.Equals(gameObject))
                {
                    if (speedCount < 3)
                    {
                        spriteRenderers[speedCount].enabled = true;
                        ++speedCount;
                    }
                    else
                    {
                        spriteRenderers[0].enabled = false;
                        spriteRenderers[1].enabled = false;
                        spriteRenderers[2].enabled = false;
                        speedCount = 0;
                    }

                    foreach (GameObject g in GameObject.FindGameObjectsWithTag("Polygon"))
                    {
                        g.GetComponent<PlayerManager>().ChangeSpeed(speedCount);
                    }
                }
            }
        }
    }
}
