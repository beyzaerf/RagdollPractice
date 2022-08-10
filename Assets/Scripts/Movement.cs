using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Movement : MonoBehaviour
{
    private bool walk;
    public float speed;
    public FloatingJoystick variableJoystick;
    public Rigidbody hipRigidbody;
    private bool didJump = false;
    [SerializeField] private ConfigurableJoint hipJoint;
    [SerializeField] private Animator anim;
    [SerializeField] private SkinnedMeshRenderer skinnedMesh;

    public void FixedUpdate()
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

        //if (didJump)
        //{
        //    hipRigidbody.AddForce(Vector3.down * 20, ForceMode.Impulse);
        //    Debug.Log("force applied");
        //}

        anim.SetBool("Walk", walk);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trampoline"))
        {
            if (!didJump)
            {
                hipRigidbody.AddForce(Vector3.up * 100, ForceMode.Impulse);
                //DOTween.To(skinnedMesh.GetBlendShapeWeight, 50, 92, 1);
                skinnedMesh.SetBlendShapeWeight(0, 92);
            }
            didJump = true;
            Debug.Log(didJump);
        }
        didJump = false;
    }
}