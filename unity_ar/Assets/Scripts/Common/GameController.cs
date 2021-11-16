using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void FirstAction()
    {
        Debug.Log("<color=white>" + "GameController FirstAction" + "</color>");
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        var mainCamera = Camera.main;
        Debug.Log("<color=white>" + "GameController Camera:" + mainCamera.name + "</color>");
    }

    private void Update()
    {
        if (TouchManager.IsTouchDowned())
        {
            //Debug.Log("<color=white>" + "GameController IsTouchDowned" + "</color>");
            var touchPos = Input.mousePosition;
            touchPos.z = -Camera.main.transform.position.z;
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(touchPos);
            //Debug.Log("<color=white>" + "Input Position:" + Input.mousePosition + "</color>");
            //Debug.Log("<color=white>" + "touchPos:" + touchPos + "</color>");
            //Debug.Log("<color=white>" + "Touched WorldPoint:" + worldPoint + "</color>");
        }
        else if (TouchManager.IsTouchHolding())
        {
            //Debug.Log("<color=white>" + "GameController IsTouchHolding" + "</color>");
        }
        else if (TouchManager.IsTouchReleased())
        {
            //Debug.Log("<color=white>" + "GameController IsTouchReleased" + "</color>");
        }
    }
}
