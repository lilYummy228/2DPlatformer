using UnityEngine;

public class Movement : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);

    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _movementSpeed;

    private void Update()
    {
        Rotate();
        Move();
    }

    private void Rotate()
    {
        float rotation = Input.GetAxis(Horizontal);

        transform.Rotate(rotation * Vector3.up * _rotationSpeed * Time.deltaTime);
    }

    private void Move()
    {
        float direction = Input.GetAxis(Vertical);
        float distance = _movementSpeed * Time.deltaTime * direction;

        transform.Translate(distance * Vector3.forward);
    }
}
