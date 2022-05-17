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

	[SerializeField]
	public bool isEnableBlur = default;

	[SerializeField, Range(0.0f, 30.0f)]
	public float blurSize = 2.0f;

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (filter == null)
		{
			Graphics.Blit(source, destination);
		}
		else
		{
			if (filter.name == "FastBlur" && isEnableBlur)
            {
				SetFastBlur(source, destination);
            }
			else
            {
				Graphics.Blit(source, destination, filter);
			}
		}
	}

	private void SetFastBlur(RenderTexture source, RenderTexture destination)
	{
		filter.SetFloat("_BlurSize", blurSize);

		var temp1 = RenderTexture.GetTemporary(Screen.width / 4, Screen.height / 4, 0, source.format);
		var temp2 = RenderTexture.GetTemporary(Screen.width / 8, Screen.height / 8, 0, source.format);

		Graphics.Blit(source, temp1, filter);
		Graphics.Blit(temp1, temp2, filter);
		Graphics.Blit(temp2, destination, filter);

		RenderTexture.ReleaseTemporary(temp1);
		RenderTexture.ReleaseTemporary(temp2);
	}
}
