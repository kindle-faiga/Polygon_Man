using UnityEngine;

namespace PolygonMan
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField]
        bool isRight = true;
        [SerializeField]
        float speed = 0;
		[SerializeField]
		float rotation = 0;
        [SerializeField]
        float gravity = 0;

        bool isSpin = false;
        [SerializeField]
        bool isGround = true;
        Rigidbody2D rigitbody2d;

        public bool GetISRight() { return isRight; }
        public void Turn() { isRight = !isRight; speed = -speed; }

        public void SetIsSpin() { isSpin = true; }
        public void ResetIsSpin() { isSpin = false; }

        public bool GetIsGround() { return isGround; }
        public void SetIsGround(Vector3 groundPos)
        {
            isGround = true;
            transform.position = new Vector3(transform.position.x, groundPos.y, 0);
        }
        public void ResetIsGround(){ isGround = false; }

        void Start()
        {
            rigitbody2d = GetComponent<Rigidbody2D>();
            if (!isRight) speed = -speed;
        }

        void FixedUpdate()
        {
			if (isGround)
			{
				rigitbody2d.velocity = new Vector2(speed, 0);
			}
			else
			{
				rigitbody2d.velocity = new Vector2(0, -gravity);
			}

            if (isSpin)
            {
				float angle = Time.time * (isRight ? -rotation : rotation);
                Debug.Log(angle);
				transform.eulerAngles = new Vector3(0, 0, angle);
            }
        }
    }
}