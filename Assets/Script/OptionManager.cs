using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonMan
{
    public class OptionManager : MonoBehaviour
    {
        [SerializeField]
        string sceneName = "Title";
        GameManager gameManager;
        float depth = 10.0f;

        void Start()
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

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
                    gameManager.LoadScene(sceneName);
                }
            }
        }
    }
}
