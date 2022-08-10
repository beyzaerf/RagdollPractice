using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    public float speed;
    public FloatingJoystick variableJoystick;
    public Rigidbody rb;
    [SerializeField] private ConfigurableJoint hipJoint;
    [SerializeField] private Animator anim;
    private bool walk;

    public void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
            hipJoint.targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            rb.AddForce(speed * Time.fixedDeltaTime * direction, ForceMode.VelocityChange);
            walk = true;
        }
        else
        {
            walk = false;
        }

        anim.SetBool("Walk", walk);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Trampoline"))
        {

        }
    }
}