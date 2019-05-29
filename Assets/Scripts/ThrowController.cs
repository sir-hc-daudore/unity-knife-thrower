using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowController : MonoBehaviour
{
    public GameObject knifePrefab;
    public float throwVelocity = 100.0f;

    private GameObject knifeInstance;

	// Use this for initialization
	void Start ()
    {
        knifeInstance = MakeKnife(knifePrefab, transform.position);
        MessageService.Subscribe(MessageType.Target_Hit, OnTargetHit);
    }

    private void OnDestroy()
    {
        MessageService.Unsubscribe(MessageType.Target_Hit, OnTargetHit);
    }

    // Update is called once per frame
    void Update ()
    {
        if (knifeInstance && Input.GetButtonDown("Fire1"))
        {
            Debug.Log(this.name + " is throwing a knife");
            var rbody = knifeInstance.GetComponent<Rigidbody2D>();
            rbody.bodyType = RigidbodyType2D.Dynamic;
            rbody.velocity = Vector2.up * throwVelocity;
            knifeInstance = null;
        }
    }

    private void OnTargetHit(MessageArguments args)
    {
        Debug.Log(this.name + " is instancing a new knife");
        knifeInstance = MakeKnife(knifePrefab, transform.position);
    }

    private static GameObject MakeKnife(GameObject prefab, Vector2 position)
    {
        var knife = Instantiate(prefab, position, Quaternion.identity);
        knife.name = "Knife " + Time.time;
        return knife;
    }
}
