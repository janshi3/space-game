using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float restartDelay = 1f;
    public GameObject completeLevelUI;
    public GameObject gameOverUI;
    public GameObject planetGreen;
    public GameObject planetRed;
    public static bool generalRed;
    public static bool generalGreen;
    public static float playerLife;
    public static bool powerupHeld;
    public static int shipID = 1;

    private void Start()
    {
        GeneratePlanets();
        powerupHeld = false;
        generalRed = false;
        generalGreen = false;
        playerLife = 3;
    }

    private void LateUpdate()
    {
        if (GameObject.FindGameObjectsWithTag("RedPlanet").Length == 0)
        {
            CompleteLevel();
        }
    }
    public void GameOver()
    {
        gameOverUI.SetActive(true);
        Invoke("Restart", 3f);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CompleteLevel()
    {
        completeLevelUI.SetActive(true);
    }

    void GeneratePlanets()
    {
        Vector3[] planets = null;
        for (int i = 0; i < Random.Range(1, 3); i++)
        {
            Vector3 pos = new Vector3(Random.Range(-100, 100), -10f, Random.Range(-100, 100));
            bool tooClose = false;
            if (planets != null)
            {
                foreach (Vector3 planet in planets)
                {
                    float distance = 40f;
                    float xPos = pos.x - planet.x;
                    if (xPos < 0)
                    {
                        xPos *= -1;
                    }
                    float zPos = pos.z - planet.z;
                    if (zPos < 0)
                    {
                        zPos *= -1;
                    }
                    if (xPos + zPos < distance)
                    {
                        tooClose = true;
                    }
                }
            }

            if (tooClose)
            {
                i--;
            }

            else
            {
                GameObject.Instantiate(planetGreen, pos, planetGreen.transform.rotation);
            }
            
        }

        for (int i = 0; i < Random.Range(4, 6); i++)
        {
            Vector3 pos = new Vector3(Random.Range(-150, 150), -10f, Random.Range(-150, 150));
            bool tooClose = false;
            if (planets != null)
            {
                foreach (Vector3 planet in planets)
                {
                    float distance = 40f;
                    float xPos = pos.x - planet.x;
                    if (xPos < 0)
                    {
                        xPos *= -1;
                    }
                    float zPos = pos.z - planet.z;
                    if (zPos < 0)
                    {
                        zPos *= -1;
                    }
                    if (xPos + zPos < distance)
                    {
                        tooClose = true;
                    }
                }
            }

            if (tooClose)
            {
                i--;
            }

            else
            {
                GameObject.Instantiate(planetRed, pos, planetRed.transform.rotation);
            }

        }
    }
}
