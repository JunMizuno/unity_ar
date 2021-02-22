using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class WebCameraManager : MonoBehaviour
{
    public Button changeCameraButton;
    public Text allDevicesName;
    public Text camerasName; 

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
        webCamTexture = new WebCamTexture(webCamDevices[selectedCameraIndex].name);
        //webCamTexture.Play();

        camerasName.text = webCamDevices[selectedCameraIndex].name;

        StringBuilder str = new StringBuilder();
        foreach (WebCamDevice device in webCamDevices)
        {
            str.Append(device.name + "\n");
        }
        allDevicesName.text = str.ToString();
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

        if (allDevicesName == null)
        {
            return;
        }

        float screenLeft = -(Screen.width / 2.0f) + BUTTON_MARGIN;
        float screenTop = (Screen.height / 2.0f) - BUTTON_MARGIN;
        textPos = allDevicesName.transform.localPosition;
        textPos.x = screenLeft;
        textPos.y = screenTop;
        allDevicesName.transform.localPosition = textPos;
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
        if (selectedCameraIndex >= webCamDevices.Length)
        {
            selectedCameraIndex = 0;
        }

        // @todo.mizuno この方法ではインカメラが表示されない…
        webCamTexture = new WebCamTexture(webCamDevices[selectedCameraIndex].name);

        camerasName.text = webCamDevices[selectedCameraIndex].name;

        webCamTexture.Play();
    }
}
