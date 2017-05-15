using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeManager : MonoBehaviour 
{
	[SerializeField]
	private List<Transform> joints = new List<Transform>();

	void Start () 
	{
		for (int i = 0; i < transform.childCount; ++i)
		{
			joints.Add(transform.GetChild(i));
		}
	}
}
