using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonMan
{
    public class PlayerCreater : MonoBehaviour
    {
        [SerializeField]
        GameObject playerObject;
        [SerializeField]
        float createTime = 1.0f;
        [SerializeField]
        int maxCount = 1;
        int count = 0;

        void Start()
        {
            StartCoroutine(WaitForCreate());
        }

        IEnumerator WaitForCreate()
        {
            GameObject p = Instantiate(playerObject, transform.position, transform.rotation) as GameObject;

            p.GetComponent<PlayerManager>().ResetState();
            p.GetComponent<SquareManager>().ResetState();

            yield return new WaitForSeconds(createTime);

            if (count < maxCount)
            {
                ++count;
                StartCoroutine(WaitForCreate());
            }
        }
    }
}