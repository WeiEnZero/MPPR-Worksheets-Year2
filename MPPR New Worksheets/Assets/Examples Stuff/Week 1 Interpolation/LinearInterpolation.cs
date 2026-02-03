using UnityEngine;

public class LinearInterpolation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform pointA;
    public Transform pointB;
    public float duration = 2.0f;

    private float elapsedTime = 0.0f;
    private Vector3 positionA;
    private Vector3 positionB;

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


            // Explict linear interpolation
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

            // Draw interpolation steps
            Gizmos.color += Color.green;
            int steps = 20;
            for (int i = 0; i < steps; i++)
            {
                float t = i / (float)steps;
                Vector3 interpolatedPosition = (1-t) * positionA + t * positionB;
                Gizmos.DrawSphere(interpolatedPosition, 0.1f);
            }
        }
    }
}
