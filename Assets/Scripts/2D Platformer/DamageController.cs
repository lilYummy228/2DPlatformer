using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AnimationController))]
[RequireComponent(typeof(SpriteRenderer))]
public class DamageController : MonoBehaviour
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
    private float _setDeathTime = 0.5f;

    private void Start()
    {
        _animationController = GetComponent<AnimationController>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _wait = new WaitForSeconds(_blinkTime);
        _defaultMaterial = _spriteRenderer.material;
    }

    public int TakeDamage(int health)
    {
        health--;

        StartCoroutine(Blink());

        if (health <= 0)
            Dead();

        return health;
    }

    private IEnumerator Blink()
    {
        _spriteRenderer.material = _blinkMaterial;
        _animationController.SetHurtState(IsHurt, true);

        yield return _wait;

        _spriteRenderer.material = _defaultMaterial;
        _animationController.SetHurtState(IsHurt, false);
    }

    private void Dead()
    {
        _animationController.SetDeathState(IsDead, true);

        if (gameObject.TryGetComponent(out Enemy enemy))
            enemy.enabled = false;
        else if (gameObject.TryGetComponent(out Player player))
            player.enabled = false;       

        Invoke(nameof(RemovePhisicsComponents), _setDeathTime);

        Invoke(nameof(ClearDeadBody), _clearDeadBodyTime);
    }

    private void RemovePhisicsComponents()
    {
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject.GetComponent<Rigidbody2D>());
    }

    private void ClearDeadBody()
    {
        Destroy(gameObject);
    }    
}
