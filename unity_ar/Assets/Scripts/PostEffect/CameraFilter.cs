using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// @memo.mizuno カメラ2つ表示のテストしているので、ExecuteInEditModeのみにしています。
//[ExecuteInEditMode, ImageEffectAllowedInSceneView]
[ExecuteInEditMode]
public class CameraFilter : MonoBehaviour
{
    [SerializeField]
    private Material filter;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
		if (filter == null)
		{
			Graphics.Blit(source, destination);
		}
		else
		{
			Graphics.Blit(source, destination, filter);
		}
	}
}
