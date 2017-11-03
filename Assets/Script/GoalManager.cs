using UnityEngine;

namespace PolygonMan
{
    public class GoalManager : MonoBehaviour
    {
		[SerializeField]
		Polygon polygon = Polygon.Triangle;
        GameManager gameManager;

        void Start()
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        public string GetPolygon() 
        {
            return polygon.ToString(); 
        }

        public void StageComplete()
        {
            gameManager.LoadScene();

        }
	}
}