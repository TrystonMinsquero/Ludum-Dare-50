using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;

    private PlayerController _controller;
    private Rigidbody2D _rb;
    private bool isDashing;

    private void Start()
    {
        _controller = GetComponent<PlayerController>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Dash()
    {
        
    }
    
    

    private void FixedUpdate()
    {
        if (!isDashing)
        {
            Vector2 direction = _controller.MoveInput;
            _rb.position = _rb.position + moveSpeed * direction;
            //_rb.velocity = Vector2.zero;
        }
    }
    
    
}
