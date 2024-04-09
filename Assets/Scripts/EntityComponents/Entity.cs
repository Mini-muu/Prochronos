using System.Collections;
using UnityEditor.Build.Pipeline;
using UnityEngine;


public class Entity : MonoBehaviour
{
    #region Infos

    //TODO - Fix Knockback => better implementation
    [Header("KnockBack Info")]
    public Vector2 knockbackDir;
    public float knockbackDuration = .07f;
    //TODO - FIX Knockback power => This is actually used when receiving not giving
    public float knockbackPower = 1.0f;    
    public float OriginalKnockbackDuration { get; private set; }
    public bool IsKnocked { get; protected set; }

    [Header("Collision Info")]
    public Transform attackCheck;
    public float attackCheckRadius;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected string whatIsPlatform;

    #endregion

    public int FacingDir { get; private set; } = 1;
    protected bool facingRight = true;

    #region Components

    public Animator Anim { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public CharacterStats Stats { get; private set; }
    public CapsuleCollider2D CD { get; private set; }

    #endregion

    protected virtual void Awake()
    {

    }

    // Use this for initialization
    protected virtual void Start()
    {
        Anim = GetComponentInChildren<Animator>();
        RB = GetComponent<Rigidbody2D>();
        Stats = GetComponent<CharacterStats>();
        CD = GetComponent<CapsuleCollider2D>();

        OriginalKnockbackDuration = knockbackDuration;
    }

    // Update is called once per frame
    protected virtual void Update() { }

    public virtual void DamageImpact()
    {
        if (!RB.bodyType.Equals(RigidbodyType2D.Static))
            StartCoroutine("HitKnockback");
    }
    public virtual void StopKnockback()
    {
        StopCoroutine("HitKnockback");
        IsKnocked = false;
    }

    protected virtual IEnumerator HitKnockback()
    {
        IsKnocked = true;

        RB.velocity = new Vector2(knockbackDir.x * -FacingDir, knockbackDir.y) * knockbackPower;

        yield return new WaitForSeconds(knockbackDuration);

        IsKnocked = false;
    }

    #region Velocity

    public void SetZeroVelocity()
    {
        if (IsKnocked) return;

        RB.velocity = new Vector2(0, 0);
    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        if (IsKnocked) return;

        RB.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }
    #endregion

    #region Collision
    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public virtual bool IsWallDetected()
    {
        RaycastHit2D hit = Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDir, wallCheckDistance, whatIsGround);
        return hit && !hit.transform.CompareTag(whatIsPlatform);
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
    #endregion

    #region Flip
    public virtual void Flip()
    {
        FacingDir = FacingDir * -1;
        facingRight = !facingRight;

        transform.Rotate(0, 180, 0);
    }

    public virtual void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
        {
            Flip();
        }
        else if (_x < 0 && facingRight)
        {
            Flip();
        }
    }

    public virtual void SetupDefaultFacingDir(int _dir)
    {
        FacingDir = _dir;
        if (FacingDir == -1)
            facingRight = false;
    }
    #endregion

    public virtual void Die() { }

    public virtual void OnLightHit() { }

    public virtual void OnHeavyHit() { }
}