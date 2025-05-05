using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashController : MonoBehaviour
{
    [SerializeField] private Image loadingImage;
    [SerializeField] private float reduceSpeed = 0.01f;
    [SerializeField] private float ammount  = 1f;
    //private float currentAmmount;

    private void Start()
    {
        // currentAmmount = ammount;
    }
    private void FixedUpdate()
    {
        AmmountOfImage();
    }

    private void AmmountOfImage()
    {
        loadingImage.fillAmount += reduceSpeed * Time.deltaTime;
        if(loadingImage.fillAmount >= ammount)
        {
            SceneManager.LoadScene("CarSelection");
        }
    }
}
