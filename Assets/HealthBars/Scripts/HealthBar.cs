using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private const string PlayerTag = "Player";

    public Player Player { get; private set; }
    public int MaxHealth { get; private set; }

    private float _timeToFind = 0.01f;

    public virtual void Start()
    {
        Invoke(nameof(FindPlayer), _timeToFind);
    }

    public virtual void FindPlayer()
    {
        if (Player == null)
            Player = GameObject.FindGameObjectWithTag(PlayerTag).GetComponent<Player>();

        MaxHealth = Player.Health;
    }
}
