using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonMan
{
    enum Gimmick {Wall, Bridge};

    public class WallManager : MonoBehaviour
    {
        [SerializeField]
        Gimmick gimmick = Gimmick.Wall;
        ConnectorManager connnectorManager;
        List<WallConnector> wallConnectors = new List<WallConnector>();
        bool isExpansion = false;
        float depth = 10.0f;

        void Start()
        {
            if(gimmick.Equals(Gimmick.Bridge))connnectorManager = GetComponent<ConnectorManager>();

            foreach (WallConnector w in GetComponentsInChildren<WallConnector>())
            {
                wallConnectors.Add(w);
            }
        }

        RaycastHit2D IsSelected()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            return Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, depth, 1 << LayerMask.NameToLayer("Wall"));
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = IsSelected();

                if (hit && hit.collider.gameObject.Equals(gameObject))
                {
                    if (isExpansion)
                    {
                        isExpansion = false;

                        if(gimmick.Equals(Gimmick.Bridge))
                        {
                            connnectorManager.Release();
                            transform.tag = "Untagged";
                        }

                        foreach (WallConnector w in wallConnectors)
                        {
                            w.ResetPosition();
                        }
                    }
                    else
                    {
                        isExpansion = true;

                        if (gimmick.Equals(Gimmick.Bridge))
                        {
                            transform.tag = gimmick.ToString();
                        }

                        foreach (WallConnector w in wallConnectors)
                        {
                            w.SetPosition(gimmick.ToString());
                        }
                    }
                }
            }
        }
    }
}
