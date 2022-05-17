using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    public Camera[] cameras;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void FirstAction()
    {
        Debug.Log("<color=white>" + "GameController FirstAction" + "</color>");
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        var mainCamera = Camera.main;
        if (mainCamera != null)
        {
            Debug.Log("<color=white>" + "GameController Camera:" + mainCamera.name + "</color>");
        }
    }

    private void Update()
    {
        if (TouchManager.IsTouchDowned())
        {
            //Debug.Log("<color=white>" + "GameController IsTouchDowned" + "</color>");
            Vector3 worldPoint = TouchManager.GetWorldPositionByMousePosition(Camera.main);
            //Debug.Log("<color=white>" + "Input Position:" + Input.mousePosition + "</color>");
            //Debug.Log("<color=white>" + "Touched WorldPoint:" + worldPoint + "</color>");

            // @todo.mizuno 一旦当たり判定のテスト。
            var collidedObject = TouchManager.CheckCollidedObject(cameras[0]);
            if (collidedObject != null)
            {
                Debug.Log("<color=white>" + "GameController collidedObject:" + collidedObject.name + "</color>");
            }
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
