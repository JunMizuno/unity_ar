using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLOD : MonoBehaviour
{
    public void Start()
    {
        var meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            // @memo.mizuno シェーダー側のLOD値は降順で設定しないといけない。
            meshRenderer.material.shader.maximumLOD = 60;
        }
    }
}
