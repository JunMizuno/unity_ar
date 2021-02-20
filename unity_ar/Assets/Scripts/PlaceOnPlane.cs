using System.Collections.Generic;
using UnityEngine;
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

    public void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
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
