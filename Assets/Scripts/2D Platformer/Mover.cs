using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public float Move(float moveDirection)
    {
        _rigidbody.velocity = new Vector2(moveDirection * _moveSpeed, _rigidbody.velocity.y);

        if (moveDirection != 0)
        {
            FlipToSight(moveDirection);

            if (moveDirection < 0)
                moveDirection = -moveDirection;
        }

        return moveDirection;
    }

    private void FlipToSight(float moveDirection)
    {
        if (moveDirection > 0)
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        else if (moveDirection < 0)
            transform.localRotation = Quaternion.Euler(0, 180, 0);
    }
}
