using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Transform _object;
    [SerializeField, Range(0, 100)] private int _chanceToSpawn = 100;

    private Vector2 _position;
    private Quaternion _rotation;
    private int _randomValue;

    private void Start()
    {
        _position = transform.position;
        _randomValue = Random.Range(0, 100);

        TrySpawn();
    }

    public void SetPosition(GameObject creature)
    {
        creature.transform.position = _position;
    }

    public void TrySpawn()
    {
        if (_object != null && _randomValue < _chanceToSpawn && _object.TryGetComponent(out Player player) == false)
            Instantiate(_object, _position, _rotation);
    }
}
