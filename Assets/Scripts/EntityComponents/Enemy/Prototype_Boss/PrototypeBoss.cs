using UnityEngine;

public class PrototypeBoss : Enemy
{
    #region States

    public PrototypeBoss_IdleState IdleState { get; private set; }
    public PrototypeBoss_MoveState MoveState { get; private set; }
    public PrototypeBoss_BattleState BattleState { get; private set; }
    public PrototypeBoss_AttackState AttackState { get; private set; }
    //public PrototypeBoss_StunnedState StunnedState { get; private set; }
    public PrototypeBoss_DeadState DeadState { get; private set; }
    public PrototypeBoss_TeleportState TeleportState { get; private set; }

    #endregion

    [Header ("Teleport details")]
    [SerializeField] private Vector2 surroundingCheckSize;
    [SerializeField] private float yOffset;
    private Vector2 previousPosition;

    protected override void Awake()
    {
        base.Awake();

        SetupDefaultFacingDir(-1);

        IdleState = new PrototypeBoss_IdleState(this, StateMachine, "Idle", this);
        MoveState = new PrototypeBoss_MoveState(this, StateMachine, "Move", this);
        BattleState = new PrototypeBoss_BattleState(this, StateMachine, "Move", this);
        AttackState = new PrototypeBoss_AttackState(this, StateMachine, "Attack", this);
        //StunnedState = new PrototypeBoss_StunnedState(this, StateMachine, "Stunned", this);
        DeadState = new PrototypeBoss_DeadState(this, StateMachine, "Dead", this);
        TeleportState = new PrototypeBoss_TeleportState(this, StateMachine, "Teleport", this);
    }

    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(IdleState);
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Die()
    {
        base.Die();
        StateMachine.ChangeState(DeadState);
    }

    public void FindPosition()
    {
        Player p = PlayerManager.instance.player;
        int dir = p.FacingDir;
        Vector3 playerPos = p.transform.position;

        previousPosition = transform.position;
        transform.position = playerPos + new Vector3(-dir * 2, yOffset, 0);

        if(IsTeleportNotValid())
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, transform.position.z);

            if (IsTeleportNotValid())
            {
                //transform.position = playerPos;
                transform.position = previousPosition;
            }
        }
    }

    private bool IsTeleportNotValid()
    {
        return !GroundBelow() || SomethingIsAround();
    }

    private RaycastHit2D GroundBelow() => Physics2D.Raycast(transform.position, Vector2.down, 100, whatIsGround);
    private bool SomethingIsAround() => Physics2D.BoxCast(transform.position, surroundingCheckSize, 0, Vector2.zero, 0 , whatIsGround);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - GroundBelow().distance));
        Gizmos.DrawWireCube(transform.position, surroundingCheckSize);
    }
}
