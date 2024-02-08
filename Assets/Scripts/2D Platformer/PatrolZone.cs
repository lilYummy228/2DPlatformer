using UnityEngine;

public class PatrolZone : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
            enemy.ReachWaypoint();
    }
}
