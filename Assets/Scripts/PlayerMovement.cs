using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    
    [Header("Dash Settings")]
    public float dashSpeed;
    public float targetDashDistance;
    public float dashTimeBuffer = .3f; // Estimated value for timeout


    private PlayerController _controller;
    private Rigidbody2D _rb;
    private bool isDashing;
    private Vector2 lookDirection;

    private void Start()
    {
        _controller = GetComponent<PlayerController>();
        _rb = GetComponent<Rigidbody2D>();
        lookDirection = Vector2.down;
    }

    private IEnumerator Dash()
    {
        // reset values
        _rb.drag = 0;
        isDashing = true;
        Vector3 startPos = transform.position;
        Debug.Log($"lookDirection: {lookDirection}");
        Debug.Log($"lookDirection normalized: {lookDirection.normalized}");
        _rb.velocity = lookDirection.normalized * dashSpeed; //initial velocity added
        float maxDashTime = (_rb.velocity.magnitude / targetDashDistance ) + dashTimeBuffer; //Estimated
        float timeStarted = Time.time;
        while ((transform.position - startPos).magnitude < targetDashDistance && Time.time - timeStarted < maxDashTime)
            yield return null;
        _rb.velocity = Vector2.zero;
        isDashing = false;
        
        // Use this debugging for testing
        Debug.Log("Actual Time: " + (Time.time - timeStarted));
        Debug.Log("Estimated Time: " + maxDashTime);
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
            
            // dash
            if(_controller.DashInput)
                StartCoroutine(Dash());
            
            //Vector2 lookDir = (_controller.LookPosition - transform.position).normalized;
            //transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(lookDir.y, lookDir.x) + 90);
            //_rb.velocity = Vector2.zero;
        }
    }
    
    
}
