using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager m_intance = null;

	// 暫代角色
	public GameObject m_Player = null;

	void Awake()
	{
		if(m_intance == null){
			m_intance = this;
		}else{
			Debug.LogError("GameManager 重複建立");
			return;
		}
	}

	// Use this for initialization
	void Start () 
	{
		Init();
	}


	public bool Init()
	{
		
		GameInit();


		return true;
	}

	public void GameInit()
	{
		// set map.
		MapManager.m_intance.Init();

		// set player.
		SetPlayer();
	}

	#region 設定出場人物
	private void SetPlayer()
	{

		MapManager.m_intance.SetPlayerPosition(m_Player);
	}

	#endregion


}
