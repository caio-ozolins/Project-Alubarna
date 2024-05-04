using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float depth = 1;

    private float _initialPositionX;
    
    private Player _player;

    private void Awake()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void Start()
    {
        _initialPositionX = transform.position.x;
    }

    private void FixedUpdate()
    {
        float realVelocity = _player.velocity.x / depth;
        Vector2 pos = transform.position;

        pos.x -= realVelocity * Time.fixedDeltaTime;

        
        if ((_initialPositionX - pos.x) >= 60)
        {
            pos.x = _initialPositionX;
        }
        
        // if (pos.x <= -60)
        // {
        //     pos.x = 60;
        // }
           
        transform.position = pos;
    }
}
