using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform followTarget;
    public float followSpeed = 5;
    private Vector3 offset;

    void Start()
    {
        offset = followTarget.position - transform.position;
    }

    private void Update()
    {
        if (followTarget)
        {
            transform.position = Vector3.Lerp(transform.position, followTarget.position - offset, 0.1f);
        }
    }
}
