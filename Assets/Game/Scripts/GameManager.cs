using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] cars;
    [SerializeField] private Button quitButton , settingButton, resumeButton, carSelectButton;
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buttonClickSound;
    private int currentIndex;
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        settingPanel.SetActive(false);
        quitButton.onClick.AddListener(OnApplicationQuit);
        settingButton.onClick.AddListener(OnSettingPanel);
        resumeButton.onClick.AddListener(OnResumeButton);
        carSelectButton.onClick.AddListener(OnCarSelectButton);
        currentIndex = PlayerPrefs.GetInt("carIndex");
        GameObject car = Instantiate(cars[currentIndex], Vector3.zero, Quaternion.identity);
        Camera.main.GetComponent<CameraFollowScript>().SetCamera(car.transform);
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
        audioSource.PlayOneShot(buttonClickSound);
        
    }
    public void OnSettingPanel()
    {
        settingPanel.SetActive(true);
        audioSource.PlayOneShot(buttonClickSound);
        
    }
    public void OnResumeButton()
    {
        settingPanel.SetActive(false);
        audioSource.PlayOneShot(buttonClickSound);
    }
    public void OnCarSelectButton()
    {
        SceneManager.LoadScene("CarSelection");
        audioSource.PlayOneShot(buttonClickSound);
    }

}
