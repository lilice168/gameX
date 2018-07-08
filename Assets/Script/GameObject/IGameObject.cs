using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IGameObject : MonoBehaviour {

	public NavMeshAgent m_Agent;

	// Use this for initialization
	void Start () {
		m_Agent = GetComponent<NavMeshAgent>();
	}
}
