using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapCube : MonoBehaviour {

	public enum EDirection{
		eUp = 0,
		eDown,
		eLeft,
		eRight,
		eMax
	}

	public enum EMapState{
		eNoMove = 0,
		eStart,
		eMove,
		eEnd
	}

	public int m_X = 0;
	public int m_Y = 0;
	public int m_Cost = 0;
	public EMapState m_State = EMapState.eNoMove;
	public List<OffMeshLink> m_MoveDirection = new List<OffMeshLink>(4);

	// Use this for initialization
	void Start () 
	{
		if(m_MoveDirection.Count != 4){
			Debug.LogError("地圖格子缺少方向,請檢查MapCube");
			return;
		}

	}

	#region 設定座標
	public void SetPos(float _x, float _y)
	{
		m_X = (int)_x;
		m_Y = (int)_y;
		m_Cost = m_X + m_Y;

		float cubeXSize = transform.localScale.x;
		float cubeYSize = transform.localScale.z;
		transform.localPosition = new Vector3(_x * cubeXSize, transform.localPosition.y, _y * cubeYSize);
	}
	#endregion

	#region 設定移動方向
	// 設定移動方向
	public void SetDirection(EDirection _direction, Transform _endTransform)
	{
		OffMeshLink offMeshLink = m_MoveDirection[(int)_direction];
		if(!offMeshLink){
			Debug.LogError("沒有OffMeshLink請檢查: " + gameObject.name);
			return;
		}
		offMeshLink.endTransform = _endTransform;

		if(m_State == EMapState.eNoMove){
			m_State = EMapState.eMove;
		}

		if(MapManager.GetTestMode() == true){
			for(int i = 0; i < offMeshLink.transform.childCount; i++){
				Transform tsChild = offMeshLink.transform.GetChild(i);
				tsChild.gameObject.SetActive(true);
			}
		}
	}

	// 檢查是否可移動的這格
	public bool CheckNextMove(EDirection _direction, int _cost)
	{
		OffMeshLink offMeshLink = m_MoveDirection[(int)_direction];
		if(!offMeshLink){
			Debug.LogError("沒有OffMeshLink請檢查: " + gameObject.name);
			return false;
		}

		if(offMeshLink.endTransform != null){
			return false;
		}

		if(_cost > m_Cost){
			return false;
		}

		return true;
	}
		
	#endregion

}
