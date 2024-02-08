using UnityEngine;
using UnityEngine.Audio;

public class VolumeInitiate : MonoBehaviour
{
    [SerializeField] private string _volumeParameter = "MasterVolume";
    [SerializeField] private AudioMixer _mixer;

    void Start()
    {
        var volumeValue = PlayerPrefs.GetFloat(_volumeParameter, _volumeParameter == "MusicVolume" ? 0f : -80f);
        _mixer.SetFloat(_volumeParameter, volumeValue);
    }
}
