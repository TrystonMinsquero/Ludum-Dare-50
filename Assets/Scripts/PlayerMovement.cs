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
            if(_rb.velocity.magnitude > 0.3f)
                lookDirection = _controller.MoveInput;

            // look
            if (_controller.LookInput.magnitude > .1f)
                lookDirection = _controller.LookInput;
            
            
            //Vector2 lookDir = (_controller.LookPosition - transform.position).normalized;
            //transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(lookDir.y, lookDir.x) + 90);
            //_rb.velocity = Vector2.zero;
        }
    }

    public Direction GetDirection()
    {
        float mid = Mathf.Sin(Mathf.Deg2Rad * 30);
        Vector2[] directions = new Vector2[8];
        directions[(int) Direction.UP] = Vector2.up;
        directions[(int) Direction.UP_RIGHT] = new Vector2(mid, mid);
        directions[(int) Direction.RIGHT] = Vector2.up;
        directions[(int) Direction.DOWN_RIGHT] = new Vector2(mid, -mid);
        directions[(int) Direction.DOWN] = Vector2.up;
        directions[(int) Direction.DOWN_LEFT] = new Vector2(-mid, -mid);
        directions[(int) Direction.LEFT] = Vector2.up;
        directions[(int) Direction.UP_LEFT] = new Vector2(-mid, mid);

        float closestVal = Mathf.Infinity;
        float closestIndex = -1;
        Vector2 v1 = lookDirection.normalized;
        for (int i = 0; i < directions.Length; i++)
        {
            Vector2 v2 = directions[i].normalized;
            float val = v1.magnitude * v2.magnitude * Mathf.Cos(Vector2.Dot(v1, v2));
            val = Mathf.Abs(1 - val);
            if (val < closestVal)
            {
                closestIndex = i;
                closestVal = val;
            }
        }

        return (Direction) (closestIndex);
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
