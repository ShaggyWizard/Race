using System;

public class Timer
{
    public event Action OnTick;
    public event Action OnTimerEnd;


    private float _duration;
    private float _tickRate;
    private float _remainingTime;
    private float _tickTimer;


    public float RemainingTime
    {
        get { return _remainingTime; }
        private set { _remainingTime = value > 0f ? value : 0f; }
    }
    public float Progress => 1f - (_remainingTime / _duration);
    public bool Finished => _remainingTime <= 0;


    public Timer() { }
    public Timer(float time, float tickRate = 0f)
    {
        Set(time);
        _tickRate = tickRate;
        _tickTimer = tickRate;
    }


    public void Set(float time)
    {
        RemainingTime = time;
        _duration = RemainingTime;
    }
    public bool TryUpdate(float deltaTime)
    {
        if (Finished) { return false; }

        RemainingTime -= deltaTime;

        if (_tickRate > 0f)
        {
            _tickTimer -= deltaTime;
            CheckForEffect();
        }

        if (Finished)
        {
            End();
        }
        return true;
    }
    public void End()
    {
        OnTimerEnd?.Invoke();
    }


    private void CheckForEffect()
    {
        if (_tickTimer > 0f) { return; }

        OnTick?.Invoke();

        _tickTimer += _tickRate;
    }
}
