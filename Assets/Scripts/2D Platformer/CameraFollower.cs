using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    private const string PlayerTag = "Player";

    [SerializeField] private float _movingSpeed;

    private Transform _player;
    private float _timeToFind = 0.2f;

    private void Start()
    {
        Invoke(nameof(FindPlayer), _timeToFind);
    }

    private void Update()
    {
        if (_player != null)
            FollowCamera();
    }

    private void FindPlayer()
    {
        if (_player == null)
            _player = GameObject.FindGameObjectWithTag(PlayerTag).transform;
    }

    private void FollowCamera()
    {
        Vector3 target = new Vector3(_player.position.x, _player.position.y + 2, _player.position.z - 10);

        transform.position = Vector3.Lerp(transform.position, target, _movingSpeed * Time.deltaTime);
    }
}
