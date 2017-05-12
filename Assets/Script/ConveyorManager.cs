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
	private float size = 0.5f;

	void Start () 
	{
		GameObject wheelObject = Resources.Load("Prefab/Wheel") as GameObject;

		Vector3 pos = transform.position;

		if (!isRight)
		{
			size = -size;
		}

		for (int i = 0; i < length; ++i)
		{
			Vector3 p = new Vector3(pos.x + size*i, pos.y, pos.z);

			GameObject wheel = Instantiate(wheelObject, p, transform.rotation) as GameObject;

			wheel.transform.parent = transform;
		}
	}
}
