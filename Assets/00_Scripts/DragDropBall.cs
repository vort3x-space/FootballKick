using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropBall : MonoBehaviour
{
    [SerializeField] private Animator animatorshooter, animatorgaolkeeper;
    public AudioClip audioData;
    public AudioSource audioSource;

    public GameObject ball;
    public float forceMultiplier = 1.0f;
    public float speed = 1.0f;
    public LineRenderer lineRendererPrefab;

    private List<Vector3> _swipePoints = new List<Vector3>();
    private bool _isSwiping;
    private LineRenderer _lineRenderer;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isSwiping = true;
            _swipePoints.Clear();

            if (_lineRenderer != null)
            {
                Debug.Log("çalış");
                Destroy(_lineRenderer.gameObject);
            }

            _lineRenderer = Instantiate(lineRendererPrefab);
            Vector3 startPoint = GetWorldPointFromScreen(Input.mousePosition);
            startPoint.x = -60.53f;
            startPoint.y = -38.301f;
            startPoint.z = 105.009f;
            _swipePoints.Add(startPoint);
        }

        if (Input.GetMouseButton(0) && _isSwiping)
        {
            Vector3 newPoint = GetWorldPointFromScreen(Input.mousePosition);
            if (Vector3.Distance(newPoint, _swipePoints[_swipePoints.Count - 1]) > 0.1f)
            {
                _swipePoints.Add(newPoint);
            }
        }

        if (Input.GetMouseButtonUp(0) && _isSwiping)
        {
            _isSwiping = false;
            animatorshooter.SetTrigger("shoot");
            audioSource.PlayOneShot(audioData);
            Destroy(_lineRenderer.gameObject);
            StartCoroutine(WaittheShoot());
        }

        if (_lineRenderer != null && _swipePoints.Count > 0)
        {
            _lineRenderer.positionCount = _swipePoints.Count;
            for (int i = 0; i < _swipePoints.Count; i++)
            {
                _lineRenderer.SetPosition(i, _swipePoints[i]);
            }
        }
    }
    private Vector3 GetWorldPointFromScreen(Vector3 screenPoint)
    {
        float depth = Mathf.Lerp(1.0f, 45.0f, screenPoint.y / Screen.height);
        return Camera.main.ScreenToWorldPoint(new Vector3(screenPoint.x, screenPoint.y, depth));
    }
    private IEnumerator MoveBallAlongPath()
    {
        int currentPointIndex = 0;

        while (currentPointIndex < _swipePoints.Count - 1)
        {
            float distance = Vector3.Distance(ball.transform.position, _swipePoints[currentPointIndex + 1]);
            float timeToReachNextPoint = distance / speed;

            float elapsedTime = 0;
            Vector3 startPoint = ball.transform.position;
            Vector3 endPoint = _swipePoints[currentPointIndex + 1];

            while (elapsedTime < timeToReachNextPoint)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / timeToReachNextPoint;

                ball.transform.position = Vector3.Lerp(startPoint, endPoint, t);

                yield return null;
            }

            currentPointIndex++;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("left"))
        {
            animatorgaolkeeper.SetTrigger("left");
            StartCoroutine(WaittheSave());
        }
        else if (other.CompareTag("right"))
        {
            animatorgaolkeeper.SetTrigger("right");
            StartCoroutine(WaittheSave());
        }
    }
    private IEnumerator WaittheShoot()
    {
        animatorshooter.SetTrigger("idle");
        yield return new WaitForSeconds(0.7f);
        StartCoroutine(MoveBallAlongPath());
    }
    private IEnumerator WaittheSave()
    {
        yield return new WaitForSeconds(0.2f);
        animatorgaolkeeper.SetTrigger("idle");
    }
}

