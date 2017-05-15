using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorManager : MonoBehaviour 
{
	[SerializeField]
	private bool isRight = true;
	[SerializeField] 
	private float length = 0;
	[SerializeField]
	private float rotation = 100;
	private List<Transform> wheels = new List<Transform>();

	void Start () 
	{
		GameObject wheelObject = Resources.Load("Prefab/Wheel") as GameObject;

		Vector3 pos = transform.position;

		float size = wheelObject.transform.localScale.sqrMagnitude;

		if (!isRight)
		{
			size = -size;
		}

		for (int i = 0; i < length; ++i)
		{
			Vector3 p = new Vector3(pos.x + size*i, pos.y, pos.z);

			GameObject wheel = Instantiate(wheelObject, p, transform.rotation) as GameObject;

			wheel.transform.parent = transform;

			wheels.Add(wheel.transform);
		}
	}

	void FixedUpdate()
	{
		float angle = Time.time * (isRight ? -rotation : rotation);

		foreach(Transform wheel in wheels)
		{
			wheel.eulerAngles = new Vector3(0, 0, angle);
		}
	}
}
