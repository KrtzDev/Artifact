using UnityEngine;

public class GrappleToHook : MonoBehaviour
{
    public bool hookedOn;

    [SerializeField] private SpringJoint grapplingHookSpringJoint;
    [SerializeField] private Rigidbody character;
    [SerializeField] private float springJointspringines;   

    void FixedUpdate()
    {
        if (hookedOn == true)
        {
            grapplingHookSpringJoint.spring = springJointspringines;
        }
        else
        {
            grapplingHookSpringJoint.spring = 0f;
            character.velocity = new Vector3(0,character.velocity.y,0);
        }
    }
}
