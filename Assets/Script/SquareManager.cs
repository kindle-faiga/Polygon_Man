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
        bool isGoal = false;

        public string GetPolygon() { return polygon.ToString(); }
        public bool GetIsGoal() { return isGoal; }
        public void ResetState() { isGround = false; isBridge = false; }

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

                    if (polygon.ToString().Equals(p.GetPolygon()) && !p.GetIsGoal() && !isGoal && !polygon.Equals(Polygon.Octagon) && !p.GetPolygon().Equals("Octagon"))
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
                    /*
                    else if(!p.GetIsGoal() && !isGoal)
                    {
                        playerManager.Turn();
                    }
                    */
                    break;
				case "Wall":
					playerManager.Turn();
					break;
                case "Ground":
                    isGround = true;
                    isBridge = false;
                    playerManager.SetIsGround(collision.transform.position);
                    break;
                case "Bridge":
                    isBridge = true;
                    isGround = false;
                    playerManager.SetIsGround(collision.transform.position);
                    //collision.GetComponent<BridgeConnector>().AddPlayer(this);
                    if(collision.GetComponent<ConnectorManager>()) collision.GetComponent<ConnectorManager>().AddPlayer(this);
                    if(collision.transform.parent.GetComponent<ConnectorManager>())collision.transform.parent.GetComponent<ConnectorManager>().AddPlayer(this);
                    break;
                case "Warp":
                    if(collision.GetComponent<WarpManager>().GetIsWarp())
                    {
                        iTween.MoveTo(gameObject, new Vector3 (transform.position.x,collision.GetComponent<WarpManager>().GetWarpPos().y,transform.position.z), 1.0f);
                    }
                    else
                    {
                        collision.GetComponent<WarpManager>().AddPlayer(this);
                    }
                    break;
                case "Goal":
                    if (!collision.GetComponent<GoalManager>().GetIsGoal() && collision.GetComponent<GoalManager>().GetPolygon().Equals(polygon.ToString()))
                    {
                        isGoal = true;
                        transform.position = collision.transform.position;
                        playerManager.Goal();
                        collision.GetComponent<GoalManager>().StageComplete();
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
                    //collision.GetComponent<BridgeConnector>().DeletePlayer(this);
                    if (collision.GetComponent<ConnectorManager>()) collision.GetComponent<ConnectorManager>().DeletePlayer(this);
                    if (collision.transform.parent.GetComponent<ConnectorManager>())collision.transform.parent.GetComponent<ConnectorManager>().DeletePlayer(this);
                    //if (!isGround) playerManager.ResetIsGround();
					break;
                case "Warp":
                    if(!collision.GetComponent<WarpManager>().GetIsWarp())collision.GetComponent<WarpManager>().DeletePlayer(this);
                    break;
                default:
                    break;
            }
        }

        void AddStroke()
        {
			polygon = (Polygon)Enum.ToObject(typeof(Polygon), (int)polygon + 1);

            sprite.sprite = Resources.Load<Sprite>("Sprite/Charactor/" + polygon.ToString());

            playerManager.Spin();
        }
	}
}