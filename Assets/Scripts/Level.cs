using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private TMP_Text _progressText;
    [SerializeField] private TMP_Text _powerText;
    [SerializeField] private TMP_Text _finishTimeText;
    [SerializeField] private TMP_Text _newRecordText;
    [SerializeField] private Input _input;
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _finish;


    private float _timer;
    private float _distance;
    private bool _finished;


    private void Awake()
    {
        _distance = (_finish.position  - _target.position).magnitude;
        _finished = false;
    }
    private void OnEnable()
    {
        _input.OnFlickPos += CalculatePower;
    }
    private void OnDisable()
    {
        _input.OnFlickPos -= CalculatePower;
    }
    private void Update()
    {
        if (_finished) { return; }
        _timer += Time.deltaTime;
        _timeText.text = Leaderboard.FormatTime(_timer);
        float currentDistance = (_finish.position - _target.position).magnitude;
        float progress = 1f - (currentDistance / _distance);
        float precentage = progress * 100;

        _progressText.text = ((int)precentage).ToString() + '%';
    }


    public void Finish()
    {
        _finished = true;
        _progressText.text = "100%";
        _finishTimeText.text = Leaderboard.FormatTime(_timer);
        if (Leaderboard.TrySaveScore(new Score("player", _timer)))
        {
            _newRecordText.gameObject.SetActive(true);
        }
    }
    public void Pause()
    {
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        Time.timeScale = 1f;
    }
    public void SetScene(int sceneID)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
    }


    private void CalculatePower(Vector2 vector)
    {
        _powerText.text = ((int)(vector.magnitude * 100)).ToString() + '%';
    }
}
