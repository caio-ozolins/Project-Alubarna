using System;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class UIControler : MonoBehaviour
{
    private Player _player;
    private Text _distanceText;

    private void Awake()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _distanceText = GameObject.Find("Distance Text").GetComponent<Text>();
    }

    private void Update()
    {
        int distance = Mathf.FloorToInt(_player.distance);
        _distanceText.text = distance + " m";
    }
}
