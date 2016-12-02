public class EventId
{
    // 心跳
    public const string HeartBeat = "HeartBeat";

    // Event AnimationController to  UnitController
    public const string AttackAnimationEnd = "AttackAnimationEnd";
    public const string DieAnimationEnd = "DieAnimationEnd";
    public const string AttackAnimationEffect = "AttackAnimationEffect";
    // Event UnitController to  AnimationController

    //Event SquadController to UnitController
    public const string SomeOneDie = "SomeOneDie";
    public const string EventConcentratedFire = "EventConcentratedFire";
    public const string RefreshEnemyList = "RefreshEnemyList";
    public const string FindAndAttackEnemy = "FindAndAttackEnemy";
    public const string UnitMarching = "UnitMarching";

    //Event Arrow to UnitController
    public const string ArrowDamageEffect = "ArrowDamageEffect";
}
