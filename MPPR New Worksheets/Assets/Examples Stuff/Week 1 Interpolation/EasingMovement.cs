using System;
using UnityEngine;

public class EasingMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform pointA;
    public Transform pointB;
    public float duration = 2.0f;

    private float elapsedTime = 0.0f;
    private Vector3 positionA;
    private Vector3 positionB;

    public enum EasingType { Linear, EaseIn, EaseOut, EaseInOut }
    public EasingType easingType = EasingType.Linear;


    void Start()
    {
        positionA = pointA.position;
        positionB = pointB.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            t = Mathf.Clamp01(t);

            // Apply the selected Easing function
            switch (easingType)
            {
                case EasingType.Linear:
                    break; // Linear easing means t stays unchanged
                case EasingType.EaseIn:
                    t = EaseInCubic(t);
                    break;
                case EasingType.EaseOut:
                    t = EaseOutCubic(t);
                    break;
                case EasingType.EaseInOut:
                    t = EaseInOutCubic(t);
                    break;
            }

            // Non-linear interpolation
            Vector3 interpolatedPosition = (1 - t) * positionA + t * positionB;

            // Update the MovingObject's Position
            transform.position = interpolatedPosition;
        }
        else
        {
            //Ensure the object reaches the exact position of pointB
            transform.position = positionB;
        }
    }

    private float EaseInOutCubic(float t)
    {
        return t < 0.5
           ? 4f * t * t * t
           : 1f - (float)Math.Pow(-2f * t + 2f, 3f) / 2f;
    }

    private float EaseOutCubic(float t)
    {
        return (float)(1 - Math.Pow(1 - t, 3));
    }

    private float EaseInCubic(float t)
    {
        return t * t * t;
    }

    void OnDrawGizmos()
    {
        if (pointA != null && pointB != null)
        {

            // Draw Point A and Point B
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(pointA.position, 0.2f);

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(pointB.position, 0.2f);

            // Draw the line between Point A and Point B
            Gizmos.color = Color.gray;
            Gizmos.DrawLine(pointA.position, pointB.position);
            // Same code as before

            // Draw interpolation steps
            Gizmos.color += Color.green;
            int steps = 20;
            for (int i = 0; i < steps; i++)
            {
                float t = i / (float)steps;

                // Apply Ease in Function
                t = t * t;

                Vector3 interpolatedPosition = (1 - t) * positionA + t * positionB;
                Gizmos.DrawSphere(interpolatedPosition, 0.1f);

                // Apply the easing function in OnDrawGizmos
                // based on the selected easingType.

                switch (easingType)
                {
                    case EasingType.Linear:
                        break;
                    case EasingType.EaseIn:
                        t = EaseInCubic(t);
                        break;
                    case EasingType.EaseOut:
                        t = EaseOutCubic(t);
                        break;
                    case EasingType.EaseInOut:
                        t = EaseInOutCubic(t);
                        break;
                }
            }
        }
    }
}
