using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    private void Update()
    {
        var pos = Camera.main.transform.position;
        pos.y = this.gameObject.transform.position.y;
        this.gameObject.transform.LookAt(pos);
    }
}
