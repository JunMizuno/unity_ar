using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class VisualizeTangentSpace : MonoBehaviour
{

}

#if UNITY_EDITOR
[CustomEditor(typeof(VisualizeTangentSpace))]
public class VisualizeTangentSpaceEditor : Editor
{
    private int targetIndex = 0;
    private VisualizeTangentSpace currentTarget;
    private Mesh mesh;
    private Vector3 modelPos;
    private Vector3 normal;
    private Vector3 tangent;
    private Vector3 binormal;

    public override void OnInspectorGUI()
    {
        targetIndex = EditorGUILayout.IntSlider(targetIndex, 0, mesh.vertexCount - 1);

        if (GUI.changed)
        {
            var localNormal = mesh.normals[targetIndex];

            // 法線がカメラ方面を向いている時のみ情報を更新する。
            var viewDir = SceneView.lastActiveSceneView.camera.transform.position - (currentTarget.transform.position + modelPos);
            if (Vector3.Dot(viewDir, localNormal) >= 0)
            {
                modelPos = mesh.vertices[targetIndex];
                this.normal = localNormal;
                var localTangent = mesh.tangents[targetIndex];
                this.tangent = localTangent;
                binormal = Vector3.Cross(this.normal, this.tangent) * localTangent.w;
            }
        }
    }

    private void OnSceneGUI()
    {
        if (Event.current.type == EventType.Repaint)
        {
            var transform = currentTarget.transform;
            Handles.color = Color.red;
            Handles.ArrowHandleCap(0, transform.position + modelPos, transform.rotation * Quaternion.LookRotation(tangent), 1.0f, EventType.Repaint);
            Handles.color = Color.green;
            Handles.ArrowHandleCap(0, transform.position + modelPos, transform.rotation * Quaternion.LookRotation(binormal), 1.0f, EventType.Repaint);
            Handles.color = Color.blue;
            Handles.ArrowHandleCap(0, transform.position + modelPos, transform.rotation * Quaternion.LookRotation(normal), 1.0f, EventType.Repaint);
        }
    }

    private void OnEnable()
    {
        currentTarget = base.target as VisualizeTangentSpace;
        mesh = currentTarget.GetComponent<MeshFilter>().sharedMesh;
        modelPos = mesh.vertices[targetIndex];
        normal = mesh.normals[targetIndex];
        tangent = mesh.tangents[targetIndex];
    }
}
#endif