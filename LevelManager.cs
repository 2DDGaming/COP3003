using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    Vector2 playerInitPosition;

    void Start()
    {
        //assign the player's initial position to the preset postion of Algo's object
        playerInitPosition = FindObjectOfType<Algo>().transform.position;
    }

    public void Restart()
    {
        //1 - Restart the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //2 - Reset the player's position, reset the score counter, reappear items in game
        // FindObjectOfType<Algo>().ResetPlayer();
        //Save the player's initial position, when game starts
        //When respawning reposition the player to the initial position
        // FindObjectOfType<Algo>().transform.position = playerInitPosition;
        //Reset life count
    }
}
