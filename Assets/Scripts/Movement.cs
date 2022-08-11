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
        anim.SetBool("Walk", walk);
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
                    hipRigidbody.AddForce(Vector3.down * 20, ForceMode.Impulse);
                });
            });
        }
    }
}