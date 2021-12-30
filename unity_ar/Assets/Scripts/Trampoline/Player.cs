using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody ownRigidbody = null;
    private SphereCollider ownCollider = null;
    private Vector3 gravityValue = Vector3.zero;

    public void Start()
    {
        ownRigidbody = GetComponent<Rigidbody>();
        ownCollider = GetComponent<SphereCollider>();
        gravityValue = Physics.gravity;
        Debug.Log("<color=white>" + "Physics gravity:" + gravityValue + "</color>");
    }

    public void Update()
    {
        if (ownRigidbody != null)
        {
            if (TouchManager.IsTouchUpKey())
            {
                var force = new Vector3(0.0f, 0.0f, 5.0f);
                ownRigidbody.AddForce(force, ForceMode.Impulse);
            }

            if (TouchManager.IsTouchDownKey())
            {
                var force = new Vector3(0.0f, 0.0f, -5.0f);
                ownRigidbody.AddForce(force, ForceMode.Impulse);
            }

            if (TouchManager.IsTouchLeftKey())
            {
                var force = new Vector3(-5.0f, 0.0f, 0.0f);
                ownRigidbody.AddForce(force, ForceMode.Impulse);
            }

            if (TouchManager.IsTouchRightKey())
            {
                var force = new Vector3(5.0f, 0.0f, 0.0f);
                ownRigidbody.AddForce(force, ForceMode.Impulse);
            }
        }

        if (ownCollider != null)
        {

        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("<color=white>" + "OnCollisionEnter" + "  collisionName:" + collision.gameObject.name + "  tag:" + collision.gameObject.tag + "</color>");
    }

    public void OnCollisionExit(Collision collision)
    {
        //Debug.Log("<color=white>" + "OnCollisionExit" + "  collisionName:" + collision.gameObject.name + "  tag:" + collision.gameObject.tag + "</color>");
    }

    public void OnCollisionStay(Collision collision)
    {
        //Debug.Log("<color=white>" + "OnCollisionStay" + "  collisionName:" + collision.gameObject.name + "  tag:" + collision.gameObject.tag + "</color>");
    }
}
