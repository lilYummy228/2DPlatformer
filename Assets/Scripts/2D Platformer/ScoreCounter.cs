using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreCounter;

    private int _scoreCount;

    public void AddScore()
    {
        _scoreCount++;
    }
       
    private void Update()
    {
        _scoreCounter.text = _scoreCount.ToString();
    }

}
