using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithCursor : MonoBehaviour
{
    public void Update()
    {
        this.gameObject.transform.position = TouchManager.GetWorldPositionByMousePosition(Camera.main);
    }
}
