using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRotate : MonoBehaviour
{
    [SerializeField]
    public bool isRotate = true;

    [SerializeField]
    public float rotateSpeed = 30.0f;

    public void Update()
    {
        if (!isRotate)
        {
            return;
        }

        var angles = this.transform.rotation.eulerAngles;
        angles.y += rotateSpeed * Time.deltaTime;
        this.transform.rotation = Quaternion.Euler(angles);
    }
}
