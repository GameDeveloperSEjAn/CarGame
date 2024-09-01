using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{
    public float moveSmoothness = 8f;
    public float rotationSmoothness = 8f;

    public Vector3 moveOffset = new Vector3(0f, 2.5f, -5f);
    public Vector3 rotOffset = new Vector3 (0f, 1f, 0f);
    private Vector3 originalmoveOffset;
    private Vector3 originalrotOffset;
    private bool isOffsetChanged = false;

    public Transform carTarget;
    private void Start()
    {
        originalmoveOffset = moveOffset;
        originalrotOffset = rotOffset;
    }
    void FixedUpdate()
    {
        MovementHandle();
        RotationHandle();

        if (Input.GetKeyUp(KeyCode.V))
        {
            if (isOffsetChanged)
            {
                // Reset to original offsets
                moveOffset = originalmoveOffset;
                rotOffset = originalrotOffset;
            }
            else
            {
                // Change to new offsets
                moveOffset = new Vector3(0, 2.5f, -7);
                rotOffset = new Vector3(0, 1f, 0f);
            }

            // Toggle the flag
            isOffsetChanged = !isOffsetChanged;
        }
    }
    void MovementHandle()
    {
        Vector3 targetPos = new Vector3();
        targetPos = carTarget.TransformPoint(moveOffset);
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSmoothness * Time.deltaTime);
    }
    void RotationHandle()
    {
        var direction = carTarget.position - transform.position;
        var rotation = new Quaternion();

        rotation = Quaternion.LookRotation(direction + rotOffset, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation , rotation , rotationSmoothness * Time.deltaTime);
    }
}