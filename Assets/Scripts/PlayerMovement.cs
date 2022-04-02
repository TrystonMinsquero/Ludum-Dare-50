using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    
    [Header("Dash Settings")]
    [Tooltip("The maximum time the player can be dashing (in case they get stuck)")]
    public float maxDashTime;
    public float dashSpeed;
    public float targetDashDistance;
    

    private PlayerController _controller;
    private Rigidbody2D _rb;
    private bool isDashing;
    private Vector2 lookDirection;

    private void Start()
    {
        _controller = GetComponent<PlayerController>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private IEnumerator Dash()
    {
        _rb.drag = 0;
        isDashing = true;
        Vector3 startPos = transform.position;
        //Debug.Log(lookDirection);
        _rb.velocity = lookDirection.normalized * dashSpeed; //initial velocity added
        float maxDashTime = (targetDashDistance / _rb.velocity.magnitude) + .3f; //Estimated
        float timeStarted = Time.time;
        while ((transform.position - startPos).magnitude < targetDashDistance && Time.time - timeStarted < maxDashTime)
            yield return null;
        _rb.velocity = Vector2.zero;
        isDashing = false;
        //Debug.Log("Actual Time: " + (Time.time - timeStarted));
        //Debug.Log("Estimated Time: " + maxDashTime);
    }
    private void FixedUpdate()
    {
        if (!isDashing)
        {
            lookDirection = _controller.MoveInput;
            _rb.velocity = _controller.MoveInput * moveSpeed;


            //Vector2 lookDir = (_controller.LookPosition - transform.position).normalized;
            //transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(lookDir.y, lookDir.x) + 90);
            //_rb.velocity = Vector2.zero;
        }
    }
    
    
}
