using UnityEngine;
using UnityEngine.InputSystem;

public class Flying : MonoBehaviour
{
    [SerializeField] private float _velocity = 1.5f;
    [SerializeField] private float _rotationSpeed = 10f;

    private Rigidbody2D _rb;
    private float _initialGravity;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _initialGravity = _rb.gravityScale;

        _rb.gravityScale = 0f; 
    }

    private void Update()
    {
        bool click = Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame;
        bool touch = Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame;

        if (click || touch)
        {
            if (!GameManager.instance.IsStarted)
            {
                GameManager.instance.StartGame();
                _rb.gravityScale = _initialGravity; 
            }

            Jump();
        }
    }

    private void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, 0f);
        _rb.velocity += Vector2.up * _velocity;
    }

    private void FixedUpdate()
    {
        float targetAngle = Mathf.Clamp(_rb.velocity.y * _rotationSpeed, -45f, 45f);
        transform.rotation = Quaternion.Euler(0, 0, targetAngle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.instance.GameOver();
    }
}
