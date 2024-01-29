using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Jumper))]
[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(DamageController))]
[RequireComponent(typeof(AnimationController))]
[RequireComponent(typeof(ScoreCounter))]
public class Player : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);

    public int Health { get; private set; } = 5;

    private readonly int VelocityX = Animator.StringToHash(nameof(VelocityX));
    private readonly int VelocityY = Animator.StringToHash(nameof(VelocityY));
    private readonly int IsGrounded = Animator.StringToHash(nameof(IsGrounded));
    private readonly int IsAttack = Animator.StringToHash(nameof(IsAttack));

    private Mover _mover;
    private Jumper _jumper;
    private Attacker _attacker;
    private DamageController _damageManagement;
    private AnimationController _animationController;
    private ScoreCounter _scoreCounter;
    private int _initialHealth;

    private void Start()
    {
        _initialHealth = Health;
        _mover = GetComponent<Mover>();
        _jumper = GetComponent<Jumper>();
        _attacker = GetComponent<Attacker>();
        _damageManagement = GetComponent<DamageController>();
        _animationController = GetComponent<AnimationController>();
        _scoreCounter = GetComponent<ScoreCounter>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Coin coin))
        {
            _scoreCounter.AddScore();
            Destroy(coin.gameObject);
        }

        if (collider.TryGetComponent(out Heart heart))
        {
            if (Health < _initialHealth)
            {
                Health++;
                Destroy(heart.gameObject);
            }
        }
    }

    private void Update()
    {
        _animationController.SetVelocityX(VelocityX, _mover.Move(Input.GetAxis(Horizontal)));
        _animationController.SetVelocityY(VelocityY, _jumper.Rigidbody.velocity.y);
        _animationController.SetGroundedState(IsGrounded, _jumper.Jump(Input.GetKeyDown(KeyCode.Space)));
        _animationController.SetAttackState(IsAttack, _attacker.IsAttack(Input.GetKeyDown(KeyCode.E)));
    }

    public void TakeHit()
    {
        Health = _damageManagement.TakeDamage(Health);
    }
}
