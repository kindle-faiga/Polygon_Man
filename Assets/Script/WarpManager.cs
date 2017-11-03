using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonMan
{
    public class WarpManager : MonoBehaviour
    {
        [SerializeField]
        List<SpriteRenderer> spriteRenderer = new List<SpriteRenderer>();
        [SerializeField]
        Transform goalPos;
        List<SquareManager> squareManagers = new List<SquareManager>();
        bool isWarp = false;
        float depth = 10.0f;


        void Start()
        {
            foreach (SpriteRenderer s in spriteRenderer)
            {
                s.color = new Color(255, 255, 255, 0.5f);
            }
        }

        public bool GetIsWarp() { return isWarp; }
        public Vector3 GetWarpPos() { return goalPos.position; }

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
            return Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, depth, 1 << LayerMask.NameToLayer("Warp"));
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = IsSelected();

                if (hit && hit.collider.gameObject.Equals(gameObject))
                {
                    if (isWarp)
                    {
                        isWarp = false;

                        foreach (SpriteRenderer s in spriteRenderer)
                        {
                            s.color = new Color(255, 255, 255, 0.5f);
                        }
                    }
                    else
                    {
                        isWarp = true;

                        foreach (SpriteRenderer s in spriteRenderer)
                        {
                            s.color = new Color(255, 255, 255, 1.0f);
                        }

                        foreach(SquareManager sqr in squareManagers)
                        {
                            iTween.MoveTo(sqr.gameObject, goalPos.position, 1.0f);
                        }

                        squareManagers.Clear();
                    }
                }
            }
        }
    }
}
