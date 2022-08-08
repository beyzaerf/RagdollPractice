using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ConfigurableJoint hipJoint;
    [SerializeField] private Rigidbody mainRb;
    [SerializeField] private Rigidbody hip;
    [SerializeField] private Animator targetAnimator;

    private float speed = 20;
    private bool isGrounded = true;
    private bool walk = false;
    private bool forceApplied = false;


    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;

            this.hipJoint.targetRotation = Quaternion.Euler(0f, targetAngle, 0f);

            this.hip.AddForce(direction * this.speed);

            this.walk = true;
        }  else 
        {
            this.walk = false;
        }

        this.targetAnimator.SetBool("Walk", this.walk);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            mainRb.AddForce(Vector3.up * 100, ForceMode.Impulse);
            isGrounded = false;
        }

        if (!isGrounded && !forceApplied)
        {
            mainRb.AddForce(Vector3.down * 50 , ForceMode.Impulse);
            forceApplied = true;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        isGrounded = true;
    }
}
