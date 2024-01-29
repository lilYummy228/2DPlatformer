using UnityEngine;

public class Player : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);

    [SerializeField] private int _health = 5;

    private readonly int VelocityX = Animator.StringToHash(nameof(VelocityX));
    private readonly int VelocityY = Animator.StringToHash(nameof(VelocityY));
    private readonly int IsGrounded = Animator.StringToHash(nameof(IsGrounded));
    private readonly int IsAttack = Animator.StringToHash(nameof(IsAttack));

    private Mover _mover;
    private Jumper _jumper;
    private Attacker _attacker;
    private DamageManagement _damageManagement;
    private AnimationController _animationController;
    private ScoreCounter _scoreCounter;
    private int _initialHealth;

    public void TakeHit()
    {
        _health = _damageManagement.TakeDamage(_health);
    }

    private void Start()
    {
        _initialHealth = _health;
        _mover = GetComponent<Mover>();
        _jumper = GetComponent<Jumper>();
        _attacker = GetComponent<Attacker>();
        _damageManagement = GetComponent<DamageManagement>();
        _animationController = GetComponent<AnimationController>();
        _scoreCounter = GetComponent<ScoreCounter>();
    }

    private void Update()
    {
        _animationController.SetVelocityX(VelocityX, _mover.Move(Input.GetAxis(Horizontal)));
        _animationController.SetVelocityY(VelocityY, _jumper.Rigidbody.velocity.y);
        _animationController.SetGroundedState(IsGrounded, _jumper.Jump(Input.GetKeyDown(KeyCode.Space)));
        _animationController.SetAttackState(IsAttack, _attacker.IsAttack(Input.GetKeyDown(KeyCode.E)));
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
            if (_health < _initialHealth)
            {
                _health++;
                Destroy(heart.gameObject);
            }
        }
    }
}
