﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer))]
public class DrawTextureLine : MonoBehaviour
{
    [SerializeField]
    public Material material;

    [SerializeField]
    public float lineSize = 3.6f;

    [SerializeField]
    public float scrollSpeed = 6.0f;

    private Mesh mesh;

    private int indexOffset = 0;
    private float uvOffset = 0.0f;

    private List<Vector3> pointList = new List<Vector3>();
    private List<Vector3> verticsList = new List<Vector3>();
    private List<Vector2> uvList = new List<Vector2>();
    private List<int> trianglesList = new List<int>();   // インデックス用
    private List<Mesh> createdMesh = new List<Mesh>();

    public void Update()
    {
        // ファーストタップ
        if (Input.GetMouseButtonDown(0))
        {
            pointList.Clear();
            verticsList.Clear();
            uvList.Clear();
            trianglesList.Clear();

            var touchPos = Input.mousePosition;
            touchPos.z = -Camera.main.transform.position.z;
            Vector3 touchPoint = Camera.main.ScreenToWorldPoint(touchPos);
            Debug.Log("<color=yellow>" + "DrawTextureLine touchPoint:" + touchPoint + "</color>");
            DownCursor(touchPoint);

            CreateMesh();
        }
        // タップムーブ
        else if (Input.GetMouseButton(0))
        {
            var touchPos = Input.mousePosition;
            touchPos.z = -Camera.main.transform.position.z;
            Vector3 touchPoint = Camera.main.ScreenToWorldPoint(touchPos);
            MoveCursor(touchPoint);
        }
    }

    public void CreateMesh()
    {
        /*
        Vector2 prevPoint = pointList[pointList.Count - 2];
        Vector2 currentPoint = pointList[pointList.Count - 1];
        Vector2 direction = (currentPoint - prevPoint).normalized;

        // タッチした位置の垂直に上下の座標を求める。(座標点がそれぞれ反面になるイメージ)
        Vector2 plus90 = currentPoint + new Vector2(-direction.y, direction.x) * lineSize;
        Vector2 minus90 = currentPoint + new Vector2(direction.y, -direction.x) * lineSize;

        verticsList.Add(plus90);
        verticsList.Add(minus90);
        */

        Vector2 testVec = pointList[0];
        testVec += new Vector2(15.0f, 0.0f);
        Vector2 plus90_2 = testVec + new Vector2(0.0f, 1.0f) * lineSize;
        Vector2 minus90_2 = testVec + new Vector2(0.0f, -1.0f) * lineSize;
        verticsList.Add(plus90_2);
        verticsList.Add(minus90_2);

        /*
        uvList.Add(new Vector2(uvOffset, 0));
        uvList.Add(new Vector2(uvOffset, 1));
        uvOffset += (currentPoint - prevPoint).magnitude / scrollSpeed;
        */
        uvList.Add(new Vector2(1.0f, 0.0f));
        uvList.Add(new Vector2(1.0f, 1.0f));

        /*
        trianglesList.Add(indexOffset);
        trianglesList.Add(indexOffset + 1);
        trianglesList.Add(indexOffset + 2);
        trianglesList.Add(indexOffset + 1);
        trianglesList.Add(indexOffset + 3);
        trianglesList.Add(indexOffset + 2);

        indexOffset += 2;
        */
        trianglesList.Add(2);
        trianglesList.Add(3);
        trianglesList.Add(1);
        trianglesList.Add(2);
        trianglesList.Add(1);
        trianglesList.Add(0);

        mesh.vertices = verticsList.ToArray();
        mesh.uv = uvList.ToArray();
        mesh.triangles = trianglesList.ToArray();

        GetComponent<MeshFilter>().sharedMesh = mesh;
        GetComponent<MeshRenderer>().material = material;
    }

    public void DownCursor(Vector3 touchPoint)
    {
        pointList.Add(touchPoint);

        /*
        verticsList.Add(touchPoint);
        verticsList.Add(touchPoint);
        */
        Vector2 currentPoint = touchPoint;
        Vector2 plus90 = currentPoint + new Vector2(0.0f, 1.0f) * lineSize;
        Vector2 minus90 = currentPoint + new Vector2(0.0f, -1.0f) * lineSize;

        verticsList.Add(plus90);
        verticsList.Add(minus90);

        uvList.Add(new Vector2(0.0f, 0.0f));
        uvList.Add(new Vector2(0.0f, 1.0f));

        indexOffset = 0;

        mesh = new Mesh();
    }

    public void MoveCursor(Vector3 touchPoint)
    {
        /*
        pointList.Add(touchPoint);
        CreateMesh();
        */
    }
}
