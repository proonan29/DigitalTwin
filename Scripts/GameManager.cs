using UnityEngine;
namespace StarterAssets
{
        public class GameManager : MonoBehaviour
        {
                private static GameManager instance = null;
                public int lives = 3;
                public Sprite heartImage;
                public GameObject heartUI;
                public GameObject heartPrefab;
                public static GameManager Instance
                {
                        get
                        {
                                return instance;
                        }
                }

                // Start is called once before the first execution of Update after the MonoBehaviour is created
                void Start()
                {
                        if ( instance == null )
                                instance = this;
                        else
                        {
                                Destroy( gameObject );
                                return;
                        }
                        // Initialize the heart UI based on the number of lives
                        for ( int i = 0; i < lives; i++ )
                        {
                                GameObject heart = Instantiate( heartPrefab, heartUI.transform );
                                heart.GetComponent<UnityEngine.UI.Image>().sprite = heartImage;
                                heart.transform.localPosition = new Vector3( i * 50, 0, 0 ); // Adjust the position of each heart
                        }
                }

                // Update is called once per frame
                void Update()
                {

                }

                public void DecreaseLife( DeathControl player )
                {
                        lives--;
                        Debug.Log( "Player has " + lives + " lives remaining." );
                        // Remove one heart from the UI
                        if ( heartUI.transform.childCount > 0 )
                        {
                                Destroy( heartUI.transform.GetChild( heartUI.transform.childCount - 1 ).gameObject );
                        }
                        if ( lives <= 0 )
                        {
                                Debug.Log( "Game Over!" );
                                // Implement game over logic here (e.g., show game over screen, restart level, etc.)
                                player.Death();
                        }
                        else
                        {

                        }
                }
        }
}
