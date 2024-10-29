using UnityEngine;

public abstract class EnemyAI : MonoBehaviour
{
    public enum MonsterState
    {
        Idle,
        Chase,
        Attack
    }
    public float chaseDistance = 5f;
    public float attackDistance = 1.5f;
    public float attackCooldown = 1f;

    public Transform player;
    protected float lastAttackTime = 0;
    protected MonsterState currentState;

    protected Animator animator;
    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;

    protected virtual void Start()
    {
        // Common setup for all enemies
        player = GameManager.Instance.player.transform;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        currentState = MonsterState.Idle;
    }

    protected virtual void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        spriteRenderer.flipX =  (player.position.x - transform.position.x) > 0 ? false:true;

        if (distanceToPlayer <= attackDistance)
        {
            SwitchState(MonsterState.Attack);
        }
        else if (distanceToPlayer <= chaseDistance)
        {
            SwitchState(MonsterState.Chase);
        }
        else
        {
            SwitchState(MonsterState.Idle);
        }

        // Call the behavior based on current state
        switch (currentState)
        {
            case MonsterState.Idle:
                Idle();
                break;
            case MonsterState.Chase:
                Chase();
                break;
            case MonsterState.Attack:
                Attack();
                break;
        }
    }

    // Define common Idle behavior for all enemies
    protected virtual void Idle()
    {
        animator.SetBool("isWalking", false);
    }

    // Abstract methods for different enemy behavior in subclasses
    protected abstract void Chase();
    protected abstract void Attack();

    // Method to switch states
    protected void SwitchState(MonsterState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
            Debug.Log($"Switched to state: {newState}");
        }
    }
}
