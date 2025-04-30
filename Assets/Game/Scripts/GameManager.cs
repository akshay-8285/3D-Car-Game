using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] cars;
    private int currentIndex;
    void Start()
    {
        currentIndex = PlayerPrefs.GetInt("carIndex");
        GameObject car = Instantiate(cars[currentIndex], Vector3.zero, Quaternion.identity);
    }

}
