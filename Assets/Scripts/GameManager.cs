using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Object = System.Object;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

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

    private void NewGame()
    {
        Objects[] objects = FindObjectsOfType<Objects>();

        foreach (var obj in objects)
        {
            Destroy(obj.gameObject);
        }
        
        GameSpeed = initialGameSpeed;
        enabled = true;
        
        _player.gameObject.SetActive(true);
        _spawner.gameObject.SetActive(true);
    }

    public void GameOver()
    {
        GameSpeed = 0f;
        enabled = false;
        
        // _player.gameObject.SetActive(false);
        _player.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        _spawner.gameObject.SetActive(false);
    }
    
    private void Update()
    {
        GameSpeed += gameSpeedAcceleration * Time.deltaTime;
    }
}
