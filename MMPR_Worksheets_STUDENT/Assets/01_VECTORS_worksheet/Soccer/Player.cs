using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Player : MonoBehaviour
{
    public bool IsCaptain = true;
    public Player OtherPlayer;

    float Magnitude(Vector3 vector)
    {
        return vector.magnitude;
    }

    Vector3 Normalise(Vector3 vector)
    {
        vector = Vector3.Normalize(vector);
        return vector;
    }

    float Dot(Vector3 vectorA, Vector3 vectorB)
    {
         return (vectorA.x * vectorB.x) + (vectorA.y * vectorB.y) + (vectorA.z * vectorB.z);
    }

    float AngleToPlayer(Player player)
    {
        //    // Steps to calculate the angle between the direction Captain is facing and 
        //    // the direction from Captain to Other
        //    // 1. A
        //    // 2. B
        //    // 3. Calculate the angle between the vectors from A and B
        //    //    3.1 Calculate the dot product between A and B
        //    //       3.1.1 Calculate the magnitude of the vector from Captain to Other (A)
        //    //       3.1.2 Normalise the vector from Captain to Other
        //    //       3.1.3 Calculate the dot product of the normalised vectors A and B
        //    //    3.2 From the dot product, calculate the actual angle between A and B
        //    //       3.2.1 Take the arc cosine (acos) of the dot product (because if 
        //    //             both vector are normalised, the dot product equals the cos
        //    //             of the angle between vectors A and B.
        //    //       3.2.2 The acos angle returned is in radians. We must convert to
        //    //             degrees.

        //    // A
        //    // Steps to draw the arrow that represents the vector from Captain to Other 
        //    // 1. Find the vector from Captain to Other
        //    //     1.1 Find the position of Captain (A -- from)
        //    //     1.2 Find the position of Other (B -- to)
        //    //     1.3 Calculate B-A to get the vector from A to B
        //    // 2. Draw the arrow to represent visually the vector AB
        //    //
        DebugExtension.DebugArrow(transform.position, OtherPlayer.transform.position - transform.position, Color.black);


        //    // B
        //    // Steps to draw an arrow that represents which direction Captain is facing
        //    // 1. Find which vector to draw as an arrow
        //    //    1.1 The transform.forward vector of Captain
        //    // 2. Draw the arrow to represent visually the the transform.forward vector of Captain
        DebugExtension.DebugArrow(transform.position, transform.forward, Color.blue);

        //    // CALCULATING THE ANGLE

        Vector3 vectorToOtherPlayer = OtherPlayer.transform.position - transform.position;

        Vector3 vectorA = transform.forward;
        Vector3 vectorB = OtherPlayer.transform.position;

        var magnitude = Magnitude(vectorToOtherPlayer);
        var normlisationVectorA = Normalise(vectorA);
        var normlisationVectorB = Normalise(vectorToOtherPlayer);
        var dotProduct = Dot(normlisationVectorA, normlisationVectorB);

        float angle = Mathf.Acos(dotProduct) * Mathf.Rad2Deg;

        Debug.Log($"Angle from {gameObject.name} to {OtherPlayer.gameObject.name} is {angle}");

        return angle;
    }

    void Update()
    {
        if (IsCaptain)
        {
            DebugExtension.DebugArrow(transform.position,  OtherPlayer.transform.position - transform.position, Color.black);
            float angle = AngleToPlayer(OtherPlayer);
            Debug.Log(angle);
        }
    }
}
