using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraScript : MonoBehaviour
{
    public float cameraSpeed = 5.0f;
    public float cameraDistanceFollow = 19.5f;
    public GameObject player;
    private Rigidbody cameraRb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        cameraRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectsByType<PlayerController>(FindObjectsSortMode.None).Length > 0)
        {
            MoveCamera();
        }
    }

    void MoveCamera()
    {
        Vector3 lookPos = player.transform.position - transform.position;

        if (Vector3.Magnitude(transform.position - player.transform.position) > cameraDistanceFollow)
        {
            cameraRb.AddForce((new Vector3(lookPos.x, 0, lookPos.z)).normalized * cameraSpeed);
        }
    }
}
