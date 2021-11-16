using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloat : MonoBehaviour
{
    [SerializeField]
    public bool isFloat = true;

    [SerializeField]
    public MOVE_PATTERN movePattern = MOVE_PATTERN.MoveY;

    [SerializeField]
    public float floatDistance = 5.0f;

    public enum MOVE_PATTERN
    {
         MoveY,
         RotateY,
    }

    private void Update()
    {
        if (!isFloat)
        {
            return;
        }

        switch(movePattern)
        {
            case MOVE_PATTERN.MoveY:
                MovePosY();
                break;

            case MOVE_PATTERN.RotateY:
                RotatePosY();
                break;

            default:
                break;
        }
    }

    private void MovePosY()
    {
        var pos = this.transform.localPosition;
        pos.y = floatDistance * Mathf.Sin(Time.time);
        this.transform.localPosition = pos;
    }

    private void RotatePosY()
    {
        var angles = this.transform.localRotation.eulerAngles;
        angles.y = floatDistance * Mathf.Sin(Time.time);
        this.transform.localRotation = Quaternion.Euler(angles);
    }
}
