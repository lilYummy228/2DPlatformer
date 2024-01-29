using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    private const string _playerTag = "Player";

    [SerializeField] BoxCollider2D _detectionZone;

    public Transform Player { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(_playerTag) && gameObject.TryGetComponent(out Enemy enemy))
        {
            Player = collision.gameObject.transform;
            StartCoroutine(enemy.DetectPlayer());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(_playerTag))
            Player = null;
    }
}
