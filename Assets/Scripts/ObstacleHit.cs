using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstacleHit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log(gameObject.name + "Hit: " + other.name + "Restarting. .");
            SceneManager.LoadScene("Gameplay");
        }
    }
}
