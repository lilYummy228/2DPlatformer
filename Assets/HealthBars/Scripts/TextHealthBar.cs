using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextHealthBar : HealthBar
{
    private TextMeshProUGUI _healthValue;

    public override void Start()
    {
        base.Start();

        _healthValue = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (Player != null)
            _healthValue.text = $"{Player.Health}/{MaxHealth}";
    }
}
