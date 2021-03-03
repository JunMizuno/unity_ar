using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ARCameraController : MonoBehaviour
{
    public ARCameraManager aRCameraManager;
    public Button changeCameraButton;

    public Text testText;

    private void Start()
    {
        if (aRCameraManager == null)
        {
            Debug.Log("ARカメラのアタッチミス");
        }

        testText.text = "Name:" + aRCameraManager.name + "  FacingDirection:" + aRCameraManager.requestedFacingDirection;

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
        if (aRCameraManager == null)
        {
            return;
        }

        // ??
        aRCameraManager.subsystem.Stop();

        // @todo.mizuno 20210303 上記のストップが意味がなかった場合、一旦ローカルに状態を保存してswitchの外で実行することを試す。
        CameraFacingDirection facingDirection = aRCameraManager.requestedFacingDirection;
        switch (facingDirection)
        {
            case CameraFacingDirection.World:
                aRCameraManager.requestedFacingDirection = CameraFacingDirection.User;
                break;
            case CameraFacingDirection.User:
                aRCameraManager.requestedFacingDirection = CameraFacingDirection.World;
                break;
            default:
                aRCameraManager.requestedFacingDirection = CameraFacingDirection.World;
                break;
        }

        // ??
        aRCameraManager.subsystem.Start();

        testText.text = "Name:" + aRCameraManager.name + "  FacingDirection:" + aRCameraManager.requestedFacingDirection;
    }
}
