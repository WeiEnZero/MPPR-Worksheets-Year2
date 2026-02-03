using UnityEngine;

public class RotationInterpolation : MonoBehaviour
{
    public Vector3 startRotationEuler = new Vector3(0, 0, 0);
    public Vector3 endRotationEuler = new Vector3(0, 180, 0);
    public float duration = 2f;

    public Color startColor = Color.red;
    private Color midColor = new Color(0.5f, 0f, 0.5f);
    public Color endColor = Color.blue;

    private float elapsedTime = 0f;
    private Vector3 currentRotationEuler;
    private Renderer objectRenderer;

    void Start()
    {
        // Set the initial rotation
        transform.rotation = Quaternion.Euler(startRotationEuler);
        currentRotationEuler = startRotationEuler;
        objectRenderer = GetComponent<Renderer>();
        objectRenderer.material.color = startColor;
    }

    void Update()
    {
        if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            t = Mathf.Clamp01(t);

            // Ease In Equation
            t = t * t;

            // Ease Out Equation
            //t = t = 1 - (1 - t) * (1 - t);


            //startColor = Color.red;

            // Perform linear interpolation for each axis
            currentRotationEuler.x = (1 - t) * startRotationEuler.x +
            t * endRotationEuler.x;
            currentRotationEuler.y = (1 - t) * startRotationEuler.y +
            t * endRotationEuler.y;
            currentRotationEuler.z = (1 - t) * startRotationEuler.z +
            t * endRotationEuler.z;

            // Apply the interpolated rotation
            transform.rotation = Quaternion.Euler(currentRotationEuler);

            Color currentColor;

            if (t < 0.5f)
            {
                // First half: Red → Purple
                float subT = t / 0.5f; // scales t from [0,0.5] to [0,1]
                float r = (1 - subT) * startColor.r + subT * midColor.r;
                float g = (1 - subT) * startColor.g + subT * midColor.g;
                float b = (1 - subT) * startColor.b + subT * midColor.b;
                currentColor = new Color(r, g, b);
            }
            else
            {
                // Second half: Purple → Blue
                float subT = (t - 0.5f) / 0.5f; // scales t from [0.5,1] to [0,1]
                float r = (1 - subT) * midColor.r + subT * endColor.r;
                float g = (1 - subT) * midColor.g + subT * endColor.g;
                float b = (1 - subT) * midColor.b + subT * endColor.b;
                currentColor = new Color(r, g, b);
            }

            objectRenderer.material.color = currentColor;
        }
        else
        {
            // Ensure the exact final rotation is applied
            transform.rotation = Quaternion.Euler(endRotationEuler);
            objectRenderer.material.color = endColor;
        }
    }
}
