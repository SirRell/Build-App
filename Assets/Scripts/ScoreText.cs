using TMPro; //Text mesh pro namespace
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    private int score; //The current score
    private TextMeshProUGUI text; //The text script

    void Start()
    {
        //Assign the text script to the one attached to this game object
        text = GetComponent<TextMeshProUGUI>();
        //Listen for the Cube Spawned announement from the Game Manager
        GameManager.OnCubeSpawned += UpdateScore;
    }

    private void OnDestroy()
    {
        //Stop listening for an announcement, because this game object has been destroyed
        GameManager.OnCubeSpawned -= UpdateScore;
    }

    private void UpdateScore()
    {
        //Add 1 to the score
        score++;
        //Change the text to display the score
        text.text = "Score: " + score;
    }
}
