using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

	public static MapManager m_intance = null;

	public static bool m_TestMode = true; // test use.

	public short m_MapMaxX = 3; // map X count.
	public short m_MapMaxY = 3; // map Y count.
	public short m_MoveLineNumber = 1; // map move line count.
	public MapCube[,] m_Maps;


	private Transform m_map = null;
	private Transform m_cube = null;


	void Awake()
	{
		if(m_intance == null){
			m_intance = this;
		}else{
			Debug.LogError("MapManager 重複建立");
			return;
		}

		m_cube = transform.Find("MapCube");
		if(!m_cube){
			Debug.LogError("找不到MapCube");
			return;
		}

		m_map = transform.Find("Map");
		if(!m_map){
			Debug.LogError("Map");
			return;
		}

		m_Maps = new MapCube[m_MapMaxX, m_MapMaxY];
	}

	public bool Init()
	{
		SetMap();
		return true;
	}


	#region static
	public static bool GetTestMode()
	{
		if(m_intance == null){
			return false;
		}
		return m_TestMode;
	}
	#endregion

	#region 建立地圖
	// 建立地圖格子
	public void SetMap()
	{

		for(int y = 0; y < m_MapMaxY; y++){
			for(int x = 0; x < m_MapMaxX; x++){
				GameObject newMapCube = GameObject.Instantiate(m_cube.gameObject);
				newMapCube.transform.SetParent(m_map);
				newMapCube.name = "x:"+ x +"y:"+ y;
				newMapCube.SetActive(true);

				MapCube mapCube = newMapCube.GetComponent<MapCube>();
				if(!mapCube){
					Debug.LogError("找不到MapCube Component" + newMapCube.name);
					continue;
				}
				mapCube.SetPos(x, y);

				m_Maps[x,y] = mapCube;
			}
		}
		SetMove();
	}
	// 設定可移動方向
	private void SetMove()
	{
		SetMoveLine();
		SetMoveDirection();
	}

	// 設定可移動方向數量
	private void SetMoveLine()
	{
		m_MoveLineNumber = 1;
		return;
	}

	// 計算路徑
	private bool SetMoveDirection()
	{
		// 暫時預設0,0為起點, 最後一點為終點
		MapCube startMap = m_Maps[0,0];
		MapCube endMap = m_Maps[m_MapMaxX -1, m_MapMaxY -1];
		startMap.m_State = MapCube.EMapState.eStart;
		endMap.m_State = MapCube.EMapState.eEnd;

		// 開始計算
		MapCube moveMap = startMap;
		while(moveMap != endMap){
			Debug.LogFormat("move map name: " + moveMap.name);
			for(MapCube.EDirection i = MapCube.EDirection.eUp ; i < MapCube.EDirection.eMax; i++){
				
				MapCube nextMapCube = CheckNext(moveMap, i);
				if(nextMapCube != null){
					moveMap.SetDirection(i, nextMapCube.transform);
					moveMap = nextMapCube;
					break;
				}
			}
		}

		return true;
	}

	private MapCube CheckNext(MapCube _mapCube, MapCube.EDirection _direction)
	{
		int xIndex = _mapCube.m_X;
		int yIndex = _mapCube.m_Y;

		MapCube nextMapCube = null;
		MapCube.EDirection checkDirection = MapCube.EDirection.eUp;
		if(_direction == MapCube.EDirection.eUp){
			yIndex = yIndex - 1;
			if(yIndex < 0){
				return null;
			}
		 	nextMapCube = m_Maps[xIndex, yIndex];
			checkDirection = MapCube.EDirection.eDown;
		}else if(_direction == MapCube.EDirection.eDown){
			yIndex = yIndex + 1;
			if(yIndex >= m_MapMaxY){
				return null;
			}
			nextMapCube = m_Maps[xIndex, yIndex];
			checkDirection = MapCube.EDirection.eUp;
		}else if(_direction == MapCube.EDirection.eLeft){
			xIndex = xIndex - 1;
			if(xIndex < 0){
				return null;
			}
			nextMapCube = m_Maps[xIndex, yIndex];
			checkDirection = MapCube.EDirection.eRight;
		}else if(_direction == MapCube.EDirection.eRight){
			xIndex = xIndex + 1;
			if(xIndex >= m_MapMaxX){
				return null;
			}
			nextMapCube = m_Maps[xIndex, yIndex];
			checkDirection = MapCube.EDirection.eLeft;
		}

		if(nextMapCube.CheckNextMove(checkDirection, _mapCube.m_Cost) == false){
			nextMapCube = null;
		}

		return nextMapCube;
	}

	private Vector2 GetMapStartPosition()
	{
		for(int indexX = 0; indexX < m_MapMaxX; indexX++){
			for(int indexY = 0; indexY < m_MapMaxY; indexY++){
				MapCube mapCube = m_Maps[indexX, indexY];
				if(mapCube == null){
					continue;
				}
					
				if(mapCube.m_State == MapCube.EMapState.eStart){
					return new Vector2(mapCube.m_X, mapCube.m_Y);
				}
			}
		}
		return new Vector2(0, 0);
	}
	#endregion


	#region 設定人物位置.
	public void SetPlayerPosition(GameObject _goPlayer)
	{
		if(_goPlayer == null){
			return;
		}

		_goPlayer.transform.SetParent(m_map);
		Vector2 position = GetMapStartPosition();
		_goPlayer.transform.SetPositionAndRotation(new Vector3(position.x, 5, 0), new Quaternion(0, 0 , 0 ,0));
	}
	#endregion
}
