using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class CutSceneController : MonoBehaviour
{
    public Camera _camera;

    private void Start()
    {
        _camera.transform.DOMove(new Vector3(-33f, 6.1f, 23.7f), 2).SetEase(Ease.Linear).OnComplete(() =>
        {
            _camera.transform.DOMove(new Vector3(-27f, -27.1f, 67f), 2).SetRelative().SetEase(Ease.Linear);
            _camera.transform.DORotate(new Vector3(7.097f, 0f, 0f), 2f).SetEase(Ease.InOutSine).OnComplete(() =>
            _camera.transform.DOMove(new Vector3(0f, -11.7f, 0f), 1).SetRelative().SetEase(Ease.InOutFlash).OnComplete(() =>
            _camera.transform.DOMove(new Vector3(0f, 0f, 4f), 2).SetRelative().SetEase(Ease.Linear).OnComplete(() =>
            SceneManager.LoadScene(1))));
        });

    }
}