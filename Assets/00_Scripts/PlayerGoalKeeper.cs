using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGoalKeeper : MonoBehaviour
{
    //[SerializeField] private float smoothTime = 0.3f;
    [SerializeField] private Animator animator, animator2, animator3;

    private Vector3 targetPosition;
    private float speed = 10f;
    private float minX = -66.47f;
    private float maxX = -54.4f;

    public void LeftButton()
    {
        animator3.SetTrigger("left");
    }
    public void RightButton()
    {
        animator3.SetTrigger("right");
    }

}