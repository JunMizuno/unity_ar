using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceOnPlane : MonoBehaviour
{
    [SerializeField, Tooltip("空間に表示させるオブジェクト")]
    GameObject appearedObject;

    private GameObject spawnedObject;
    private ARRaycastManager raycastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    public Text arStateText;

    public void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    public void Start()
    {
        SetStateTextPosition();
    }

    private void SetStateTextPosition()
    {
        if (arStateText == null)
        {
            return;
        }

        Vector2 textSize = arStateText.GetComponent<RectTransform>().sizeDelta;
        float screenRight = (Screen.width / 2.0f) - textSize.x - 20.0f;
        float screenTop = (Screen.height / 2.0f) - (textSize.y / 2.0f) - 20.0f - 100.0f;
        var pos = arStateText.transform.localPosition;
        pos.x = screenRight;
        pos.y = screenTop;
        arStateText.transform.localPosition = pos;
    }

    private void SetText(string str)
    {
        if (arStateText == null)
        {
            return;
        }

        arStateText.text = str;
    }

    public void Update()
    {
        // 画面のどこかがタップされた場合
        if (Input.touchCount > 0)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;

            // TrackableTypeを変えると判定する種別を変更できる
            if (raycastManager.Raycast(touchPosition, hits, TrackableType.Planes))
            {
                // ヒットした場合、距離順にソートされて先頭に一番近いものが格納されるとのこと
                var hitPose = hits[0].pose;

                if (spawnedObject)
                {
                    spawnedObject.transform.position = hitPose.position;
                }
                else
                {
                    spawnedObject = Instantiate(appearedObject, hitPose.position, Quaternion.identity);
                }
            }
        }
    }
}
