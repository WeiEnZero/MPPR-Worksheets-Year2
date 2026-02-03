using UnityEngine;
using DG.Tweening;

public class DoTweenPosition : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float duration = 2f;

    void Start()
    {
        // Tween the object’s position from start to end point 
        // over the duration.
        transform.DOMove(endPoint.position, duration);

        // Tween the object’s position with an ease-out effect
        //transform.DOMove(endPoint.position, duration).SetEase(Ease.InOutCubic);

        // Tween the object’s position and loop with a ping-pong effect
        transform.DOMove(endPoint.position,duration).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);


    }
}
