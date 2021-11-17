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
}
