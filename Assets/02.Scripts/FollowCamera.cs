using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform targetTransform;
    private Transform cameraTransform;
    [Range(0.0f, 20.0f)]
    public float distance = 10f;
    [Range(0.0f, 10.0f)]
    public float height = 2.0f;

    public float moveDamping = 15f;
    public float rotateDamping = 10f;

    public float targetOffset = 3f;
    private void Start()
    {
        cameraTransform = GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        Vector3 pos = targetTransform.position + (-targetTransform.forward * distance) + (targetTransform.up * height);
        cameraTransform.position = Vector3.Slerp(cameraTransform.position, pos, moveDamping * Time.deltaTime);
        cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, targetTransform.rotation, rotateDamping * Time.deltaTime);
        cameraTransform.LookAt(targetTransform.position + (targetTransform.up * targetOffset));
    }
}
