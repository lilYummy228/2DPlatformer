using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange = 0.5f;
    [SerializeField] private LayerMask _enemyLayer;

    private float _attackRate = .5f;
    private float _nextAttackTime = 0f;
    private bool _isAttack;  

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null)
            return;

        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }

    public bool IsAttack(bool isAttack)
    {
        _isAttack = isAttack;

        RechargeAttack();

        return _isAttack;
    }

    private void RechargeAttack()
    {
        if (Time.time >= _nextAttackTime)
        {
            if (_isAttack)
            {
                MeleeAttack();
                _nextAttackTime = Time.time + _attackRate;
            }
        }
    }

    private void MeleeAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayer);

        foreach (Collider2D collider in hitEnemies)
            if (collider.TryGetComponent(out Enemy enemy))
                enemy.TakeHit();
    }
}
