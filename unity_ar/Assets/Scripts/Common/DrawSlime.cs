using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways] // 実行していない間もプレビューするために。
public class DrawSlime : MonoBehaviour
{
    [SerializeField]
    private Material material;　// Unlit/DrawSlimeを指定すること。

    private const int MaxSphereCount = 256; // 球の最大個数(シェーダー側と合わせる、グローバルで作ってもいいかもしれない。)
    private readonly Vector4[] spheres = new Vector4[MaxSphereCount];
    private SphereCollider[] colliders;

    public void Start()
    {
        colliders = GetComponentsInChildren<SphereCollider>();

        // シェーダー側の_SphereCountを更新。
        material.SetInt("_SphereCount", colliders.Length);
    }

    public void Update()
    {
        // 子のSphereColliderの分だけspheresに中心座標と半径を入れていく。
        for (var i = 0; i < colliders.Length; i++)
        {
            var col = colliders[i];
            var t = col.transform;
            var center = t.position;
            var radius = t.lossyScale.x * col.radius;

            // 中心座標と半径を格納。
            spheres[i] = new Vector4(center.x, center.y, center.z, radius);
        }

        // シェーダー側の_Spheresを更新。
        material.SetVectorArray("_Spheres", spheres);
    }
}
