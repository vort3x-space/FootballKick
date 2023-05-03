using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAIScript : MonoBehaviour
{
    public GameObject ballAI; 
    public List<Vector3> points = new List<Vector3>(); 
    public float powerMin = 1f; 
    public float powerMax = 100f; 
    public float moveDuration = 1f; 
    public float forceDuration = 1f; 

    public void ApplyRandomPowerToBallAI()
    {
        StartCoroutine(ApplyPowerSmoothly());
    }

    IEnumerator ApplyPowerSmoothly()
    {
        yield return new WaitForSeconds(0.5f);
        // Select a random point from the list
        int randomIndex = Random.Range(0, points.Count);
        Vector3 randomPoint = points[randomIndex];

        // Generate a random power value
        float randomPower = Random.Range(powerMin, powerMax);

        // Smoothly move the ball AI to the target point
        float moveStartTime = Time.time;
        Vector3 initialPosition = ballAI.transform.position;
        while (Time.time - moveStartTime < moveDuration)
        {
            float t = (Time.time - moveStartTime) / moveDuration;
            ballAI.transform.position = Vector3.Lerp(initialPosition, randomPoint, t);
            yield return null;
        }

        // Apply the force gradually over time
        Rigidbody rb = ballAI.GetComponent<Rigidbody>();
        if (rb != null)
        {
            float forceStartTime = Time.time;
            while (Time.time - forceStartTime < forceDuration)
            {
                float t = (Time.time - forceStartTime) / forceDuration;
                rb.AddForce(Vector3.up * randomPower * t, ForceMode.Force);
                yield return null;
            }
        }
    }
}
