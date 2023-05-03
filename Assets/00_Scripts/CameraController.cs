using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public Camera goalkeeperCamera; // Kaleci kamera objesi

    // [SerializeField] private Transform target; // kamera hedefi, örneğin karakter nesnesi
    // [SerializeField] private float smoothTime = 0.3f; // smooth hareket süresi
    // [SerializeField] private Vector3 offset; // hedef ile kamera arasındaki mesafe
     [SerializeField] private Button button; // açıyı değiştirecek buton

    // private Vector3 refVelocity = Vector3.zero; // smooth hareket için gerekli değişken
    // private Quaternion targetRotation; // kamera hedef açısı

    private void Start()
    {
        // başlangıçta hedef açısı olarak kamera yönünü alıyoruz
        //targetRotation = transform.rotation;
        //butona tıklama olayı ekle
         button.onClick.AddListener(ChangeCameraAngle);
    }

    // private void FixedUpdate()
    // {
    //     // hedef konumunu ve kamera pozisyonunu güncelle
    //     Vector3 targetPosition = target.position + offset;
    //     transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref refVelocity, smoothTime);
    //     transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothTime);
    // }

    // private void ChangeCameraAngle()
    // {
    //     // butona tıklandığında kamera açısını belirli bir açıya döndür
    //     targetRotation = Quaternion.Euler(0f, 120f, 0f);

    // }
    private void ChangeCameraAngle()
    {
        // Kaleci kamera objesine geçiş yap
        Camera.main.gameObject.SetActive(false);
        goalkeeperCamera.gameObject.SetActive(true);

    }

}
