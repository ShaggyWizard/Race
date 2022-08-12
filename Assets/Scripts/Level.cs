using TMPro;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private TMP_Text _progressText;
    [SerializeField] private TMP_Text _powerText;
    [SerializeField] private Input _input;
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _finish;


    private float _timer;
    private float _distance;


    private void Awake()
    {
        _distance = (_finish.position  - _target.position).magnitude;
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
        _timer += Time.deltaTime;
        _timeText.text = FormatTime(_timer);
        float currentDistance = (_finish.position - _target.position).magnitude;
        float progress = 1f - (currentDistance / _distance);
        float precentage = progress * 100;

        _progressText.text = ((int)precentage).ToString() + '%';
    }


    private void CalculatePower(Vector2 vector)
    {
        _powerText.text = ((int)(vector.magnitude * 100)).ToString() + '%';
    }
    private string FormatTime(float time)
    {
        int seconds = Mathf.FloorToInt(time);
        int milliseconds = (int)((time - seconds) * 10000);
        return $"{seconds}.{ milliseconds}";
    }
}
