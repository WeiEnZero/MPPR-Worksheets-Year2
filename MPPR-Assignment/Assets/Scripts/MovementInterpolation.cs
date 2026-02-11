using System.Drawing;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Color = UnityEngine.Color;

public class MovementInterpolation : MonoBehaviour
{
    // Takes in the position component of the control points
    public Transform pointA;
    public Transform pointB;
    public Transform pointC;
    // Total time for ball to travel in the bezier path
    public float duration = 2.0f;

    private float elapsedTime = 0.0f;
    private Vector3 positionA;
    private Vector3 positionB;
    private Vector3 positionC;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        positionA = pointA.position;
        positionB = pointB.position;
        positionC = pointC.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (elapsedTime < duration)
        {
            GeneralEasing();
        }
        else
        {
            transform.position = positionC;
        }
    }

    public void GeneralEasing()
    {
        elapsedTime += Time.deltaTime;
        float t = elapsedTime / duration;
        t = Mathf.Clamp01(t);

        // Interpolation formula
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 interpolatedBasketball = uu * positionA;
        interpolatedBasketball += 2 * u * t * positionB;
        interpolatedBasketball += tt * positionC;

        transform.position = interpolatedBasketball;
    }
}


