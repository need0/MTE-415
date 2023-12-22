using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckWinner : MonoBehaviour
{

    public static CheckWinner instance;

    public Camera defaultCamera;
    public Camera winnerCamera;
    public bool isWinner = false;

    public Transform target;
    public float smoothSpeed = 1.0f;

    public Transform playerRotation;

    private void Awake()
    {
        instance = this; 
    }

    // Start is called before the first frame update
    void Start()
    {
        defaultCamera.enabled = true;
        winnerCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWinner) 
        {
            defaultCamera.enabled = false;
            winnerCamera.enabled = true;
        }
    }

    private void LateUpdate()
    {
        if (target != null && isWinner) 
        { 
            Vector3 desiredPosition = new Vector3(target.position.x,target.position.y+1,target.position.z-2.5f);

            Vector3 smootherPosition = Vector3.Lerp(winnerCamera.transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            winnerCamera.transform.position = smootherPosition;

            playerRotation.LookAt(new Vector3(playerRotation.position.x,playerRotation.position.y,winnerCamera.transform.position.z));
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")&&PlayerController.instance.groundedPlayer) 
        { 
            isWinner = true;
        }
    }

}
