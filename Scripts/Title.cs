using UnityEngine;

public class Title : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        Debug.Log( "Start Game button clicked!" );
        // Implement logic to start the game, such as loading the main game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene( "MainStage" ); // Replace with your actual scene name
    }
}
