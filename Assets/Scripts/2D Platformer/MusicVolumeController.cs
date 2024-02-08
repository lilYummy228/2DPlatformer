using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicVolumeController : MonoBehaviour
{
    private const float Multiplier = 20f;

    [SerializeField] private string _volumeParameter = "MasterVolume";
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _volumePercent;

    private float _volumeValue;

    private void Start()
    {
        _volumeValue = PlayerPrefs.GetFloat(_volumeParameter, Mathf.Log10(_slider.value) * Multiplier);
        _slider.value = Mathf.Pow(10f, _volumeValue / Multiplier);
    }

    public void HandleSliderValueChanger(float value)
    {
        _volumePercent.text = Mathf.Round(value * 100) + "%";
        _volumeValue = Mathf.Log10(value) * Multiplier;
        _mixer.SetFloat(_volumeParameter, _volumeValue);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(_volumeParameter, _volumeValue);
    }
}
