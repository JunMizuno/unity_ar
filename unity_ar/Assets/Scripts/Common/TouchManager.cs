using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TouchManager
{
    public enum TOUCH_PATTERN
    {
         TouchDowned,
         TouchHolding,
         TouchReleased,
    }

    public static bool IsTouchDowned()
    {
        return Input.GetMouseButtonDown(0);
    }

    public static bool IsTouchReleased()
    {
        return Input.GetMouseButtonUp(0);
    }

    public static bool IsTouchHolding()
    {
        return Input.GetMouseButton(0);
    }

    public static bool IsTouchUpKey()
    {
        return Input.GetKeyDown(KeyCode.UpArrow);
    }

    public static bool IsTouchDownKey()
    {
        return Input.GetKeyDown(KeyCode.DownArrow);
    }

    public static bool IsTouchLeftKey()
    {
        return Input.GetKeyDown(KeyCode.LeftArrow);
    }

    public static bool IsTouchRightKey()
    {
        return Input.GetKeyDown(KeyCode.RightArrow);
    }

    public static GameObject CheckCollidedObject(Camera targetCamera)
    {
        if (targetCamera == null)
        {
            return null;
        }

        GameObject collidedGameObject = null;

        Ray ray = targetCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // @todo.mizuno 何か処理を渡してもいいかと思う。
            collidedGameObject = hit.collider.gameObject;
        }

        return collidedGameObject;
    }

    public static Vector3 GetWorldPositionByMousePosition(Camera targetCamera)
    {
        var touchPos = Input.mousePosition;
        touchPos.z = -targetCamera.transform.position.z;
        Vector3 worldPoint = targetCamera.ScreenToWorldPoint(touchPos);
        return worldPoint;
    }
}
