public class EventId
{
    // 心跳
    public const string HeartBeat = "HeartBeat";

    // Event AnimationController to  UnitController

    //public const string AttackAnimationEnd = "AttackAnimationEnd";
    //public const string DieAnimationEnd = "DieAnimationEnd";
    //public const string AttackAnimationEffect = "AttackAnimationEffect";

    //Event SquadController to UnitController

    //public const string MemberDied = "MemberDied";
    //public const string RefreshEnemyList = "RefreshEnemyList";
    //public const string FindAndAttackEnemy = "FindAndAttackEnemy";
    //public const string UnitMarching = "UnitMarching";

    //Event Arrow to UnitController

    //public const string ArrowDamageEffect = "ArrowDamageEffect";
    //Event UnitController to UnitController
    public const string SomeOneDie = "SomeOneDie";
    //public const string ForceAttack = "ForceAttack";

    //Event SquadController to SquadController
    public const string SomeSquadDie = "SomeSquadDie";
    //Event Embattling to Squad
    public const string ReSetPosition = "ReSetPosition";
    //Event TestFPS to BattleController
    public const string LoadSquad = "LoadSquad";
    public const string StartBattle = "StartBattle";
    public const string StartFight = "StartFight";
    public const string SendMarch = "SendMarch";
    public const string UseSkill = "UseSkill";
    public const string AutoFight = "AutoFight";
    //Event from Handler to BattleManager
    public const string ReceiveFight = "ReceiveFight";
    public const string ReceiveSkill = "ReceiveSkill";
    public const string ReceiveBattleInfor = "ReceiveBattleInfor";
    public const string ReceiveSquadPosInfor = "ReceiveSquadPosInfor";
    public const string ReceiveCommandPursue = "ReceiveCommandPursue";
    public const string ReceivePrepareForAttack = "ReceivePrepareForAttack";
    public const string ReceiveObjGetHurt = "ReceiveObjGetHurt";
    public const string ReceiveBattleEnd = "ReceiveBattleEnd";
    //Lua test event
    public const string LuaTestUpdate = "BagUpdated";

    public const string WorldMarchDataUpdate = "WorldMarchDataUpdate";
    public const string WorldCityDataUpdate = "WorldCityDataUpdate";

	public const string CreateRole = "CreateRole";

	public const string PlayerProxyUpdate = "PlayerProxyUpdate";

	public const string BuildingSelected = "BuildingSelected";
	public const string BuildingLevelUp = "BuildingLevelUp";
	public const string AssignHero = "AssignHero";
	public const string TroopTrain = "TroopTrain";
	public const string TroopTrainOver = "TroopTrainOver";

	public const string MapBlockSelected = "MapBlockSelected";
	public const string ReceiveSendMarchMsg = "ReceiveSendMarchMsg";
	public const string ReceiveUpdateMarchMsg = "ReceiveUpdateMarchMsg";
	public const string ReceiveAllMarchMsg = "ReceiveAllMarchMsg";

}
