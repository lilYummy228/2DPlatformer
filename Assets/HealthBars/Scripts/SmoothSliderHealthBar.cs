using UnityEngine;

public class SmoothSliderHealthBar : SliderHealthBar
{
    [SerializeField] private float _reduceSpeed = 35f;

    public override void Start()
    {
        base.Start();
    }

    public override void FindPlayer()
    {
        base.FindPlayer();

        HealthSlider.value = Player.Health;
    }

    public override void Update()
    {
        if (Player != null)
            HealthSlider.value = Mathf.MoveTowards(HealthSlider.value, Player.Health, _reduceSpeed * Time.deltaTime);
    }
}
