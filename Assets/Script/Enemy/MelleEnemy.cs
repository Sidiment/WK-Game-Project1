using UnityEngine;
public class MelleEnemy : EnemyAI
{
    public float chaseSpeed = 6f;
    public float attackDamage = 5f;

    protected override void Chase()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * chaseSpeed;
        animator.SetBool("isWalking", true);
    }

    protected override void Attack()
    {
        if (Time.time - lastAttackTime > attackCooldown)
        {
            lastAttackTime = Time.time;
            //animator.SetTrigger("Attack");
            // Apply small damage to player
            Debug.Log("Fast enemy attacks for " + attackDamage + " damage.");
        }
    }
}
