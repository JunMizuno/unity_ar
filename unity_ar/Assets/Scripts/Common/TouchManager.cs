﻿using System.Collections;
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

    public static GameObject CheckCollidedObject()
    {
        GameObject collidedGameObject = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);



        return collidedGameObject;
    }
}
