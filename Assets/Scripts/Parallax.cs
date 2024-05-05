using System;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private float _layer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _layer = transform.position.z;
    }

    private void Update()
    {
        float speed = GameManager.Instance.GameSpeed / transform.localScale.x;
        _meshRenderer.material.mainTextureOffset += (Vector2.right * (speed * Time.deltaTime)) / (_layer / 2);
    }
}
