using UnityEngine;

public class QuadraticBezier : MonoBehaviour
{
    public GameObject p0;   // The start point GameObject
    public GameObject p1;   // The mid point GameObject
    public GameObject p2;   // The end point GameObject 

    // Add this to the top of the QuadraticBezier class
    public LineRenderer lineRenderer;

    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1,
    Vector3 p2)
    {
        float u = 1 - t;   // u = (1 - t)
        float tt = t * t;  // t squared
        float uu = u * u;  // (1 - t) squared

        Vector3 point = uu * p0;  // (1 - t)^2 * P0
        point += 2 * u * t * p1;  // 2(1 - t)t * P1
        point += tt * p2;         // t^2 * P2

        return point;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Method to calculate and draw the quadratic Bezier curve
    private void DrawBezierCurve()
    {
        // Number of points on the curve for smoothness
        int curveResolution = 50;
        lineRenderer.positionCount = curveResolution;

        // Loop through each point on the curve
        for (int i = 0; i < curveResolution; i++)
        {
            // Parameter t varies from 0 to 1
            float t = i / (float)(curveResolution - 1);
            Vector3 curvePoint = CalculateBezierPoint(t,
            p0.transform.position,
            p1.transform.position,
            p2.transform.position);

            lineRenderer.SetPosition(i, curvePoint);
        }
    }

    // Update is called once per frame
    void Update()
    {
        DrawBezierCurve();
    }
}
