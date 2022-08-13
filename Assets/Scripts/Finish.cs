using UnityEngine;
using UnityEngine.Events;

public class Finish : MonoBehaviour
{
    [SerializeField] private UnityEvent _onFinish;


    private void OnTriggerEnter()
    {
        //Didn't bother to check for correct gameobject because there is only car in game

        _onFinish.Invoke();
    }
}
