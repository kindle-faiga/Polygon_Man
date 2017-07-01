using System;
using System.Collections;
using UnityEngine;

namespace PolygonMan
{
    enum Polygon {Triangle, Diamond, Pentagon, Hexagon, Heptagon, Octagon};

    [RequireComponent(typeof(PlayerManager))]
    public class SquareManager : MonoBehaviour
    {
        [SerializeField]
        Polygon polygon = Polygon.Triangle;

        SpriteRenderer sprite;
        PlayerManager playerManager;

        public string GetPolygon() { return polygon.ToString(); }

		void Start()
		{
			sprite = GetComponent<SpriteRenderer>();
			playerManager = GetComponent<PlayerManager>();
		}

        void OnTriggerEnter2D(Collider2D collision)
        {
            switch(collision.tag)
            {
                case "Polygon":
					SquareManager p = collision.GetComponent<SquareManager>();

					if (polygon.ToString().Equals(p.GetPolygon()))
					{
						Vector3 pos = transform.position;
						Vector3 anotherPos = collision.transform.position;

                        if (anotherPos.y < pos.y)
						{
							StartCoroutine(AddStroke());
							Destroy(collision.gameObject);
						}
						else
						{
							if (pos.y.Equals(anotherPos.y) && pos.x < anotherPos.x)
							{
								StartCoroutine(AddStroke());
								Destroy(collision.gameObject);
							}
							else
							{
								Destroy(gameObject);
							}
						}
					}
					else
					{
						playerManager.Turn();
					}
                    break;
                case "Ground":
                    playerManager.SetIsGround(collision.transform.position);
                    break;
                default:
                    break;
            }
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            switch (collision.tag)
            {
                case "Ground":
                    playerManager.ResetIsGround();
                    break;
                default:
                    break;
            }
        }

        IEnumerator AddStroke()
        {
			polygon = (Polygon)Enum.ToObject(typeof(Polygon), (int)polygon + 1);

            sprite.sprite = Resources.Load<Sprite>("Sprite/" + polygon.ToString());

            playerManager.SetIsSpin();

            yield return new WaitForSeconds(0.5f);

            playerManager.ResetIsSpin();
        }
	}
}