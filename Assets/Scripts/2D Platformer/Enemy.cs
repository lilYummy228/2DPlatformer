using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(AnimationController))]
[RequireComponent(typeof(DamageController))]
[RequireComponent(typeof(EnemyDetector))]
public class Enemy : MonoBehaviour
{
    private readonly int VelocityX = Animator.StringToHash(nameof(VelocityX));
    private readonly int IsAttack = Animator.StringToHash(nameof(IsAttack));

    [SerializeField] private int _health = 3;

    private EnemyDetector _playerDetector;
    private WaitForSeconds _wait;
    private WaitForSecondsRealtime _stun;
    private Mover _mover;
    private AnimationController _animationController;
    private DamageController _damageManagement;
    private Coroutine _hitCoroutine;
    private float _moveDirection = 1f;
    private float _waitingTime = 2f;
    private float _stunningTime = 0.4f;

    private void Start()
    {
        _wait = new WaitForSeconds(_waitingTime);
        _stun = new WaitForSecondsRealtime(_stunningTime);
        _mover = GetComponent<Mover>();
        _animationController = GetComponent<AnimationController>();
        _damageManagement = GetComponent<DamageController>();
        _playerDetector = GetComponent<EnemyDetector>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Player player))
            _hitCoroutine = StartCoroutine(Hit(player));
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Player player) && _hitCoroutine != null)
        {
            StopCoroutine(_hitCoroutine);
            _animationController.SetAttackState(IsAttack, false);
        }
    }

    private void Update()
    {
        _animationController.SetVelocityX(VelocityX, _mover.Move(_moveDirection));
    }

    public void TakeHit()
    {
        _health = _damageManagement.TakeDamage(_health);

        StartCoroutine(nameof(Stun));
    }

    public void ReachWaypoint()
    {
        StartCoroutine(nameof(ChangeMoveDirection));
    }

    public IEnumerator DetectPlayer()
    {
        if (_playerDetector.Player != null)
        {
            _moveDirection = -(transform.position.x - _playerDetector.Player.transform.position.x);

            if (_moveDirection > 0)
                _moveDirection = 1;
            else
                _moveDirection = -1;
        }

        yield return null;
    }

    private IEnumerator Hit(Player player)
    {
        while (player.Health > 0)
        {
            _animationController.SetAttackState(IsAttack, true);

            player.TakeHit();

            yield return _stun;

            _animationController.SetAttackState(IsAttack, false);

            yield return _stun;
        }

        if (_hitCoroutine != null)
            StopCoroutine(_hitCoroutine);
    }

    private IEnumerator Stun()
    {
        if (DetectPlayer() != null)
            StopCoroutine(DetectPlayer());

        float moveDirection = _moveDirection;
        _moveDirection = 0;

        yield return _stun;

        _moveDirection = moveDirection;
    }

    private IEnumerator ChangeMoveDirection()
    {
        float moveDirection = _moveDirection;
        _moveDirection = 0f;

        yield return _wait;

        _moveDirection = -moveDirection;
    }    
}
