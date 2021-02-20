using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebCameraManager : MonoBehaviour
{
    public Button changeCameraButton;

    public void Start()
    {
        SetButtonPosition();
    }

    public void SetButtonPosition()
    {
        float buttonWidth = changeCameraButton.GetComponent<RectTransform>().sizeDelta.x;
        float buttonHeight = changeCameraButton.GetComponent<RectTransform>().sizeDelta.y;
        float screenLeft = -(Screen.width / 2.0f) + (buttonWidth / 2.0f);
        float screenBottom = -(Screen.height / 2.0f) + (buttonHeight / 2.0f);
        var pos = changeCameraButton.transform.localPosition;
        pos.x = screenLeft;
        pos.y = screenBottom;
        changeCameraButton.transform.localPosition = pos;
    }

    public void ExecuteChangeCamera()
    {

    }
}
