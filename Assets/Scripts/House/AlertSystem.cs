using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class AlertSystem : MonoBehaviour
{
    private const int MinVolume = 0;
    private const int MaxVolume = 1;

    [SerializeField] private AudioSource _alert;
    [SerializeField] private EntranceDetector _entranceDetector;
    [SerializeField] private float _speed = 0.5f;

    private IEnumerator _fadeSound;

    private void OnEnable()
    {
        _alert.volume = MinVolume;
        _entranceDetector.CreatureDetected += ReplayCoroutine;
    }

    private void OnDisable()
    {
        _entranceDetector.CreatureDetected -= ReplayCoroutine;
    }

    private IEnumerator FadeSound()
    {
        if (_entranceDetector.IsEntered)
        {
            _alert.Play();

            yield return FadeSound(MaxVolume);            
        }
        else
        {
            yield return FadeSound(MinVolume);

            _alert.Stop();
        }
    }

    private IEnumerator FadeSound(int finalVolume)
    {
        while (_alert.volume != finalVolume)
        {
            _alert.volume = Mathf.MoveTowards(_alert.volume, finalVolume, _speed * Time.deltaTime);
            yield return null;
        }
    }

    private void ReplayCoroutine()
    {
        if (_fadeSound != null)
            StopCoroutine(_fadeSound);

        _fadeSound = FadeSound();

        StartCoroutine(_fadeSound);
    }
}