using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarSelector : MonoBehaviour
{
    [SerializeField] private GameObject[] cars;
    [SerializeField] private Button previousButton , nextButton , PlayButton , quitButton;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buttonClickSound;
    private int currentIndex;

    private void Awake()
    {
        
        currentIndex = PlayerPrefs.GetInt("carIndex");

        for(int i = 0; i< cars.Length; i++)
        {
           cars[i].SetActive(false);
           cars[currentIndex].SetActive(true);
        }
    }
    void Start()
    {

        nextButton.onClick.AddListener(NextCar);
        previousButton.onClick.AddListener(previousCar);
        PlayButton.onClick.AddListener(PlayGame);
        quitButton.onClick.AddListener(OnApplicationQuit);
        
    }


    public void Update()
    {
        if(currentIndex >= cars.Length -1)
        {
            nextButton.interactable = false;
        }
        else
        {
            nextButton.interactable = true;
        }
        if(currentIndex <= 0)
        {
            previousButton.interactable = false;
        }
        else
        {
            previousButton.interactable = true;
        }
    }

    public void NextCar()
    {
        currentIndex++;
        for(int i = 0; i < cars.Length; i++)
        {
            cars[i].SetActive(false);
            cars[currentIndex].SetActive(true);

        }
        audioSource.PlayOneShot(buttonClickSound);
        PlayerPrefs.SetInt("carIndex", currentIndex);
        PlayerPrefs.Save();
    }

    public void previousCar()
    {
        currentIndex--;
        for(int i = 0; i < cars.Length; i++)
        {
            cars[i].SetActive(false);
            cars[currentIndex].SetActive(true);

        }
        audioSource.PlayOneShot(buttonClickSound);
        PlayerPrefs.SetInt("carIndex", currentIndex);
        PlayerPrefs.Save();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
        audioSource.PlayOneShot(buttonClickSound);
    }
    public void OnApplicationQuit()
    {
        Application.Quit();
        audioSource.PlayOneShot(buttonClickSound);
    }




}
