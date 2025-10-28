using UnityEngine;

public class MovePipe : MonoBehaviour
{
    [SerializeField] private float _speed = 0.65f;

    private void Update()
    {
        if (!GameManager.instance.IsStarted) return; 

        transform.position += Vector3.left * _speed * Time.deltaTime;
    }
}

