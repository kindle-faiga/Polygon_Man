using UnityEngine;

namespace PolygonMan
{
    public class GoalManager : MonoBehaviour
    {
		[SerializeField]
		Polygon polygon = Polygon.Triangle;
        public string GetPolygon() { return polygon.ToString(); }
	}
}