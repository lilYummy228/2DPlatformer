using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderHealthBar : HealthBar
{
    public Slider HealthSlider { get; private set; }

    public override void Start()
    {
        base.Start();

        HealthSlider = GetComponent<Slider>();
    }

    public virtual void Update()
    {
        if (Player != null)
            HealthSlider.value = Player.Health;
    }
}
