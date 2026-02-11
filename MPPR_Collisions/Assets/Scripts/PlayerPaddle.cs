using UnityEngine;

public class PlayerPaddle : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 8f;

    [Header("Walls")]
    public Transform topWall;
    public Transform bottomWall;

    [Header("Ball")]
    public Transform ballPrefab;

    float paddleHalfHeight;
    float paddleHalfWidth;
    float ballRadius;

    float minY;
    float maxY;

    void Start()
    {
        paddleHalfHeight = transform.localScale.y * 0.5f;
        paddleHalfWidth = transform.localScale.x * 0.5f;

        ballRadius = ballPrefab.localScale.x * 0.5f;

        CalculateBounds();
    }

    void Update()
    {
        float inputY = 0f;

        if (Input.GetKey(KeyCode.W)) inputY = 1f;
        if (Input.GetKey(KeyCode.S)) inputY = -1f;

        if (inputY == 0f)
            return;

        Vector3 nextPosition = transform.position;
        nextPosition.y += inputY * moveSpeed * Time.deltaTime;

        // Clamp between walls
        nextPosition.y = Mathf.Clamp(nextPosition.y, minY, maxY);

        // Prevent intersection with ball
        if (!WillIntersectBall(nextPosition))
        {
            transform.position = nextPosition;
        }
    }

    void CalculateBounds()
    {
        float topWallBottom =
            topWall.position.y - topWall.localScale.y * 0.5f;

        float bottomWallTop =
            bottomWall.position.y + bottomWall.localScale.y * 0.5f;

        maxY = topWallBottom - paddleHalfHeight;
        minY = bottomWallTop + paddleHalfHeight;
    }

    bool WillIntersectBall(Vector3 nextPosition)
    {
        Vector2 paddleMin = new Vector2(
            nextPosition.x - paddleHalfWidth,
            nextPosition.y - paddleHalfHeight
        );

        Vector2 paddleMax = new Vector2(
            nextPosition.x + paddleHalfWidth,
            nextPosition.y + paddleHalfHeight
        );

        Vector2 ballPos = ballPrefab.position;

        float closestX = Mathf.Clamp(ballPos.x, paddleMin.x, paddleMax.x);
        float closestY = Mathf.Clamp(ballPos.y, paddleMin.y, paddleMax.y);

        float dx = ballPos.x - closestX;
        float dy = ballPos.y - closestY;

        return (dx * dx + dy * dy) < (ballRadius * ballRadius);
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(
            new Vector3(transform.position.x, minY),
            new Vector3(transform.position.x, maxY)
        );
    }
#endif
}
