using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerManager : MonoBehaviour
{
    //ctrl + /
    //cmd + /
    //inventory management
    //private List<Collectable> inventory = new List<Collectable>();
    public Text inventoryText;
    public Text descriptionText;
    private int currentIndex;

    // Player specific variables
    //private int health;
    //private int score;

    // Boolean values
    private bool isGamePaused = false;

    // UI stuff
    public Text healthText;
    public Text scoreText;
    public GameObject pauseMenu;
    public GameObject winMenu;
    public GameObject loseMenu;
    public PlayerInfo info; //keep track of health, score, and inventory in this
    public GameObject camera;

    // Start is called before the first frame update
    void Start()
    {   

        
        if (info == null)
        {
            info = GameObject.Find("PlayerInfo").GetComponent<PlayerInfo>();
        }
        // Makes sure game is "unpaused"
        isGamePaused = false;
        Time.timeScale = 1.0f;

        // Make sure all menus are filled in
        FindAllMenus();
        camera.SetActive(false);

        //Start player with initial health and score
        // info.health = 100;
        // info.score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (info.inventory.Count == 0)
        {
            inventoryText.text = "Current Selection: None";
            descriptionText.text = "";
        }
        else
        {
            inventoryText.text = "Current Selection: " + info.inventory[currentIndex].collectableName + " " + currentIndex.ToString();
            descriptionText.text = "Press [E] to " + info.inventory[currentIndex].description;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (info.inventory.Count > 0)
            {
                info.inventory[currentIndex].Use();
                info.inventory.RemoveAt(currentIndex);
                currentIndex = (currentIndex - 1) % info.inventory.Count;
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (info.inventory.Count > 0)
            {
                currentIndex = (currentIndex + 1) % info.inventory.Count;
            }
        }

        healthText.text = "Health: " + info.health.ToString();
        scoreText.text = "Score:  " + info.score.ToString();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }

        if (info.health < 100)
        {
            info.health += 1;
        }

        if (info.health <= 40)
        {
            camera.SetActive(true);
        }
        if (info.health <= 0)
        {
            LoseGame();
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collectable>() != null)
        {
            collision.GetComponent<Collectable>().player = this.gameObject;
            collision.gameObject.transform.parent = null;
            info.inventory.Add(collision.GetComponent<Collectable>());
            collision.gameObject.SetActive(false);
        }
    }

    void FindAllMenus()
    {
        if (healthText == null)
        {
            healthText = GameObject.Find("HealthText").GetComponent<Text>();
        }
        if (scoreText == null)
        {
            scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        }
        if (winMenu == null)
        {
            winMenu = GameObject.Find("WinGameMenu");

        }
        winMenu.SetActive(false);
        if (loseMenu == null)
        {
            loseMenu = GameObject.Find("LoseGameMenu");

        }
        loseMenu.SetActive(false);
        if (pauseMenu == null)
        {
            pauseMenu = GameObject.Find("PauseGameMenu");

        }
        pauseMenu.SetActive(false);
    }

    public void WinGame()
    {
        Time.timeScale = 0.0f;
        winMenu.SetActive(true);
    }

    public void LoseGame()
    {
        Time.timeScale = 0.0f;
        loseMenu.SetActive(true);
    }

    public void PauseGame()
    {
        if (isGamePaused)
        {
            // Unpause game
            Time.timeScale = 1.0f;
            pauseMenu.SetActive(false);
            isGamePaused = false;
        }
        else
        {
            // Pause game
            Time.timeScale = 0.0f;
            pauseMenu.SetActive(true);
            isGamePaused = true;
        }
    }

    public void ChangeHealth(int value)
    {
        info.health += value;
    }

    public void ChangeScore(int value)
    {
        info.score += value;
    }

    IEnumerator healthRegen()
    {
        info.health += 1;
        yield return new WaitForSeconds(1f);

    }

}
