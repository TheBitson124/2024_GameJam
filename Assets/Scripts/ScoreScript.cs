using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour {
    //score
    private TextMeshProUGUI _textMeshProUGUI;
    void OnEnable() {
        Player_Stats.OnScoreChanged += UpdateScore;
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    private void OnDisable() {
        Player_Stats.OnScoreChanged -= UpdateScore;
    }

    // Update is called once per frame
    private void UpdateScore(int newScore) {
        _textMeshProUGUI.text = "SCORE: " + newScore;
    }
}
