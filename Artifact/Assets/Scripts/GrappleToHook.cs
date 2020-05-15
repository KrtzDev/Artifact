using UnityEngine;

public class GrappleToHook : MonoBehaviour
{
    public bool hookedOn;

    [SerializeField] private SpringJoint grapplingHookSpringJoint;
    [SerializeField] private Rigidbody character;
    [SerializeField] private float springJointspringines;   

    void FixedUpdate()
    {
        if (hookedOn)
        {
            grapplingHookSpringJoint.spring = springJointspringines;
        }
        else
        {
            grapplingHookSpringJoint.spring = 0f;
            character.velocity = new Vector3(0,character.velocity.y + Physics.gravity.y * Time.fixedDeltaTime, 0);
        }   
    }
}
