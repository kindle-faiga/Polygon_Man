using UnityEngine;

namespace PolygonMan
{
    public class CountManager : MonoBehaviour
    {
        [SerializeField]
        int count = 0;
        SpriteRenderer spriteRenderer;

        void Start()
        {
            spriteRenderer = GameObject.Find("UI/CountManager/Number").GetComponent<SpriteRenderer>();
        }

        public int GetCount()
        {
            return count;
        }

        public void UpdateCount()
        {
            if (0 < count)
            {
                --count;
            }

            spriteRenderer.sprite = Resources.Load<Sprite>("Sprite/Number/" + count.ToString());
        }
    }
}