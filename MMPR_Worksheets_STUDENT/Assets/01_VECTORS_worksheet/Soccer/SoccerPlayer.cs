using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class SoccerPlayer : MonoBehaviour
{
    public bool IsCaptain = false;
    public SoccerPlayer[] OtherPlayers;
    public float rotationSpeed = 1f;

    float angle = 0f;

    private void Start()
    {
        OtherPlayers = FindObjectsOfType<SoccerPlayer>().Where(t => t != this).ToArray();
        FindMinimum();
    }

    void FindMinimum()
    {
        for (int i = 0; i < 10; i++)
        {
            float height = Random.Range(5f, 20f);
            Debug.Log(height);
        }
    }

    float Magnitude(Vector3 vector)
    {
        return vector.magnitude;
    }

    Vector3 Normalise(Vector3 vector)
    {
        vector = Vector3.Normalize(vector);
        return vector;
    }

    float Dot(Vector3 b)
    {
        Vector3 a = transform.forward;
        return (a.x * b.x) + (a.y * b.y) + (a.z * b.z);
    }

    SoccerPlayer FindClosestPlayerDot()
    {
        SoccerPlayer closest = null;
        float minAngle = 180f;

        for (int i = 0; i < OtherPlayers.Length; i++)
        {
            Vector3 toPlayer = OtherPlayers[i].transform.position - transform.position;
            var normalizedVectorToPlayer = toPlayer.normalized;

            float dot = Dot(normalizedVectorToPlayer);
            float angle = Mathf.Acos(dot);
            angle = angle * Mathf.Rad2Deg;

            if (angle < minAngle)
            {
                minAngle = angle;
                closest = OtherPlayers[i];
            }
        }

        return closest;
    }

    void DrawVectors()
    {
        foreach (SoccerPlayer other in OtherPlayers)
        {
            if (IsCaptain)
            {
                Vector3 vectorToOtherPlayers = other.transform.position - transform.position;
                Debug.DrawRay(transform.position, vectorToOtherPlayers, Color.black);
            }
        }
    }

    void Update()
    {
        DebugExtension.DebugArrow(transform.position, transform.forward, Color.red);
        

        if (IsCaptain)
        {
            angle += Input.GetAxis("Horizontal") * rotationSpeed;
            transform.localRotation = Quaternion.AngleAxis(angle, Vector3.up);
            Debug.DrawRay(transform.position, transform.forward * 10f, Color.red);

            DrawVectors();

            SoccerPlayer targetPlayer = FindClosestPlayerDot();
            if (targetPlayer != null)
            {
                targetPlayer.GetComponent<Renderer>().material.color = Color.green;

                foreach (SoccerPlayer other in OtherPlayers.Where(t => t != targetPlayer))
                {
                    other.GetComponent<Renderer>().material.color = Color.white;
                }
            }
        }
    }
}


