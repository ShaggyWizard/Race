using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Respawner : MonoBehaviour
{
    public UnityEvent OnRespawn;

    private Vector3 _checkPointPosition;
    private Quaternion _checkPointRotation;


    public void SaveCheckPoint()
    {
        _checkPointPosition = transform.position;
        _checkPointRotation = transform.rotation;
    }
    public void Respawn(float delay = 0)
    {
        StartCoroutine(LoadAfterDelay(delay));
    }


    private IEnumerator LoadAfterDelay(float delay)
    {
        float duration = delay;

        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }

        transform.position = _checkPointPosition;
        transform.rotation = _checkPointRotation;
        OnRespawn?.Invoke();
    }
}
