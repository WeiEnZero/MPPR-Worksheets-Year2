using UnityEngine;

public class BallController : MonoBehaviour
{
    [Header("Ball Settings")]
    public float ballSpeed = 5f;
    public float speedMultiplier = 1.1f;
    public float boostDuration = 0.3f;
    public float ballRadius = 0.25f;

    [Header("Walls")]
    public Transform leftWall;
    public Transform rightWall;
    public Transform topWall;
    public Transform bottomWall;

    private Vector2 velocity;
    private float currentSpeed;
    private float boostTimer = 0f;

    void Start()
    {
        currentSpeed = ballSpeed;

        // Random initial direction (avoid too vertical or horizontal)
        float angle = Random.Range(-45f, 45f); // angle in degrees
        if (Random.value > 0.5f) angle += 180f; // randomly flip left/right

        float rad = angle * Mathf.Deg2Rad;
        velocity = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * currentSpeed;
    }

    void Update()
    {
        float dt = Time.deltaTime;

        // Handle boost timer
        if (boostTimer > 0f)
        {
            boostTimer -= dt;
            if (boostTimer <= 0f)
            {
                // Reset to normal speed
                currentSpeed = ballSpeed;
                velocity = velocity.normalized * currentSpeed;
            }
        }

        SweepMove(dt);
    }

    void SweepMove(float dt)
    {
        float remaining = dt;
        Vector2 pos = transform.position;

        // Handle multiple collisions in the same frame
        for (int i = 0; i < 3; i++) // prevent infinite loop
        {
            Vector2 movement = velocity * remaining;

            if (Sweep(pos, movement, out Vector2 normal, out float t))
            {
                // Move to impact point
                pos += movement * t;

                // Reflect and temporarily boost speed
                ReflectFromWall(normal, true);

                // Remaining movement after bounce
                remaining *= (1f - t);
            }
            else
            {
                pos += movement;
                break; // no collision this frame
            }
        }

        transform.position = pos;
    }

    public void ReflectFromPaddle(Vector2 normal)
    {
        velocity = Vector2.Reflect(velocity, normal).normalized * currentSpeed;
    }

    public void ReflectFromWall(Vector2 normal, bool temporaryBoost = false)
    {
        velocity = Vector2.Reflect(velocity, normal).normalized * currentSpeed;

        if (temporaryBoost)
        {
            currentSpeed = ballSpeed * speedMultiplier;
            velocity = velocity.normalized * currentSpeed;
            boostTimer = boostDuration;

            Debug.Log("It hits a wall - speed boosted temporarily!");
        }
        else
        {
            Debug.Log("It hits a wall");
        }
    }

    bool Sweep(Vector2 pos, Vector2 move, out Vector2 hitNormal, out float hitTime)
    {
        hitNormal = Vector2.zero;
        hitTime = 1f;
        bool hit = false;

        // RIGHT WALL
        if (move.x > 0f)
        {
            float targetX = rightWall.position.x - rightWall.localScale.x * 0.5f - ballRadius;
            float t = (targetX - pos.x) / move.x;
            float yAtImpact = pos.y + move.y * t;
            float minY = bottomWall.position.y + bottomWall.localScale.y * 0.5f;
            float maxY = topWall.position.y - topWall.localScale.y * 0.5f;
            if (t >= 0f && t <= hitTime && yAtImpact >= minY && yAtImpact <= maxY)
            {
                hit = true;
                hitTime = t;
                hitNormal = Vector2.left;
            }
        }

        // LEFT WALL
        if (move.x < 0f)
        {
            float targetX = leftWall.position.x + leftWall.localScale.x * 0.5f + ballRadius;
            float t = (targetX - pos.x) / move.x;
            float yAtImpact = pos.y + move.y * t;
            float minY = bottomWall.position.y + bottomWall.localScale.y * 0.5f;
            float maxY = topWall.position.y - topWall.localScale.y * 0.5f;
            if (t >= 0f && t <= hitTime && yAtImpact >= minY && yAtImpact <= maxY)
            {
                hit = true;
                hitTime = t;
                hitNormal = Vector2.right;
            }
        }

        // TOP WALL
        if (move.y > 0f)
        {
            float targetY = topWall.position.y - topWall.localScale.y * 0.5f - ballRadius;
            float t = (targetY - pos.y) / move.y;
            float xAtImpact = pos.x + move.x * t;
            float minX = leftWall.position.x + leftWall.localScale.x * 0.5f;
            float maxX = rightWall.position.x - rightWall.localScale.x * 0.5f;
            if (t >= 0f && t <= hitTime && xAtImpact >= minX && xAtImpact <= maxX)
            {
                hit = true;
                hitTime = t;
                hitNormal = Vector2.down;
            }
        }

        // BOTTOM WALL
        if (move.y < 0f)
        {
            float targetY = bottomWall.position.y + bottomWall.localScale.y * 0.5f + ballRadius;
            float t = (targetY - pos.y) / move.y;
            float xAtImpact = pos.x + move.x * t;
            float minX = leftWall.position.x + leftWall.localScale.x * 0.5f;
            float maxX = rightWall.position.x - rightWall.localScale.x * 0.5f;
            if (t >= 0f && t <= hitTime && xAtImpact >= minX && xAtImpact <= maxX)
            {
                hit = true;
                hitTime = t;
                hitNormal = Vector2.up;
            }
        }

        return hit;
    }
}
