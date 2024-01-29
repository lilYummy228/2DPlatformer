using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Transform _object;

    private Vector2 _position;
    private Quaternion _rotation;

    public void Teleport(GameObject creature)
    {
        creature.transform.position = _position;
    }

    public void Spawn()
    {
        if (_object != null)
            Instantiate(_object, _position, _rotation);
    }

    public void SpawnRandomly()
    {
        if (Random.Range(0, 2) == 1)
            Spawn();
    }

    private void Start()
    {
        _position = transform.position;

        if (_object.TryGetComponent(out Heart heart))
            SpawnRandomly();
        else
            Spawn();
    }
}
