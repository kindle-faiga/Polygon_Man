using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonMan
{
    public class ConnectorManager : MonoBehaviour
    {

        List<SquareManager> squareManagers = new List<SquareManager>();

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

        public void Release()
        {
            foreach (SquareManager sqr in squareManagers.ToArray())
            {
                sqr.transform.GetComponent<PlayerManager>().ResetIsGround();
                sqr.transform.GetComponent<SquareManager>().ResetState();
            }

            squareManagers.Clear();
        }
    }
}
