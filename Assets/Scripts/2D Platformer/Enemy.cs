using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private readonly int VelocityX = Animator.StringToHash(nameof(VelocityX));
    private readonly int IsAttack = Animator.StringToHash(nameof(IsAttack));

    [SerializeField] private int _health = 3;

    private PlayerDetector _playerDetector;
    private WaitForSeconds _wait;
    private WaitForSecondsRealtime _stun;
    private Mover _mover;
    private AnimationController _animationController;
    private DamageManagement _damageManagement;
    private float _moveDirection = 1f;
    private float _waitingTime = 2f;
    private float _stunningTime = 0.5f;

    public void ReachWaypoint()
    {
        StartCoroutine(nameof(ChangeMoveDirection));
    }

    public void TakeHit()
    {
        _health = _damageManagement.TakeDamage(_health);

        StartCoroutine(nameof(Stun));
    }

    public void Hit(bool isAttack)
    {
        _animationController.SetAttackState(IsAttack, isAttack);
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

    private void Start()
    {
        _wait = new WaitForSeconds(_waitingTime);
        _stun = new WaitForSecondsRealtime(_stunningTime);
        _mover = GetComponent<Mover>();
        _animationController = GetComponent<AnimationController>();
        _damageManagement = GetComponent<DamageManagement>();
        _playerDetector = GetComponent<PlayerDetector>();
    }

    private void Update()
    {
        _animationController.SetVelocityX(VelocityX, _mover.Move(_moveDirection));
    }
}
