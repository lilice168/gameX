using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move : MonoBehaviour {

	float speed = 100.0f;

	float x;
	float y;
	float z;

	NavMeshAgent navMeshAgent =null;

	// Use this for initialization
	void Start () {
		navMeshAgent = transform.GetComponent<NavMeshAgent>();

		if(navMeshAgent == null){
			Debug.LogError("NavMeshAgent null");
			return;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButtonDown(0) == true){



			y = Input.GetAxis("Mouse X") * Time.deltaTime * speed;

			x = Input.GetAxis("Mouse Y") * Time.deltaTime * speed;


			transform.Rotate(new Vector3(x,y,0));

			navMeshAgent.Move(new Vector3(x,y,0));

		}


		//Time.deltaTime 
	}
}
