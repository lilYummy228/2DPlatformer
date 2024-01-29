using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetGroundedState(int parameter, bool isGrounded)
    {
        _animator.SetBool(parameter, isGrounded);
    }

    public void SetAttackState(int parameter, bool isAttack)
    {
        _animator.SetBool(parameter, isAttack);
    }

    public void SetHurtState(int parameter, bool isHurt)
    {
        _animator.SetBool(parameter, isHurt);
    }

    public void SetDeathState(int parameter, bool isDead)
    {
        _animator.SetBool(parameter, isDead);
    }

    public void SetVelocityX(int parameter, float speed)
    {
        _animator.SetFloat(parameter, speed);
    }

    public void SetVelocityY(int parameter, float velocityY)
    {
        _animator.SetFloat(parameter, velocityY);
    }
}
