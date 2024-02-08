using UnityEngine;

public class MusicPlayerController : MonoBehaviour
{
    private string _musicTag = "Music";

    private void Start()
    {
        GameObject music = GameObject.FindWithTag(_musicTag);

        if (music != null)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.tag = _musicTag;
            DontDestroyOnLoad(gameObject);
        }
    }
}
