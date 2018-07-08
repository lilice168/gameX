

public class AIDestroy : IAI {

	public AIDestroy()
	{
		m_State = EState.eDestroy;
	}

	#region AI 基礎架構
	public override void Init(IGameObject gameobj)
	{
		m_GameObj = gameobj;
	}

	public override void Begin()
	{
	}

	public override void Action()
	{
	}

	public override void End()
	{
	}


	public override EState NextState()
	{
		return EState.eDestroy;
	}
	#endregion
}
