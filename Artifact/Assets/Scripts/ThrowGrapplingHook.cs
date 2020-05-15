using System.Collections;
using UnityEngine;

public class ThrowGrapplingHook : MonoBehaviour
{
    public float raycastLenght;

    [SerializeField] private LayerMask layerMask;

    [SerializeField] private RaycastHit hit;

    [SerializeField] private float throwSpeed;

    [SerializeField] private Transform weaponGrapplingHook;
    [SerializeField] private Transform weaponPoint;
    [SerializeField] private Transform aimingTarget;

    private Transform mainCamera;

    private GrappleToHook grappleToHook;
    private AimToggle aimToggle;

    private bool throwingInput;

    void Start()
    {
        grappleToHook = FindObjectOfType<GrappleToHook>();
        aimToggle = GetComponent<AimToggle>();
        mainCamera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Throw") > 0.1f || Input.GetButton("Fire1"))
        {
            throwingInput = true;
        }
        else
        {
            throwingInput = false;
        }

        if (throwingInput && aimToggle.aiming == true && grappleToHook.hookedOn == false)
        {
            ThrowGrappleHookRaycast();
        }
        else if (!throwingInput)
        {
            grappleToHook.hookedOn = false;
            weaponGrapplingHook.SetParent(weaponPoint);
            weaponGrapplingHook.transform.position = Vector3.Lerp(weaponGrapplingHook.transform.position,weaponPoint.position, 0.15f);
            weaponGrapplingHook.rotation = weaponPoint.rotation;
        }

        Debug.DrawRay(weaponPoint.position,aimingTarget.position - weaponPoint.position);
    }

    void ThrowGrappleHookRaycast()
    {
        Physics.Raycast(weaponPoint.position,aimingTarget.position - weaponPoint.position,out hit,raycastLenght,layerMask);
        if (hit.collider != null)
        {
            grappleToHook.hookedOn = true;
            weaponGrapplingHook.SetParent(null);
            weaponGrapplingHook.transform.rotation = mainCamera.rotation;
            StopCoroutine(ThrowOut());
            StartCoroutine(ThrowOut());
        }
    }

    IEnumerator ThrowOut()
    {
        float startTime = Time.time;
        while (Time.time < startTime + throwSpeed)
        {
            weaponGrapplingHook.transform.position = Vector3.Lerp(weaponGrapplingHook.transform.position, hit.collider.gameObject.transform.position, (Time.time - startTime)/throwSpeed);
            yield return null;
        }
        weaponGrapplingHook.transform.position = hit.collider.gameObject.transform.position;
    }
}
