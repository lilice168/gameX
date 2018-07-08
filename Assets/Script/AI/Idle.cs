

public class AIIdle : IAI {


	public AIIdle()
	{
		m_State = EState.eIdle;
	}

	#region AI 基礎架構
	public int m_time;
	
	public override void Init(IGameObject gameobj)
	{
		m_GameObj = gameobj;
	}

	public override void Begin()
	{
	}

	public override void Action()
	{
		m_time++;
	}

	public override void End()
	{
	}

	public override EState NextState()
	{
		if(m_time > 3){
			return EState.eWalk;
		}

		return EState.eIdle;
	}
	#endregion
}
