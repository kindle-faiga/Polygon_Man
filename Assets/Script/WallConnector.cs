using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonMan
{
    public class WallConnector : MonoBehaviour
    {
        Vector3 defaultPos;
        BoxCollider2D boxCollider;

        void Start()
        {
            defaultPos = transform.position;
            transform.position = transform.parent.position;
            boxCollider = GetComponent<BoxCollider2D>();
            boxCollider.enabled = false;
        }

        public void SetPosition(string tagName)
        {
            transform.tag = tagName;
            boxCollider.enabled = true;
            iTween.MoveTo(gameObject, defaultPos, 1.0f);
        }

        public void ResetPosition()
        {
            transform.tag = "Untagged";
            boxCollider.enabled = false;
            iTween.MoveTo(gameObject, transform.parent.position, 1.0f);
        }
    }
}
