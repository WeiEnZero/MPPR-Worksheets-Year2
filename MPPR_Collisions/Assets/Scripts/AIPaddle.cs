using UnityEngine;

public class AIPaddle : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;        // How fast the AI paddle moves
    [Range(0f, 1f)]
    public float reactionFactor = 0.8f; // How "perfectly" AI tracks the ball (1 = perfect)

    [Header("Walls")]
    public Transform topWall;
    public Transform bottomWall;

    [Header("Ball")]
    public Transform ball;

    private float paddleHalfHeight;
    private float minY;
    private float maxY;

    void Start()
    {
        paddleHalfHeight = transform.localScale.y * 0.5f;
        CalculateBounds();
    }

    void Update()
    {
        // Target Y position based on ball
        float targetY = Mathf.Lerp(
            transform.position.y,
            ball.position.y,
            reactionFactor
        );

        // Clamp inside walls
        targetY = Mathf.Clamp(targetY, minY, maxY);

        // Move smoothly towards the target Y
        Vector3 nextPosition = transform.position;
        nextPosition.y = Mathf.MoveTowards(
            transform.position.y,
            targetY,
            moveSpeed * Time.deltaTime
        );

        transform.position = nextPosition;
    }

    void CalculateBounds()
    {
        float topWallBottom = topWall.position.y - topWall.localScale.y * 0.5f;
        float bottomWallTop = bottomWall.position.y + bottomWall.localScale.y * 0.5f;

        maxY = topWallBottom - paddleHalfHeight;
        minY = bottomWallTop + paddleHalfHeight;
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(
            new Vector3(transform.position.x, minY),
            new Vector3(transform.position.x, maxY)
        );
    }
#endif
}
