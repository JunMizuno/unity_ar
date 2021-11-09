using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloat : MonoBehaviour
{
    [SerializeField]
    public bool isFloat = true;

    [SerializeField]
    public float floatDistance = 5.0f;

    private void Update()
    {
        if (!isFloat)
        {
            return;
        }

        var pos = this.transform.localPosition;
        pos.y = floatDistance * Mathf.Sin(Time.time);
        this.transform.localPosition = pos;
    }
}
