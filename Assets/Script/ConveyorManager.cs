using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ConveyorManager : MonoBehaviour 
{
	[SerializeField]
	bool isRight = true;
	[SerializeField] 
	float length = 0;
	[SerializeField]
	float rotation = 100;
	List<Transform> wheels = new List<Transform>();

	void Awake () 
	{
		GameObject wheelObject = Resources.Load("Prefab/Wheel") as GameObject;

        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();

		Vector3 pos = transform.position;

		float size = wheelObject.transform.localScale.sqrMagnitude;

		for (int i = 0; i < length; ++i)
		{
			Vector3 p = new Vector3(pos.x + (isRight ? 1 : -1)*size*i, pos.y, pos.z);

			GameObject wheel = Instantiate(wheelObject, p, transform.rotation) as GameObject;

			wheel.transform.parent = transform;

			wheels.Add(wheel.transform);
		}

        boxCollider.size = new Vector2(length*size, size);
        boxCollider.offset = new Vector2((isRight ? 1:-1)*(length-1)*size/2, 0);
	}

	void FixedUpdate()
	{
        /*
		float angle = Time.time * (isRight ? -rotation : rotation);

		foreach(Transform wheel in wheels)
		{
			wheel.eulerAngles = new Vector3(0, 0, angle);
		}
		*/
	}
}
