using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] private LayerMask _ground;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private float _jumpForce;

    public Rigidbody2D Rigidbody { get; private set; }

    private float _checkRadius = 0.5f;

    private void Start()
    {
        if (gameObject.TryGetComponent(out Rigidbody2D rigidbody))
            Rigidbody = rigidbody;
    }

    public bool Jump(bool isJump)
    {
        if (IsGrounded())
            if (isJump)
                Rigidbody.velocity = Vector2.up * _jumpForce;

        return IsGrounded();
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundChecker.position, _checkRadius, _ground);
    }
}
