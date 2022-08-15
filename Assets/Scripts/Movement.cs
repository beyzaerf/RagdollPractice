using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Movement : MonoBehaviour
{
    private bool walk;
    //private bool isGrounded = true;
    public float speed;
    private int gravityScale = 5;
    public FloatingJoystick variableJoystick;
    public Rigidbody hipRigidbody;
    [SerializeField] private ConfigurableJoint hipJoint;
    [SerializeField] private Animator anim;
    [SerializeField] private SkinnedMeshRenderer skinnedMesh;

    public void FixedUpdate()
    {
        Move();

        //if (!isGrounded)
        //{
        //if (hipRigidbody.velocity.y == 0)
        //{
        //    hipRigidbody.AddForce(gravityScale * hipRigidbody.mass * Physics.gravity);
        //    Debug.Log("works");
        //}
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trampoline"))
        {
            float weight = skinnedMesh.GetBlendShapeWeight(0);
            hipRigidbody.AddForce(Vector3.up * 160, ForceMode.Impulse);
            DOTween.To(() => weight, x => skinnedMesh.SetBlendShapeWeight(0, 92), 0f, 0.1f).OnComplete(() =>
            {
                DOTween.To(() => weight, x => skinnedMesh.SetBlendShapeWeight(0, 0), 0f, 0.1f).OnComplete(() =>
                {
                    skinnedMesh.SetBlendShapeWeight(0, 50);
                });
            });
        }
    }

    private void Move()
    {
        Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
            hipJoint.targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            hipRigidbody.AddForce(speed * Time.fixedDeltaTime * direction, ForceMode.VelocityChange);
            walk = true;
        }
        else
        {
            walk = false;
        }
        anim.SetBool("Walk", walk);

        hipRigidbody.AddForce(gravityScale * hipRigidbody.mass * Physics.gravity);
    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    isGrounded = true;
    //}
    //private void OnCollisionExit(Collision collision)
    //{
    //    isGrounded = false;
    //}
}