using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // public TextMeshProUGUI gameOverText;
    // public Button retryButton;
    public GameObject gameOverMenu;
    
    public float initialGameSpeed = 30f;
    public float gameSpeedAcceleration = 2;
    public float GameSpeed { get; private set; }

    private Player _player;
    private Spawner _spawner;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _spawner = FindObjectOfType<Spawner>();
        
        NewGame();
    }

    public void NewGame()
    {
        Objects[] objects = FindObjectsOfType<Objects>();

        foreach (var obj in objects)
        {
            Destroy(obj.gameObject);
        }
        
        GameSpeed = initialGameSpeed;
        enabled = true;
        
        _player.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        _player.gameObject.GetComponent<Animator>().SetBool("isDead", false);
        _spawner.gameObject.SetActive(true);
        gameOverMenu.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        GameSpeed = 0f;
        enabled = false;
        
        _player.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        _spawner.gameObject.SetActive(false);
        gameOverMenu.gameObject.SetActive(true);
    }
    
    private void Update()
    {
        GameSpeed += gameSpeedAcceleration * Time.deltaTime;
    }
}
