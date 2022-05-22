using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] float delaySeconds = 1f;
    [SerializeField] AudioClip finishSound;
    [SerializeField] AudioClip crashSound;
    [SerializeField] ParticleSystem finishParticles;
    [SerializeField] ParticleSystem crashParticles;

    bool isTransitioning = false;
    bool enableCollision = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        ToggleDebugKeys();
    }

    private void ToggleDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            enableCollision = !enableCollision;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        string collidedTag = collision.gameObject.tag;
        if (isTransitioning)
        {
            return;
        }
        switch(collidedTag)
        {
            case "Safe":
                // Debug.Log("Collided with safe object");
                break;
            case "Finish":
                InitiateShipLanding();
                break;
            default:
                if(enableCollision)
                {
                    InitiateShipCrash();
                }
                break;
        }
    }

    void InitiateShipCrash()
    {
        DisableMovement();
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        crashParticles.Play();
        isTransitioning = true;
        Invoke("ReloadLevel", delaySeconds);
    }

    void InitiateShipLanding()
    {
        DisableMovement();
        audioSource.Stop();
        audioSource.PlayOneShot(finishSound);
        finishParticles.Play();
        isTransitioning = true;
        Invoke("LoadNextLevel", delaySeconds);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void DisableMovement()
    {
        GetComponent<Movement>().enabled = false;
    }
}
