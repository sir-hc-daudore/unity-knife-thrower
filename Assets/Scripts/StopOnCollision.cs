using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class StopOnCollision : MonoBehaviour
{
    public string targetTag = "Target";
    public string knifeTag = "Knife";
    private Rigidbody2D rbody;

    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (string.Equals(targetTag, collision.gameObject.tag))
        {
            rbody.bodyType = RigidbodyType2D.Kinematic;
            rbody.velocity = Vector2.zero;
            rbody.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
            transform.parent = collision.transform;

            MessageService.Send(MessageType.Target_Hit);

            Debug.Log(this.name + ": " + MessageType.Target_Hit.ToString() + ", " + collision.gameObject.name);
        }
        else if(string.Equals(knifeTag, collision.gameObject.tag))
        {
            MessageService.Send(MessageType.Knife_Hit);

            Debug.Log(this.name + ": " + MessageType.Knife_Hit.ToString() + ", " + collision.gameObject.name);
        }
    }
}
