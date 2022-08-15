using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderboardView : MonoBehaviour
{
    [SerializeField] private TMP_Text _timeText;

    private void Awake()
    {
        if (Leaderboard.TryLoadScore(out Score score))
        {
            _timeText.text = $"{Leaderboard.FormatTime(score.time)}";
        }
        else
        {
            _timeText.gameObject.SetActive(false);
        }
    }

    public void SetScene(int sceneID)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
    }
}
