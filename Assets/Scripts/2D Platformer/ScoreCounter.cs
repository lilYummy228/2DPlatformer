using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ScoreCounter : MonoBehaviour
{
    private TMP_Text _scoreCounter;

    private int _scoreCount = 0;

    private void Start()
    {
        _scoreCounter = GetComponent<TMP_Text>();
    }

    public void AddScore()
    {
        _scoreCount++;
        _scoreCounter.text = _scoreCount.ToString();
    }
}
