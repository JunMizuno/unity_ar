using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ARCameraController : MonoBehaviour
{
    public ARCameraManager aRCameraManager;
    public Button changeCameraButton;

    private WebCamTexture webCamTexture;
    private WebCamDevice[] webCamDevices;

    private void Start()
    {
        if (aRCameraManager == null)
        {
            Debug.Log("ARカメラのアタッチミス");
        }

        SetButtonPosition();
    }

    private void SetButtonPosition()
    {
        if (changeCameraButton == null)
        {
            return;
        }

        Vector2 buttonSize = changeCameraButton.GetComponent<RectTransform>().sizeDelta;
        float screenLeft = -(Screen.width / 2.0f) + (buttonSize.x / 2.0f) + 20.0f;
        float screenBottom = -(Screen.height / 2.0f) + (buttonSize.y / 2.0f) + 20.0f;
        var pos = changeCameraButton.transform.localPosition;
        pos.x = screenLeft;
        pos.y = screenBottom;
        changeCameraButton.transform.localPosition = pos;
    }

    public void ChangeCamera()
    {

    }

    /// <summary>
    /// @memo.mizuno FacingDirectionは顔認識で使用するっぽい。
    /// </summary>
    public void ChangeFacingDirection()
    {
        if (aRCameraManager == null)
        {
            return;
        }

        CameraFacingDirection targetDirection = CameraFacingDirection.World;
        switch (aRCameraManager.currentFacingDirection)
        {
            case CameraFacingDirection.World:
                targetDirection = CameraFacingDirection.User;
                break;
            case CameraFacingDirection.User:
                targetDirection = CameraFacingDirection.World;
                break;
            default:
                targetDirection = CameraFacingDirection.World;
                break;
        }

        aRCameraManager.requestedFacingDirection = targetDirection;
    }
}
