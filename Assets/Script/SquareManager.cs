using System;
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
        bool isGround = true;
        bool isBridge = false;

        public string GetPolygon() { return polygon.ToString(); }

		void Start()
		{
			sprite = GetComponent<SpriteRenderer>();
			playerManager = GetComponent<PlayerManager>();
		}

        void OnTriggerEnter2D(Collider2D collision)
        {
            switch (collision.tag)
            {
                case "Polygon":
                    SquareManager p = collision.GetComponent<SquareManager>();

                    if (polygon.ToString().Equals(p.GetPolygon()))
                    {
                        Vector3 pos = transform.position;
                        Vector3 anotherPos = collision.transform.position;

                        if (anotherPos.y < pos.y)
                        {
                            AddStroke();
                            Destroy(collision.gameObject);
                        }
                        else
                        {
                            if (pos.y.Equals(anotherPos.y) && pos.x < anotherPos.x)
                            {
                                AddStroke();
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
				case "Wall":
					playerManager.Turn();
					break;
                case "Ground":
                    isGround = true;
                    playerManager.SetIsGround(collision.transform.position);
                    break;
                case "Bridge":
                    isBridge = true;
                    playerManager.SetIsGround(collision.transform.position);
                    collision.GetComponent<BridgeConnector>().AddPlayer(this);
                    break;
                case "Goal":
                    if (collision.GetComponent<GoalManager>().GetPolygon().Equals(polygon.ToString()))
                    {
                        transform.position = collision.transform.position;
                        playerManager.Goal();
                    }
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
                    isGround = false;
                    if(!isBridge)playerManager.ResetIsGround();
					break;
				case "Bridge":
                    isBridge = false;
                    collision.GetComponent<BridgeConnector>().DeletePlayer(this);
                    if (!isGround) playerManager.ResetIsGround();
					break;
                default:
                    break;
            }
        }

        void AddStroke()
        {
			polygon = (Polygon)Enum.ToObject(typeof(Polygon), (int)polygon + 1);

            sprite.sprite = Resources.Load<Sprite>("Sprite/" + polygon.ToString());

            playerManager.Spin();
        }
	}
}