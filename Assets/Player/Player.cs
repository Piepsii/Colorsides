using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Range(0f, 2000f)]
    public float slingForce;
    public int airSlings = 2;
    private int remainingAirSlings;

    public Transform pivot;
    public Transform cameraTarget;

    private Rigidbody2D body;
    private Rigidbody2D attachedBody;
    private Camera cam;
    private Vector2 distanceToMouse;
    private bool slingRequested;
    private bool isGrounded;
    private Vector3 spawnPosition;


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        spawnPosition = transform.position;
        remainingAirSlings = airSlings;
    }

    private void Update()
    {
        Vector2 mousePositionInWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        distanceToMouse = mousePositionInWorld - (Vector2)transform.position;
        cameraTarget.position = transform.position + (Vector3)distanceToMouse / 2f;
        distanceToMouse.Normalize();
        float angle = Mathf.Atan2(distanceToMouse.y, distanceToMouse.x);
        angle *= Mathf.Rad2Deg;
        pivot.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        if (Input.GetMouseButtonDown(0) && (isGrounded || remainingAirSlings > 0) )
        {
            attachedBody = null;
            body.velocity = Vector3.zero;
            slingRequested = true;
            remainingAirSlings--;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void FixedUpdate()
    {
        if(attachedBody != null)
        {
            transform.parent = attachedBody.transform;
        }

        if (slingRequested)
        {
            body.AddForce(distanceToMouse * slingForce);
            slingRequested = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        attachedBody = collision.rigidbody;

        Platform platform = collision.collider.GetComponent<Platform>();
        if(platform != null)
        {
            if (platform.isSticky)
            {
                body.gravityScale = 0f;
                body.velocity = Vector2.zero;
                isGrounded = true;
                remainingAirSlings = airSlings;
            }
        }

        Spike spike = collision.collider.GetComponent<Spike>();
        if(spike != null)
        {
            Respawn();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        attachedBody = null;
        transform.parent = null;

        Platform platform = collision.collider.GetComponent<Platform>();
        if (platform != null)
        {
            if (platform.isSticky)
            {
                body.gravityScale = 1f;
                isGrounded = false;
            }
        }
    }

    private void Respawn()
    {
        transform.position = spawnPosition;
    }
}
