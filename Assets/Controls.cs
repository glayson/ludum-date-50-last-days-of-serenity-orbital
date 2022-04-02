using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{


    public float speed = 10f;
    public Vector3 frame = Vector3.zero;
    public float playerBoundries = 0.9f;
    public float lerpSpeed = 0.1f;
    public Transform nave;
    Vector2 topLeft = Vector2.zero;
    Vector2 bottomRight = Vector2.zero;

    public Vector3 inclination = Vector3.zero;

    public Vector3 naveInitialAngles = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        float height = Camera.main.orthographicSize * 2.0f;
        float width = height * Camera.main.aspect;
        frame.x = width;
        frame.y = height;

        topLeft.y = height * .5f * playerBoundries;
        topLeft.x = -width * .5f * playerBoundries;

        bottomRight.y = -height * .5f * playerBoundries;
        bottomRight.x = width * .5f * playerBoundries;
    }

    // Update is called once per frame
    void Update()
    {
        if (nave)
        {
            float vertical = Mathf.Clamp(nave.position.y + speed * Input.GetAxis("Vertical") * Time.deltaTime,
                bottomRight.y,
                topLeft.y);
            float horizontal = Mathf.Clamp(nave.position.x + Input.GetAxis("Horizontal") * speed * Time.deltaTime,
            topLeft.x,
            bottomRight.x
            );

            nave.rotation = Quaternion.Euler(
                (Input.GetAxis("Vertical") * inclination.x) + naveInitialAngles.x,
                (Input.GetAxis("Horizontal") * inclination.y) + naveInitialAngles.y,
                (Input.GetAxis("Horizontal") * inclination.z) + naveInitialAngles.z
            );

            nave.position = new Vector3(horizontal, vertical, nave.position.z);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, frame);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, frame * playerBoundries);
    }
}
