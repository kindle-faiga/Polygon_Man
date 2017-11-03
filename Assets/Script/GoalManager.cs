using UnityEngine;

namespace PolygonMan
{
    public class GoalManager : MonoBehaviour
    {
		[SerializeField]
		Polygon polygon = Polygon.Triangle;
        GameManager gameManager;
        bool isGoal = false;

        void Start()
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        public bool GetIsGoal()
        {
            return isGoal;
        }

        public string GetPolygon() 
        {
            return polygon.ToString(); 
        }

        public void StageComplete()
        {
            isGoal = true;
            gameManager.LoadScene();
        }
	}
}