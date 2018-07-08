using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerWalk : MonoBehaviour {

	public Transform m_Target;
	public NavMeshAgent agent;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(agent){
			if(m_Target){
				agent.SetDestination(m_Target.position);
			}
		}
	}
}
