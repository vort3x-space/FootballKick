using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class GoalPost : MonoBehaviour
{
    public BallAIScript AI;
    public AudioClip audioData;
    public AudioSource audioSource;
    [SerializeField] private Animator animator;
    [SerializeField] private string playerPrefsKeyPrefix = "goalStatus";
    public GameObject UI, goalkeeperUI, goalkeepercamera, ball, aiBall;
    public GameObject _camera;
    public TextMeshProUGUI goalText;
    public Image[] goalStatusImages;
    public int currentShot;

    private bool scored = false;
    private bool nextShot = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!scored)
        {
            if (collision.gameObject.CompareTag("Goalpost"))
            {
                scored = true;
                UpdateGoalStatusImage(currentShot, Color.green);
                SaveGoalStatus(currentShot, 1); // 1 for goal scored
                audioSource.PlayOneShot(audioData);
                StartCoroutine(VictoryGoal());
            }
            else if (collision.gameObject.CompareTag("NoGoalPost"))
            {
                scored = true;
                UpdateGoalStatusImage(currentShot, Color.red);
                SaveGoalStatus(currentShot, 0); // 0 for no goal
                nextShot = true;
            }
        }
    }

    private IEnumerator VictoryGoal()
    {
        yield return new WaitForSeconds(0.5f);
        goalText.text = "Goal!";
        animator.SetTrigger("victory");
        SwitchCameraAndView();
    }
    private IEnumerator SwitchGoalKeeperCamera()
    {
        yield return new WaitForSeconds(0.5f);
        ball.transform.position = new Vector3(-60.3f, -38.301f, 105.009f);
        ball.SetActive(false);
        aiBall.SetActive(true);
        _camera.SetActive(false);
        goalkeepercamera.SetActive(true);
        goalkeeperUI.SetActive(true);
        goalText.text = "";
        AI.ApplyRandomPowerToBallAI();
    }

    private void SwitchCameraAndView()
    {
        UI.SetActive(false);
        Vector3 position = new Vector3(-54.18f, -33.38f, 110.3f);
        Vector3 endValue = new Vector3(7.097f, -132.47f, 0);
        _camera.transform.DORotate(endValue, 2f, RotateMode.WorldAxisAdd).SetRelative().SetEase(Ease.InQuad);
        _camera.transform.DOMove(position, 2f).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            StartCoroutine(SwitchGoalKeeperCamera());
            goalText.text = "Your Turn!";
        });
    }

    private void Start()
    {
        currentShot = 0;
        // Load goal status from PlayerPrefs and set image colors accordingly
        for (int i = 0; i < goalStatusImages.Length; i++)
        {
            int goalStatus = PlayerPrefs.GetInt(playerPrefsKeyPrefix + i, 0);
            if (goalStatus == 1)
            {
                goalStatusImages[i].color = Color.green;
                currentShot = i + 1;
            }
            else
            {
                goalStatusImages[i].color = Color.red;
            }
        }
        // Update the remaining goal status images
        for (int i = currentShot; i < goalStatusImages.Length; i++)
        {
            if (nextShot && i == currentShot)
            {
                goalStatusImages[i].color = Color.red;
            }
            else
            {
                goalStatusImages[i].color = Color.white;
            }
        }
    }

    private void UpdateGoalStatusImage(int index, Color color)
    {
        if (index >= 0 && index < goalStatusImages.Length)
        {
            goalStatusImages[index].color = color;
        }
    }

    private void SaveGoalStatus(int index, int status)
    {
        PlayerPrefs.SetInt(playerPrefsKeyPrefix + index, status);
        PlayerPrefs.Save();
    }
}
