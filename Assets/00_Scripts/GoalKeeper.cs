using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalKeeper : MonoBehaviour
{
    public Transform ball;

    public float movementSpeed = 5f;
    public float jumpForce = 500f;
    public float jumpDelay = 0.5f;
    private float defaultYPosition;
    private Rigidbody rb;
    private bool isJumping = false;
    private bool isDragging = false;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        defaultYPosition = transform.position.y;
        //rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
    }

    void FixedUpdate()
    {
        if (!isDragging)
        {
            // Calculate direction to move towards ball
            Vector3 direction = ball.position - transform.position;
            direction.y = 0;

            // Move towards ball
            rb.MovePosition(transform.position + direction.normalized * movementSpeed * Time.fixedDeltaTime);

            // Jump if the ball is above the goalie and not currently jumping
            if (ball.position.y > transform.position.y && !isJumping)
            {
                StartCoroutine(Jump());
            }
        }
    }

    IEnumerator Jump()
    {
        isJumping = true;
        yield return new WaitForSeconds(jumpDelay);
        rb.AddForce(Vector3.up * jumpForce);
        isJumping = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Jump when the ball collides with the goal
        StartCoroutine(Jump());
    }

    private Vector3 mousePressDownPos;
    private Vector3 mouseReleasePos;

    private void OnMouseDown()
    {
        isDragging = true;
        mousePressDownPos = Input.mousePosition;

    }

    private void OnMouseUp()
    {
        isDragging = false;
        mouseReleasePos = Input.mousePosition;
        Shoot(mouseReleasePos - mousePressDownPos);
        transform.position = new Vector3(transform.position.x, defaultYPosition, transform.position.z);
        rb.constraints = RigidbodyConstraints.FreezePositionZ;
    }

    private float forceMultiplier = 1;

    private void Shoot(Vector3 force)
    {
        if (isJumping)
            return;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        Vector3 targetPosition = ball.position + (new Vector3(force.x, 0, force.y) * forceMultiplier);
        StartCoroutine(MoveToTarget(targetPosition));
    }
    private IEnumerator MoveToTarget(Vector3 targetPosition)
    {
        float t = 0f;
        Vector3 startingPos = transform.position;
        float distance = Vector3.Distance(startingPos, targetPosition);
        float timeToReachTarget = distance / movementSpeed;

        while (t < 1f)
        {
            t += Time.deltaTime / timeToReachTarget;
            transform.position = Vector3.Lerp(startingPos, targetPosition, t);
            yield return null;
        }

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
