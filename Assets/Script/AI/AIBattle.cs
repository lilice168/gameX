

public class AIBattle : IAI {


	public AIBattle()
	{
		m_State = EState.eBattle;
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
		return EState.eBattle;
	}
	#endregion
}
