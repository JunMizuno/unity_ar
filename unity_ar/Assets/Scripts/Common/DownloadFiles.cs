using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

// @todo.mizuno もう少し仕組みを考えるため、未完のままにします。
public class DownloadFiles : MonoBehaviour
{
    public void SetSprite(SpriteRenderer spriteRenderer, string uri, string fileName)
    {





        spriteRenderer.sprite = GetSprite(uri, fileName);
    }

    public Sprite GetSprite(string uri, string fileName)
    {
        var saveRoot = Path.Combine(Application.temporaryCachePath, "Cache");
        var savePath = Path.Combine(saveRoot, fileName);

        if (!File.Exists(savePath))
        {

        }






        var texture = new Texture2D(0, 0);
        texture.LoadImage(File.ReadAllBytes(savePath));

        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
    }

    public IEnumerator ExecuteLoadTexture(string uri, string fileName)
    {
        yield return StartCoroutine(LoadAndSaveTexture(uri, fileName));
    }

    // @memo.mizuno fileNameは拡張子も含む。
    public IEnumerator LoadAndSaveTexture(string uri, string fileName)
    {
        var saveRoot = Path.Combine(Application.temporaryCachePath, "Cache");
        var savePath = Path.Combine(saveRoot, fileName);

        // ファイルが見つかった場合。
        if (Directory.Exists(savePath))
        {
            yield break;
        }

        var directoryName = Path.GetDirectoryName(savePath);
        if (!Directory.Exists(directoryName))
        {
            Directory.CreateDirectory(directoryName);
        }

        using (var request = UnityWebRequestTexture.GetTexture(uri))
        {
            request.disposeUploadHandlerOnDispose = false;
            
            request.downloadHandler = new DownloadHandlerFile(savePath);
            yield return request.SendWebRequest();

            if (request.isHttpError || request.isNetworkError)
            {
                // @todo.mizuno ダウンロード失敗。
            }
        }

        yield return true;
    }
}
