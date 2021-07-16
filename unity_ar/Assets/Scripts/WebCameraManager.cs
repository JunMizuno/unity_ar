using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class WebCameraManager : MonoBehaviour
{
    public Button changeCameraButton;
    public Text camerasName;
    public RawImage camerasImage;

    private WebCamTexture webCamTexture;
    private WebCamDevice[] webCamDevices;

    private int selectedCameraIndex; 

    private readonly float BUTTON_MARGIN = 20.0f;

    public void Start()
    {
        SetButtonPosition();
        SetTextPosition();
        InitWebCamState();
    }

    private void InitWebCamState()
    {
        webCamDevices = WebCamTexture.devices;

        if (webCamDevices.Length <= 0)
        {
            return;
        }

        // @memo.mizuno 1番目に登録してあるカメラを選択して保持しておく。
        // @memo.mizuno カメラ名、画面サイズ、FPSを指定して作成できる。
        float screenMax = Mathf.Max(Screen.width, Screen.height);
        webCamTexture = new WebCamTexture(webCamDevices[selectedCameraIndex].name, (int)screenMax, (int)screenMax);

        // @memo.mizuno 参考、アスペクト比そのままに画面サイズに表示させる場合
        /*
        webCamTexture.requestedWidth = Screen.width;
        webCamTexture.requestedHeight = Screen.height;
        */

        // @memo.mizuno スクリーンは大きいサイズに合わせて矩形に設定。
        camerasImage.GetComponent<RectTransform>().sizeDelta = new Vector2(screenMax, screenMax);
        // @memo.mizuno デフォルトだとランドスケープで表示されてしまうので、苦肉ですが強引にポートレートになる様に角度を変えています。
        camerasImage.GetComponent<RectTransform>().localEulerAngles = new Vector3(0.0f, 0.0f, -90.0f);
        camerasImage.texture = webCamTexture;
        webCamTexture.Play();

        camerasName.text = webCamDevices[selectedCameraIndex].name;
    }

    private void SetButtonPosition()
    {
        if (changeCameraButton == null)
        {
            return;
        }

        float buttonWidth = changeCameraButton.GetComponent<RectTransform>().sizeDelta.x;
        float buttonHeight = changeCameraButton.GetComponent<RectTransform>().sizeDelta.y;
        float screenLeft = -(Screen.width / 2.0f) + (buttonWidth / 2.0f) + BUTTON_MARGIN;
        float screenBottom = -(Screen.height / 2.0f) + (buttonHeight / 2.0f) + BUTTON_MARGIN;
        var pos = changeCameraButton.transform.localPosition;
        pos.x = screenLeft;
        pos.y = screenBottom;
        changeCameraButton.transform.localPosition = pos;        
    }

    private void SetTextPosition()
    {
        if (changeCameraButton == null)
        {
            return;
        }

        if (camerasName == null)
        {
            return;
        }

        float buttonWidth = changeCameraButton.GetComponent<RectTransform>().sizeDelta.x;
        var buttonPos = changeCameraButton.transform.localPosition;
        var textPos = camerasName.transform.localPosition;
        textPos.x = buttonPos.x + buttonWidth + BUTTON_MARGIN;
        textPos.y = buttonPos.y;
        camerasName.transform.localPosition = textPos;
    }

    public void ExecuteChangeCamera()
    {
        ChangeCamera();
    }

    private void ChangeCamera()
    {
        if (webCamTexture == null)
        {
            return;
        }

        if (webCamDevices.Length <= 1)
        {
            Debug.Log("カメラは1台のみ");
            return;
        }

        webCamTexture.Stop();

        selectedCameraIndex++;
        // @memo.mizuno 自前のスマホだとインデックスについてメインカメラが「0」、インカメラが「1」となっていたため、それ以外は使用しない様にした。
        // @memo.mizuno 因みに、自前のスマホはインカメラ含め、4つのカメラ判定が存在していた。
        if (selectedCameraIndex >= 2)
        {
            selectedCameraIndex = 0;
        }

        float screenMax = Mathf.Max(Screen.width, Screen.height);
        webCamTexture = new WebCamTexture(webCamDevices[selectedCameraIndex].name, (int)screenMax, (int)screenMax);
        if (selectedCameraIndex == 0)
        {
            camerasImage.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else
        {
            // @memo.mizuno インカメラの場合は画面を反転させる。
            camerasImage.GetComponent<RectTransform>().localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        camerasImage.texture = webCamTexture;
        camerasName.text = webCamDevices[selectedCameraIndex].name;
        webCamTexture.Play();
    }
}
