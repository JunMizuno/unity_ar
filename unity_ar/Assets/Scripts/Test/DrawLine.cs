using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DrawLine : MonoBehaviour
{
    private void Start()
    {
        // @memo.mizuno ↓は動的にアタッチする場合。
        //var lineRenderer = gameObject.AddComponent<LineRenderer>();
        var lineRenderer = this.GetComponent<LineRenderer>();

        var positions = new Vector3[]
        {
             new Vector3(-4, 0, 0),
             new Vector3(0, 4, 0),
             new Vector3(4, -4, 0),
        };

        lineRenderer.positionCount = positions.Length;
        lineRenderer.loop = true;            // 点を全てつなぎ合わせる
        lineRenderer.numCapVertices = 5;     // 線の両端に影響
        lineRenderer.numCornerVertices = 5;  // 中間の角に影響
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.black;

        lineRenderer.SetPositions(positions);
    }
}
