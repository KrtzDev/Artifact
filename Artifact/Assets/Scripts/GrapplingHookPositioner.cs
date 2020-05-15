using UnityEngine;

public class GrapplingHookPositioner : MonoBehaviour
{
    [SerializeField] private Transform WeaponPoint;

    private void Awake()
    {
        ResetWeapon();
    }

    public void ResetWeapon()
    {
        transform.gameObject.SetActive(true);
        transform.SetParent(WeaponPoint);

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }
}
