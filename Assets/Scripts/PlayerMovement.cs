using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Direction
{
    UP,
    UP_RIGHT,
    RIGHT,
    DOWN_RIGHT,
    DOWN,
    DOWN_LEFT,
    LEFT,
    UP_LEFT
}

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;

    [Header("Dash Settings")] 
    public float dashDelay = .2f;
    public float dashSpeed;
    public float targetDashDistance;
    public float dashTimeBuffer = 1f; // Estimated value for timeout

    private PlayerController _controller;
    private Rigidbody2D _rb;
    private bool isDashing;
    private Vector2 lookDirection;
    private Direction _direction;

    private float _canDashTime;
    private void Start()
    {
        _controller = GetComponent<PlayerController>();
        _rb = GetComponent<Rigidbody2D>();
        lookDirection = Vector2.down;
        _canDashTime = 0;
    }

    private IEnumerator Dash()
    {
        // reset values
        _rb.drag = 0;
        isDashing = true;
        Vector3 startPos = transform.position;
        _rb.velocity = lookDirection.normalized * dashSpeed; //initial velocity added
        float maxDashTime = dashTimeBuffer; //Estimated
        float timeStarted = Time.time;
        while ((transform.position - startPos).magnitude < targetDashDistance && Time.time - timeStarted < maxDashTime)
            yield return null;
        EndDash();
        // Use this debugging for testing
        // Debug.Log("Actual Time: " + (Time.time - timeStarted));
        // Debug.Log("Estimated Time: " + maxDashTime);
    }

    private void EndDash()
    {
        _rb.velocity = Vector2.zero;
        isDashing = false;
        _canDashTime = Time.time + dashDelay;
    }
    
    private void FixedUpdate()
    {
        if(isDashing)
            return;

        if (!isDashing)
        {
            // move
            _rb.velocity = _controller.MoveInput * moveSpeed;

            // look
            // if (_controller.LookInput.magnitude > .1f)
            //     lookDirection = _controller.LookInput;
            
            
            if(_rb.velocity.magnitude > 0.3f)
                lookDirection = _controller.MoveInput;
            
            
            //Vector2 lookDir = (_controller.LookPosition - transform.position).normalized;
            //transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(lookDir.y, lookDir.x) + 90);
            //_rb.velocity = Vector2.zero;
        }
    }
    
    public Vector2 GetDirectionVector() {
        var v = lookDirection.normalized;
        var x = (Mathf.Abs(v.x) > 0.5f) ? Mathf.Sign(v.x) : 0;
        var y = (Mathf.Abs(v.y) > 0.5f) ? Mathf.Sign(v.y) : 0;
        return new Vector2(x, y);
    }

    public Direction GetDirection()
    {

        Vector2 v = GetDirectionVector();
        // Debug.Log(v);
        switch (v.x)
        {
            case 0:
                if (v.y == 1)
                    return Direction.UP;
                if (v.y == -1)
                    return Direction.DOWN;
                if (v.y == 0)
                    return _direction;
                break;
            case 1:
                if (v.y == 1)
                    return Direction.UP_RIGHT;
                if (v.y == -1)
                    return Direction.DOWN_RIGHT;
                return Direction.RIGHT;
                break;
            case -1:
                if (v.y == 1)
                    return Direction.UP_LEFT;
                if (v.y == -1)
                    return Direction.DOWN_LEFT;
                return Direction.LEFT;
                break;
                
                    
        }
        Debug.LogWarning($"{v} could not be converted to a direction");
        return Direction.DOWN;
        // Debug.Log($"dir: {lookDirection} goes to {(Direction) (closestIndex)}" );

        // return (Direction) (closestIndex);
    }

    private void Update()
    {
        if (!isDashing)
        {
            // dash
            if(_controller.DashInput && _canDashTime <= Time.time)
                StartCoroutine(Dash());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            StopCoroutine(Dash());
            EndDash();
        }
    }

    public bool IsDashing()
    {
        return isDashing;
    }
}
