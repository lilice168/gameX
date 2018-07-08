using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIManager : MonoBehaviour {

	public IAI m_AI;
	public IAI.EState m_State;
	public IGameObject m_GameObj;

	// Use this for initialization
	void Start () {

		m_AI = new AIIdle();
		m_GameObj = GetComponent<IGameObject>();
		Time.timeScale =2.0f; // 加速.
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_AI == null){
			Debug.LogError(" None AI. ");
			return;
		}

		// 更新AI目前狀態
		UpdateState();

		// AI行動
		m_AI.AIUpdate();
	}


	#region 判斷AI狀態
	public void UpdateState()
	{

		IAI.EState state = NextState();
		if(state != m_State){

			switch(state){
			case IAI.EState.eIdle:
				m_AI = new AIIdle();
				break;
			case IAI.EState.eWalk:
				m_AI = new AIWalk();
				break;
			case IAI.EState.eBattle:
				m_AI = new AIBattle();
				break;
			case IAI.EState.eDestroy:
				m_AI = new AIDestroy();
				break;
			default:
				break;
			}

			m_AI.Init(m_GameObj);
		}

		// 取得目前ＡＩ狀態
		m_State = m_AI.GetState();
	}

	private IAI.EState NextState()
	{
		return m_AI.NextState();
	}

	#endregion
}
