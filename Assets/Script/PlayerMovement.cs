//using UnityEngine;

//public class PlayerMovement : MonoBehaviour
//{
//    public float speed = 10f; // Car ki speed
//    public float rotationSpeed = 100f; // Car ki rotation speed
//    private bool isMovingForward = false; // Flag to track whether moving forward or not
//    private bool isRotatingLeft = false; // Flag to track left rotation
//    private bool isRotatingRight = false; // Flag to track right rotation

//    public void OnLeftButtonDown()
//    {
//        isRotatingLeft = true; // Flag for left rotation
//    }

//    public void OnRightButtonDown()
//    {
//        isRotatingRight = true; // Flag for right rotation
//    }

//    public void OnButtonUp()
//    {
//        isRotatingLeft = false; // Reset left rotation flag
//        isRotatingRight = false; // Reset right rotation flag
//    }

//    public void OnMoveForwardButtonDown()
//    {
//        isMovingForward = true;
//    }

//    public void OnMoveForwardButtonUp()
//    {
//        isMovingForward = false;
//    }

//    void Update()
//    {
//        if (isMovingForward)
//        {
//            // Move the car forward
//            transform.Translate(Vector3.forward * speed * Time.deltaTime);
//        }

//        if (isRotatingLeft)
//        {
//            // Rotate the car to the left
//            RotateCar(-1);
//        }

//        if (isRotatingRight)
//        {
//            // Rotate the car to the right
//            RotateCar(1);
//        }
//    }

//    public void RotateCar(int direction)
//    {
//        float rotationAmount = direction * rotationSpeed * Time.deltaTime;
//        transform.Rotate(Vector3.up, rotationAmount);
//    }
//}

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f; // Car ki speed
    public float rotationSpeed = 100f; // Car ki rotation speed
    private bool isMovingForward = false; // Flag to track whether moving forward or not
    private int rotationDirection = 0; // Track rotation direction: -1 for left, 1 for right
    public float forwordSpeed;
    public Rigidbody rb;
    public float iniitForce;
    private bool isStartButton = false;
    public GameObject startButton;
    public AudioClip startSound;
    public AudioClip boostSound;
    private AudioSource audioSource;
    public GameObject gameover;
    public GameObject reStartButton;

    public void OnLeftButtonDown()
    {
        rotationDirection = -1; // Set rotation direction to left
    }

    public void OnRightButtonDown()
    {
        rotationDirection = 1; // Set rotation direction to right
    }

    public void OnButtonUp()
    {
        rotationDirection = 0; // Reset rotation direction
    }

    public void OnMoveForwardButtonDown()
    {
        isMovingForward = true;
        audioSource.clip = boostSound;
        audioSource.Play();
    }

    public void OnMoveForwardButtonUp()
    {
        isMovingForward = false;
        rb.AddForce(0f, 0f, iniitForce);
        audioSource.clip = boostSound;
        audioSource.Pause();

    }
    public void OnMoveStartButtonDown()
    {
        isStartButton = true;
        startButton.SetActive(false);
        audioSource.clip = startSound;
        audioSource.loop = true;

        audioSource.Play();
        //CollisionEffect.isStartButton = false;

    }
    public void OnMoveStartButtonUp()
    {
        //gameover.SetActive(true);
        //reStartButton.SetActive(true);
        //isStartButton = false;

    }

    void Update()
    {
    

        if (isMovingForward)
        {
            // Move the car forward
            transform.Translate(0f, 0f, forwordSpeed * speed * Time.deltaTime);

        }
        else if (isStartButton)
        {
            transform.Translate(0f, 0f, iniitForce * speed * Time.deltaTime);
        }

        // Rotate the car based on the rotation direction
        float rotationAmount = rotationDirection * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, rotationAmount);
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Coins")
        {
            Destroy(collision.gameObject);
        }
    }

}

//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class PlayerMovement : MonoBehaviour
//{
//    public float speed = 10f; // Car ki speed
//    public float rotationSpeed = 100f; // Car ki rotation speed
//    private bool isMovingForward = false; // Flag to track whether moving forward or not
//    private int rotationDirection = 0; // Track rotation direction: -1 for left, 1 for right
//    public float forwardSpeed;
//    public Rigidbody rb;
//    public float initForce;
//    private bool isStartButton = false;
//    public GameObject startButton;
//    public AudioClip startSound;
//    public AudioClip boostSound;
//    private AudioSource audioSource;
//    public GameObject gameover;
//    public GameObject restartButton;
//    private bool isGameOver = false;

//    public void OnLeftButtonDown()
//    {
//        rotationDirection = -1; // Set rotation direction to left
//    }

//    public void OnRightButtonDown()
//    {
//        rotationDirection = 1; // Set rotation direction to right
//    }

//    public void OnButtonUp()
//    {
//        rotationDirection = 0; // Reset rotation direction
//    }

//    public void OnMoveForwardButtonDown()
//    {
//        if (!isGameOver) // Agar game over nahi hua hai
//        {
//            isMovingForward = true;
//            audioSource.clip = boostSound;
//            audioSource.Play();
//        }
//    }

//    public void OnMoveForwardButtonUp()
//    {
//        isMovingForward = false;
//        rb.AddForce(0f, 0f, initForce);
//        audioSource.clip = boostSound;
//        audioSource.Pause();
//    }

//    public void OnMoveStartButtonDown()
//    {
//        isStartButton = true;
//        startButton.SetActive(false);
//        audioSource.clip = startSound;
//        audioSource.loop = true;
//        audioSource.Play();
//    }

//    public void OnMoveStartButtonUp()
//    {
//        // Filhal yahan kuch nahi karna
//    }

//    public void OnRestartButtonDown()
//    {
//        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Scene ko reload karo
//    }

//    void Update()
//    {
//        if (isGameOver) return; // Agar game over hai to kuch mat karo

//        if (isMovingForward)
//        {
//            // Car ko aage badhao
//            transform.Translate(0f, 0f, forwardSpeed * speed * Time.deltaTime);
//        }
//        else if (isStartButton)
//        {
//            transform.Translate(0f, 0f, initForce * speed * Time.deltaTime);
//        }

//        // Car ko rotate karo
//        float rotationAmount = rotationDirection * rotationSpeed * Time.deltaTime;
//        transform.Rotate(Vector3.up, rotationAmount);
//    }

//    private void Start()
//    {
//        audioSource = GetComponent<AudioSource>();
//    }

//    public void GameOver()
//    {
//        isGameOver = true;
//        isMovingForward = false;
//        isStartButton = false;
//        gameover.SetActive(true);
//        restartButton.SetActive(true);
//        audioSource.Stop();
//    }

//    private void OnCollisionEnter(Collision collision)
//    {
//        if (collision.gameObject.CompareTag("Obstacle"))
//        {
//            GameOver();
//        }
//    }
//}
