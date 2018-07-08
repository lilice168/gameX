using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class IAI {

	public enum EState
	{
		eError = -1, // 錯誤
		eIdle = 0, // 等待階段
		eWalk, // 行動階段
		eBattle, // 戰鬥階段
		eDestroy // 結束階段
	}

	public Transform m_Target;
	public IGameObject m_GameObj;

	// AI狀態
	public EState m_State = EState.eError;
	public EState GetState(){ return m_State;}
	public abstract EState NextState();

	// abstract AI行為
	public abstract void Init(IGameObject gameobj);
	public abstract void Begin();
	public abstract void Action();
	public abstract void End();


	public void AIUpdate()
	{
		Begin();
		Action();
		End();
	}

}
