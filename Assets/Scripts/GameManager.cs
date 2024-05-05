using System;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float initialGameSpeed = 30f;
    public float gameSpeedAcceleration = 2;
    public float GameSpeed { get; private set; }
    
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
        NewGame();
    }

    private void NewGame()
    {
        GameSpeed = initialGameSpeed;
    }

    private void Update()
    {
        GameSpeed += gameSpeedAcceleration * Time.deltaTime;
    }
}
