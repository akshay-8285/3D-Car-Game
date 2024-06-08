//using UnityEngine;

//public class CollisionEffect : MonoBehaviour
//{
//    public GameObject smokeEffect; // Car par associated ParticleSystem GameObject ka reference
//    public AudioClip collisionSound;
//    void Start()
//    {
//        // Ensure karein ki smokeEffect active nahi hai start mein
//        if (smokeEffect != null)
//        {
//            smokeEffect.SetActive(false);
//        }
//    }

//    void OnCollisionEnter(Collision collision)
//    {
//        if(collisionSound != null)
//        {
//            AudioSource.PlayClipAtPoint(collisionSound, transform.position);

//            // Check karein ki collision obstacle se hui hai ya nahi
//            if (collision.gameObject.CompareTag("Obstacle"))
//        {
//            // Agar car par smoke effect GameObject mil gaya hai
//            if (smokeEffect != null)
//            {
//                // Smoke effect ko car ke sath move karo
//                smokeEffect.transform.position = transform.position;
//                smokeEffect.SetActive(true);
//            }
//            else
//            {
//                Debug.LogWarning("Smoke effect not assigned to the car GameObject!");
//            }

//            // Yahan aur bhi logic add kar sakte hain, jaise ki health ya score kam karna
//        }
//    }
//}
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionEffect : MonoBehaviour
{
    public GameObject smokeEffect; // Car par associated ParticleSystem GameObject ka reference
    //public AudioClip collisionSound; // Sound effect clip for collision
    public GameObject gameOver;
    public GameObject restartButton;
    public CollisionEffect instance;
    public GameObject gameoverpanel;
    public AudioClip gameOverSound;
    private AudioSource audioSource;

    //public static bool isStartButton { get; internal set; }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        //StartCoroutine(GameOver());
        // Ensure karein ki smokeEffect active nahi hai start mein
        if (smokeEffect != null)
        {
            smokeEffect.SetActive(false);
            gameOver.SetActive(false);
            restartButton.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Collision ke time par sound effect play karo, agar audio clip assign hai


        // Check karein ki collision obstacle se hui hai ya nahi
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Agar car par smoke effect GameObject mil gaya hai
            if (smokeEffect != null)
            {
                // Smoke effect ko car ke sath move karo
                smokeEffect.transform.position = transform.position;
                smokeEffect.SetActive(true);
                StartCoroutine(CarDestroy());
                gameoverpanel.SetActive(true);
                if(gameOverSound != null )
                {
                    audioSource.clip = gameOverSound;
                    audioSource.Play();
                }
                //transform.position = Vector3.zero; transform.rotation = Quaternion.identity;


            }

            else
            {
                Debug.LogWarning("Smoke effect not assigned to the car GameObject!");
            }
            //if (collisionSound != null)
            //{
            //    AudioSource.PlayClipAtPoint(collisionSound, transform.position);
            //}

            // Yahan aur bhi logic add kar sakte hain, jaise ki health ya score kam karna
        }
    }

    IEnumerator CarDestroy()
    {
        yield return new WaitForSeconds(2);
        //Destroy(gameObject);
        gameOver.SetActive(true);
        restartButton.SetActive(true);
       
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //instance = this;




        //StartCoroutine(GameOverPanel());



    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
//using System.Collections;
//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class CollisionEffect : MonoBehaviour
//{
//    public GameObject smokeEffect; // Car par associated ParticleSystem GameObject ka reference
//    public GameObject gameOver;
//    public GameObject restartButton;
//    private PlayerMovement playerMovement;

//    void Start()
//    {
//        // Ensure karein ki smokeEffect, gameOver aur restartButton active nahi hai start mein
//        if (smokeEffect != null)
//        {
//            smokeEffect.SetActive(false);
//        }
//        gameOver.SetActive(false);
//        restartButton.SetActive(false);

//        // PlayerMovement script ko retrieve karein
//        playerMovement = GetComponent<PlayerMovement>();
//    }

//    void OnCollisionEnter(Collision collision)
//    {
//        // Check karein ki collision obstacle se hui hai ya nahi
//        if (collision.gameObject.CompareTag("Obstacle"))
//        {
//            // Agar car par smoke effect GameObject mil gaya hai
//            if (smokeEffect != null)
//            {
//                // Smoke effect ko car ke sath move karo
//                smokeEffect.transform.position = transform.position;
//                smokeEffect.SetActive(true);
//                StartCoroutine(CarDestroy());
//            }
//            else
//            {
//                Debug.LogWarning("Smoke effect not assigned to the car GameObject!");
//            }

//            // PlayerMovement script ko inform karein ki game over ho gaya
//            if (playerMovement != null)
//            {
//                playerMovement.GameOver();
//            }
//        }
//    }

//    IEnumerator CarDestroy()
//    {
//        yield return new WaitForSeconds(2);
//        gameOver.SetActive(true);
//        restartButton.SetActive(true);
//    }

//    public void RestartGame()
//    {
//        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
//    }
//}
