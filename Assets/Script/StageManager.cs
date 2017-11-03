using UnityEngine;

namespace PolygonMan
{
    public class StageManager : MonoBehaviour
    {
        TitleManager titileManager;
        float depth = 10.0f;

        void Start()
        {
            titileManager = GameObject.Find("TitleManager").GetComponent<TitleManager>();
        }

        RaycastHit2D IsSelected()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            return Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, depth, 1 << LayerMask.NameToLayer("Stage"));
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = IsSelected();

                if (hit && hit.collider.gameObject.Equals(gameObject))
                {
                    titileManager.LoadScene(transform.name);
                }
            }
        }
    }
}
