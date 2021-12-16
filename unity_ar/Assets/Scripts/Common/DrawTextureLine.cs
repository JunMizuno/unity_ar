using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTextureLine : MonoBehaviour
{
    [SerializeField]
    public Material material;

    [SerializeField]
    public float lineSize = 0.6f;

    [SerializeField]
    public float scrollSpeed = 0.18f;

    private int indexOffset = 0;
    private float uvOffset = 0.0f;

    private List<Vector3> pointList = new List<Vector3>();
    private List<Vector3> verticsList = new List<Vector3>();
    private List<Vector2> uvList = new List<Vector2>();
    private List<int> trisList = new List<int>();

    public void Update()
    {
        // ファーストタップ
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 touchPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        // タップムーブ
        else if (Input.GetMouseButton(0))
        {
            Vector3 touchPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //PenMove(tp, this.penSize);
        }
    }

    public void CreateMesh()
    {

    }

    public void DownCursor(Vector3 touchPoint)
    {

    }

    public void MoveCursor(Vector3 touchPoint)
    {

    }
}
