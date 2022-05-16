using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delaySeconds = 1f;

    private void OnCollisionEnter(Collision collision)
    {
        string collidedTag = collision.gameObject.tag;
        switch(collidedTag)
        {
            case "Safe":
                Debug.Log("Collided with safe object");
                break;
            case "Finish":
                InitiateShipLanding();
                break;
            default:
                InitiateShipCrash();
                break;
        }
    }

    void InitiateShipCrash()
    {
        // TODO: add particle effects for crash
        // TODO: add sound effect for crash
        DisableMovement();
        Invoke("ReloadLevel", delaySeconds);
    }

    void InitiateShipLanding()
    {
        DisableMovement();
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
