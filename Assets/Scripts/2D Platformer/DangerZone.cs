using UnityEngine;

public class DangerZone : MonoBehaviour
{
    [SerializeField] private SpawnPoint _spawnPoint;

    private int _damage = 20;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            _spawnPoint.SetPosition(collision.gameObject);
            player.TakeHit(_damage);
        }
    }
}
