using UnityEngine;

public class Objects : MonoBehaviour
{

    private float _leftEdge;

    private void Start()
    {
        _leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x; //Gets the camera left edge
    }

    private void Update()
    {
        transform.position += Vector3.left * (GameManager.Instance.GameSpeed * Time.deltaTime) / 5; //Moves obj :)

        if (transform.position.x < _leftEdge - 5f) //Destroy obj after it leaves the screen
        {
            Destroy(gameObject);
        }
    }
}
