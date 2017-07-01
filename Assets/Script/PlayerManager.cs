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
        [SerializeField]
		float spinTime = 1.0f;

        bool isGround = true;
        float spinRange = 0;
        Rigidbody2D rigitbody2d;

		public bool GetISRight() { return isRight; }
		public bool GetIsGround() { return isGround; }
		public void SetIsGround(Vector3 groundPos)
		{
			isGround = true;
			transform.position = new Vector3(transform.position.x, groundPos.y, 0);
		}
		public void ResetIsGround() { isGround = false; }

		public void Turn()
		{
            isRight = !isRight; 
            speed = -speed;
		}

		public void Spin()
		{
            spinRange = Time.time;
		}

        void Start()
        {
            rigitbody2d = GetComponent<Rigidbody2D>();
            if (!isRight) speed = -speed;
            spinRange = Time.time;
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

            float angle = (Time.time - spinRange) * spinTime;
            transform.eulerAngles = Vector3.Slerp(Vector3.zero, new Vector3(0, 0, 360), angle);
        }
    }
}