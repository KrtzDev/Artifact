using System.Collections;
using UnityEngine;

public class AimToggle : MonoBehaviour
{
    public bool aiming;
    public float leftRight;
    
    [SerializeField] private Cinemachine.CinemachineFreeLook playerCam;
    [SerializeField] private Cinemachine.CinemachineFreeLook aimCam;
    
    [SerializeField] private Transform AimingIndicator;
    [SerializeField] private Transform aimingTargetRotation;
    
    private float upDown;
    
    private float minClampLeftRight = -15f;
    private float maxClampLeftRight = 15f;
    private float minClampUpDown = -50f;
    private float maxClampUpDown = 30f;
    
    private CharMovement charMovement;
    private float camSwitchTime;

    // Start is called before the first frame update
    void Start()
    {
        charMovement = GetComponent<CharMovement>();
        AimingIndicator.gameObject.SetActive(false);
        camSwitchTime = 0.35f;
    }

    // Update is called once per frame
    void Update()
    {
        //Inputs
        upDown = Mathf.Clamp(upDown - Input.GetAxis("Mouse Y"), minClampUpDown, maxClampUpDown);
        leftRight = Mathf.Clamp(leftRight - Input.GetAxis("Mouse X"), minClampLeftRight, maxClampLeftRight);
     
        if (Input.GetButton("Aim"))
        {
            AimCamActive();
            MoveAimingTarget();
            AimActions();
        }

        if (Input.GetButtonUp("Aim"))
        {
            PlayerCamActive();
            ResetAimingPoint();
            RevertAimActions();        
        }
    }

    void AimActions()
    {
        AimingIndicator.gameObject.SetActive(true);
        aiming = true;
        charMovement.moveSpeed = 5f;
    }

    void RevertAimActions()
    {
        AimingIndicator.gameObject.SetActive(false);
        StartCoroutine(WaitForCamswitch());
        charMovement.moveSpeed = 10f;
    }

    void MoveAimingTarget()
    {
        aimingTargetRotation.localEulerAngles = new Vector3(upDown, -leftRight, 0);
        AimingIndicator.gameObject.SetActive(true);
    }

    void ResetAimingPoint()
    {
        aimingTargetRotation.localRotation = Quaternion.identity;
        upDown = 0f;
        leftRight = 0f;
        aimingTargetRotation.localEulerAngles = new Vector3(upDown, -leftRight, 0);
    }

    void AimCamActive()
    {
        aimCam.Priority = 1;
        playerCam.Priority = 0;
    }

    void PlayerCamActive()
    {
        playerCam.Priority = 1;
        aimCam.Priority = 0;
    }

    //Stop Turning during Camswitch -> CharMovement
    IEnumerator WaitForCamswitch()
    {
        yield return new WaitForSeconds(camSwitchTime);
        aiming = false;
    }
}
