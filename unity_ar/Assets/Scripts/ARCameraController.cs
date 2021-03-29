using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ARCameraController : MonoBehaviour
{
    public ARCameraManager arCameraManager;
    public Button changeCameraButton;

    private WebCamTexture webCamTexture;
    private WebCamDevice[] webCamDevices;

    private int currentCameraIndex = 0;

    private void Start()
    {
        if (arCameraManager == null)
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
        Debug.Log("<color=red>" + "ARCameraController:ChangeCamera" + "</color>");

        webCamDevices = WebCamTexture.devices;

        if (webCamDevices.Length <= 1)
        {
            return;
        }

        webCamTexture = new WebCamTexture(webCamDevices[currentCameraIndex].name);
        webCamTexture.Stop();

        // @memo.mizuno 原則、2つのカメラしか使用しないこととする。
        if (currentCameraIndex == 0)
        {
            currentCameraIndex++;
        }
        else
        {
            currentCameraIndex--;
        }

        webCamTexture = new WebCamTexture(webCamDevices[currentCameraIndex].name);
        webCamTexture.Play();
    }

    /// <summary>
    /// @memo.mizuno FacingDirectionは顔認識で使用するっぽい。
    /// </summary>
    public void ChangeFacingDirection()
    {
        if (arCameraManager == null)
        {
            return;
        }

        CameraFacingDirection targetDirection = CameraFacingDirection.World;
        switch (arCameraManager.currentFacingDirection)
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

        arCameraManager.requestedFacingDirection = targetDirection;
    }
}
