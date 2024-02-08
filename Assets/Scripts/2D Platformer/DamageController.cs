using System.Collections;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    private readonly int IsDead = Animator.StringToHash(nameof(IsDead));
    private readonly int IsHurt = Animator.StringToHash(nameof(IsHurt));

    [SerializeField] private Material _hurtBlinkMaterial;
    [SerializeField] private Material _healBlinkMaterial;

    private WaitForSeconds _wait;
    private Material _defaultMaterial;
    private SpriteRenderer _spriteRenderer;
    private AnimationController _animationController;
    private float _blinkTime = 0.2f;
    private float _clearDeadBodyTime = 4f;

    private void Start()
    {
        if (gameObject.TryGetComponent(out SpriteRenderer spriteRenderer))
            _spriteRenderer = spriteRenderer;

        if (gameObject.TryGetComponent(out AnimationController animationController))
            _animationController = animationController;

        _wait = new WaitForSeconds(_blinkTime);

        _defaultMaterial = _spriteRenderer.material;
    }

    public int TakeDamage(int health, int damage)
    {
        health -= damage;

        StartCoroutine(HurtBlink());

        if (health <= 0)
            Dead();

        return health;
    }

    private IEnumerator HurtBlink()
    {
        _spriteRenderer.material = _hurtBlinkMaterial;

        _animationController.SetHurtState(IsHurt, true);

        yield return _wait;

        _spriteRenderer.material = _defaultMaterial;

        _animationController.SetHurtState(IsHurt, false);
    }

    private IEnumerator HealBlink()
    {
        _spriteRenderer.material = _healBlinkMaterial;

        yield return _wait;

        _spriteRenderer.material = _defaultMaterial;
    }

    public int Heal(int health, int healValue)
    {
        health += healValue;

        StartCoroutine(HealBlink());

        return health;
    }

    private void Dead()
    {
        _animationController.SetDeathState(IsDead, true);

        if (gameObject.TryGetComponent(out Enemy enemy))
            enemy.enabled = false;
        else if (gameObject.TryGetComponent(out Player player))
            player.enabled = false;

        gameObject.GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject.GetComponent<Rigidbody2D>());

        Invoke(nameof(ClearDeadBody), _clearDeadBodyTime);
    }

    private void ClearDeadBody()
    {
        Destroy(gameObject);
    }
}
