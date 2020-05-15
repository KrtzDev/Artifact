using UnityEngine;

public class CharMovement : MonoBehaviour
{
    public float moveSpeed = 10f;

    [SerializeField] private float airBornMoveSpeed = 3f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float rotateSpeed = 3f;
    [SerializeField] private LayerMask layerMask;

    private Rigidbody character;
    private Camera mainCam;

    private float vertical;
    private float horizontal;
    private Vector3 movement;
    private Vector3 turnVector;
    private float leftRigh_RightJoyStick;

    private AimToggle aimToggle;

    void Start()
    {
        character = GetComponent<Rigidbody>();
        aimToggle = GetComponent<AimToggle>();
        mainCam = Camera.main;
    }

    void Update()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        leftRigh_RightJoyStick = Input.GetAxis("Mouse X");        

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }       
    }

    void FixedUpdate()
    {
        //Cameramovement
        Vector3 camF = mainCam.transform.forward;
        Vector3 camR = mainCam.transform.right;

        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;

        movement = new Vector3(vertical, 0, horizontal);

        if (IsGrounded())
        {
            character.MovePosition(transform.position + (movement.x * camF + movement.z * camR) * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            character.MovePosition(transform.position + (movement.x * camF + movement.z * camR) * airBornMoveSpeed * Time.fixedDeltaTime);
        }

        if (aimToggle.aiming == false)
        {          
                if (movement != Vector3.zero)
                {
                    character.MoveRotation(Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement.z * camR + movement.x * camF), 0.35f));
                }           
        }

        //Turn while Aiming
        if (aimToggle.aiming == true)
        {
            turnVector = new Vector3(0, leftRigh_RightJoyStick, 0);
            turnVector = turnVector.normalized * rotateSpeed;
            if (aimToggle.leftRight > 14f)
            {
                //Turn left
                Quaternion deltaRotation = Quaternion.Euler(turnVector);
                character.MoveRotation(Quaternion.Slerp(character.rotation, character.rotation * deltaRotation, 0.35f));
            }
            if (aimToggle.leftRight < -14f)
            {
                //Turn right
                Quaternion deltaRotation = Quaternion.Euler(turnVector);
                character.MoveRotation(Quaternion.Slerp(character.rotation, character.rotation * deltaRotation, 0.35f));
            }
        }           
    }

    public bool IsGrounded()
    {
        RaycastHit groundHit;
        Physics.SphereCast(transform.position, .5f, Vector3.down, out groundHit, .4f, layerMask);
        return groundHit.collider != null;
    }

    void Jump()
    {
        character.velocity = new Vector3(0, jumpForce, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + Vector3.down * .4f, .5f);
    }
}
