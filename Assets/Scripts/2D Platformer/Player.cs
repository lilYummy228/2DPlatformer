using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Jumper))]
[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(DamageController))]
[RequireComponent(typeof(AnimationController))]
public class Player : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string ScoreCounterTag = "ScoreCounter";

    public int Health { get; protected set; } = 100;
    public int Damage { get; private set; } = 20;

    private readonly int VelocityX = Animator.StringToHash(nameof(VelocityX));
    private readonly int VelocityY = Animator.StringToHash(nameof(VelocityY));
    private readonly int IsGrounded = Animator.StringToHash(nameof(IsGrounded));
    private readonly int IsAttack = Animator.StringToHash(nameof(IsAttack));

    private Mover _mover;
    private Jumper _jumper;
    private Attacker _attacker;
    private DamageController _damageController;
    private AnimationController _animationController;
    private ScoreCounter _scoreCounter;
    private int _initialHealth;

    private void Start()
    {        
        _initialHealth = Health;
        _mover = GetComponent<Mover>();
        _jumper = GetComponent<Jumper>();
        _attacker = GetComponent<Attacker>();
        _damageController = GetComponent<DamageController>();
        _animationController = GetComponent<AnimationController>();

        if (GameObject.FindWithTag(ScoreCounterTag).TryGetComponent(out ScoreCounter scoreCounter))
            _scoreCounter = scoreCounter;
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
                Heal(heart.HealValue);
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

    public void Heal(int heal)
    {
        Health = _damageController.Heal(Health, heal);

        if (Health > _initialHealth)
            Health = _initialHealth;
    }

    public void TakeHit(int damage)
    {
        Health = _damageController.TakeDamage(Health, damage);
    }
}
