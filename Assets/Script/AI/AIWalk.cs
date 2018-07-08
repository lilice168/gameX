using UnityEngine.AI;
using UnityEngine;


public class AIWalk : IAI {

	public Transform[] m_targets;

	public AIWalk()
	{
		m_State = EState.eWalk;
	}

	#region AI 基礎架構
	public override void Init(IGameObject gameobj)
	{

		m_GameObj = gameobj;

		GameObject point = GameObject.Find("Scene/point");
		m_targets = point.GetComponentsInChildren<Transform>();
	}

	public override void Begin()
	{
		GetTarget();
	}

	public override void Action()
	{
		if(m_GameObj){
			if(m_Target){
				m_GameObj.m_Agent.SetDestination(m_Target.position);
			}
		}
	}

	public override void End()
	{
	}

	public override EState NextState()
	{

		return EState.eWalk;
	}

	#endregion


	#region AiWalk自身函式
	private void GetTarget()
	{
		if(m_Target == null){
			m_Target = m_targets[1];
		}

		if(CheckWalkEnd()){
			
			int rand = Random.Range(1, m_targets.Length);
			m_Target = m_targets[rand];
		}
	}


	private bool CheckWalkEnd()
	{

		if(m_Target.position.x == m_GameObj.transform.position.x &&
		   m_Target.position.z == m_GameObj.transform.position.z ){
			return true;
		}
		return false;
	}
	#endregion
}
