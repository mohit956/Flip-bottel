using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;

    [SerializeField] private float jumpForce;
    [SerializeField] private float spinSpeed;
    [SerializeField] private float groundDst;

    private Rigidbody rb;
    private bool isGrounded;
    private bool previousGroundedState;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        isGrounded = CheckGrounded();
        CheckLanding();

        if (!isGrounded) return;

        //Just testing with keyboard inputs, This will need to change to Android inputs
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void CheckLanding()
    {
        if (previousGroundedState != isGrounded)
        {
            previousGroundedState = isGrounded;

            if (isGrounded)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0f);
            }
        }
    }

    private bool CheckGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundDst, groundMask);
    }

    private void Jump()
    {
        Vector3 dir = new Vector3(.5f, 1f, 0f);
        rb.AddForce(dir * jumpForce, ForceMode.Impulse);
        rb.AddTorque(transform.forward * spinSpeed * -1f, ForceMode.Impulse);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(groundCheck.position, groundDst);
    }
}
