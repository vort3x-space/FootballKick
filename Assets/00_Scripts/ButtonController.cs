using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;



public class ButtonController : MonoBehaviour
{
    [SerializeField] private Animator animator; // Animasyon bileşeni
    [SerializeField] private Button button;
    [SerializeField] private Button button2; // Animasyonu başlatmak için buton
    public ParticleSystem ballparticleSystem;
    public Rigidbody rb;
    public float specialForceValue;

    private void Start()
    {
        ballparticleSystem.Stop();
        button.onClick.AddListener(PlayAnimation);
        button2.onClick.AddListener(PlayAnimation2);

    }
    private void PlayAnimation()
    {
        animator.SetTrigger("left");
    }
    private void PlayAnimation2()
    {
        animator.SetTrigger("right");
    }
    public void AddSpecialForce()
    {
        rb.AddForce(transform.forward * specialForceValue);
        ballparticleSystem.Play();
    }
    public void RestartDemoScene()
    {
        SceneManager.LoadScene(1);
    }

}
