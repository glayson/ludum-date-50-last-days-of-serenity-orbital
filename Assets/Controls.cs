using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controls : MonoBehaviour
{


    public float speed = 10f;
    public Vector3 frame = Vector3.zero;
    public float playerBoundries = 0.9f;
    public float lerpSpeed = 0.1f;
    public Transform nave;
    Vector2 topLeft = Vector2.zero;
    Vector2 bottomRight = Vector2.zero;
    public Transform cameraParent;

    public Image crossHair;

    public Vector3 inclination = Vector3.zero;

    public Vector3 naveInitialAngles = Vector3.zero;

    public float euclideanDistance = 10f;
    public Vector3 euclideanPoint;
    public Emiter emiter;
    public bool gameEndend = false;

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
            if (!crossHair.gameObject.activeSelf && !gameEndend)
            {
                crossHair.gameObject.SetActive(true);
            }
            euclideanPoint = nave.position + (nave.forward * euclideanDistance);
            crossHair.transform.position = Camera.main.WorldToScreenPoint(euclideanPoint);
            float vertical = Mathf.Clamp(nave.localPosition.y + speed * Input.GetAxis("Vertical") * Time.deltaTime,
                bottomRight.y,
                topLeft.y);
            float horizontal = Mathf.Clamp(nave.localPosition.x + Input.GetAxis("Horizontal") * speed * Time.deltaTime,
            topLeft.x,
            bottomRight.x
            );

            nave.localRotation = Quaternion.Euler(
                (Input.GetAxis("Vertical") * inclination.x) + naveInitialAngles.x,
                (Input.GetAxis("Horizontal") * inclination.y) + naveInitialAngles.y,
                (Input.GetAxis("Horizontal") * inclination.z) + naveInitialAngles.z
            );

            nave.localPosition = new Vector3(horizontal, vertical, nave.localPosition.z);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, frame);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, frame * playerBoundries);
        Gizmos.color = new Color(1f, 1f, 0);
        //Gizmos.DrawCube(euclideanPoint, Vector3.one);
    }
    public void ReleaseThePods()
    {
        emiter.createPods = true;
    }
    public void EndGame()
    {
        gameEndend = true;
        crossHair.gameObject.SetActive(false);
        
        Camera.main.transform.parent = cameraParent;
        Camera.main.transform.position = cameraParent.position;
        Camera.main.transform.localEulerAngles = Vector3.zero;
        Camera.main.fieldOfView = 60f;
    }
}
