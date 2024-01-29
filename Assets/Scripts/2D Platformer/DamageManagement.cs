using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AnimationController))]
public class DamageManagement : MonoBehaviour
{
    private readonly int IsDead = Animator.StringToHash(nameof(IsDead));
    private readonly int IsHurt = Animator.StringToHash(nameof(IsHurt));

    [SerializeField] private Material _blinkMaterial;

    private WaitForSeconds _wait;
    private AnimationController _animationController;
    private SpriteRenderer _spriteRenderer;
    private Material _defaultMaterial;
    private float _blinkTime = 0.2f;
    private float _clearDeadBodyTime = 4f;

    public int TakeDamage(int health)
    {
        health--;

        StartCoroutine(Blink());

        if (health <= 0)
            Dead(true);

        return health;
    }

    public void Dead(bool isDead)
    {
        _animationController.SetDeathState(IsDead, isDead);

        if (gameObject.TryGetComponent(out Enemy enemy))
            SetDeath(enemy);
        else if (gameObject.TryGetComponent(out Player player))
            SetDeath(player);

        Invoke(nameof(ClearDeadBody), _clearDeadBodyTime);
    }

    private void SetDeath(Behaviour behaviour)
    {
        behaviour.enabled = false;
        Destroy(behaviour.GetComponent<Rigidbody2D>());
        behaviour.GetComponent<Collider2D>().enabled = false;
    }

    private void ClearDeadBody()
    {
        Destroy(gameObject);
    }

    private IEnumerator Blink()
    {
        _spriteRenderer.material = _blinkMaterial;
        _animationController.SetHurtState(IsHurt, true);

        yield return _wait;

        _spriteRenderer.material = _defaultMaterial;
        _animationController.SetHurtState(IsHurt, false);
    }

    private void Start()
    {
        _wait = new WaitForSeconds(_blinkTime);
        _animationController = GetComponent<AnimationController>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultMaterial = _spriteRenderer.material;
    }
}
