using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    private const string PlayerTag = "Player";

    [SerializeField] private float _movingSpeed;

    private Transform _playerTransform;
    private float _timeToFind = 0.2f;

    private void Start()
    {
        Invoke(nameof(FindPlayer), _timeToFind);
    }

    private void Update()
    {
        if (_playerTransform != null)
            FollowCamera();
    }

    private void FindPlayer()
    {
        if (_playerTransform == null)
            _playerTransform = GameObject.FindGameObjectWithTag(PlayerTag).transform;
    }

    private void FollowCamera()
    {
        Vector3 target = new Vector3(_playerTransform.position.x, _playerTransform.position.y + 2, _playerTransform.position.z - 10);

        transform.position = Vector3.Lerp(transform.position, target, _movingSpeed * Time.deltaTime);
    }
}
