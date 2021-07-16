using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARObjectsController : MonoBehaviour
{
    [SerializeField]
    public RawImage previewImage;

    public void Start()
    {
        SetImagePosition();
    }

    private void SetImagePosition()
    {
        if (previewImage == null)
        {
            return;
        }

        // イメージ枠のデフォルトサイズは162＊288に設定。
        Vector2 imageSize = previewImage.GetComponent<RectTransform>().sizeDelta;

        // @todo.mizuno 選択されていないカメラの画像を反映させるところから続き。


    }
}
