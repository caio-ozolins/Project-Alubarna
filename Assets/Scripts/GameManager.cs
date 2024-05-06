using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject gameOverMenu;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI coinsText;
    
    
    public float initialGameSpeed = 30f;
    public float gameSpeedAcceleration = 2;
    public float GameSpeed { get; private set; }

    private Player _player;
    private Spawner _spawner;

    private float _score;
    private static readonly int IsDead = Animator.StringToHash("isDead");

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
        _score = 0f;
        
        _player.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        _player.gameObject.GetComponent<Animator>().SetBool(IsDead, false);
        _player.gameObject.GetComponent<PlayerInput>().enabled = true;
        _spawner.gameObject.SetActive(true);
        gameOverMenu.gameObject.SetActive(false);
        
        UpdateHighScore();
    }

    public void GameOver()
    {
        GameSpeed = 0f;
        enabled = false;
        
        _player.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        _spawner.gameObject.SetActive(false);
        gameOverMenu.gameObject.SetActive(true);
        
        UpdateHighScore();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    private void Update()
    {
        GameSpeed += gameSpeedAcceleration * Time.deltaTime;
        _score += GameSpeed * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(_score).ToString("D6");
        coinsText.text = _player.currency.ToString("D3");
    }

    private void UpdateHighScore()
    {
        float highScore = PlayerPrefs.GetFloat("highScore", 0);
        if (_score > highScore)
        {
            highScore = _score;
            PlayerPrefs.SetFloat("highScore", highScore);
        }
        
        highScoreText.text = Mathf.FloorToInt(highScore).ToString("D6");
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
