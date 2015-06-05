using UnityEngine;
using System.Collections;

public class PoppyStart : MonoBehaviour {

	float x;
	float y;
	float z;
	Vector3 pos;
	
	void Start()
	{
		x = Random.Range(-15, 15);
		y = 0;
		z = Random.Range(-15, 15);
		pos = new Vector3(x, y, z);
		transform.position = pos;
	}
}