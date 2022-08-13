using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderboardView : MonoBehaviour
{
    [SerializeField] private TMP_Text _timeText;

    private void Awake()
    {
        var score = Leaderboard.LoadScore();

        _timeText.text = $"{score.name} - {Leaderboard.FormatTime(score.time)}";
    }

    public void SetScene(int sceneID)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
    }
}
