using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    private int score;
    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
        GameManager.OnCubeSpawned += UpdateScore;
    }

    private void OnDestroy()
    {
        GameManager.OnCubeSpawned -= UpdateScore;
    }

    private void UpdateScore()
    {
        score++;
        text.text = "Score: " + score;
    }
}
