using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RotateBehaviour : MonoBehaviour
{
    public float maxAngularVelocity = 100f;
    public float angularAcceleration = 20f;

    private Rigidbody2D rbody;

	// Use this for initialization
	void Start () {
        rbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        rbody.angularVelocity = Mathf.Min(maxAngularVelocity, rbody.angularVelocity + angularAcceleration * Time.fixedDeltaTime);
	}
}
