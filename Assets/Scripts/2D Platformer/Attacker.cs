using UnityEngine;

public class Attacker : MonoBehaviour
{   
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange = 0.5f;
    [SerializeField] private LayerMask _enemies;

    private float _attackRate = .5f;
    private float _nextAttackTime = 0f;

    public bool IsAttack(bool isAttack)
    {
        if (Time.time >= _nextAttackTime)
        {
            if (isAttack)
            {
                MeleeAttack();
                _nextAttackTime = Time.time + _attackRate;
            }
        }

        return isAttack;
    }

    private void MeleeAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemies);

        foreach (Collider2D collider in hitEnemies)
            if (collider.TryGetComponent(out Enemy enemy))
                enemy.TakeHit();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Enemy enemy) && gameObject.TryGetComponent(out Player player))
        {
            player.TakeHit();
            enemy.Hit(true);

            if (player.enabled == false)
            {
                enemy.Hit(false);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Enemy enemy))
        {
            enemy.Hit(false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null)
            return;

        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
}
