using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public GameObject MainCamera;
    private Vector3 CameraFollowPosition;
    private float zoom;
    public float CameraSpeed = 30f;


    // Start is called before the first frame update
    void Start()
    {
        CameraFollowPosition = MainCamera.transform.position;
        zoom = MainCamera.GetComponent<Camera>().orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        HandleZoom();
        HandleMovement();

        HandleMovementCamera();
    }

    public void HandleZoom()
    {
            float MoveAmount = 100f;
            if (Input.mouseScrollDelta.y > 0)
                zoom -= MoveAmount * Time.deltaTime;
            else if (Input.mouseScrollDelta.y < 0)
                zoom += MoveAmount * Time.deltaTime;

            zoom = Mathf.Clamp(zoom, 2f, 30f);
    }

    public void HandleMovement()
    {

        if (CameraFollowPosition == transform.position)
        {
            float MoveAmount = 100f;
            if (Input.GetKey(KeyCode.Z))
                CameraFollowPosition.y += MoveAmount * Time.deltaTime;
            if (Input.GetKey(KeyCode.Q))
                CameraFollowPosition.x -= MoveAmount * Time.deltaTime;
            if (Input.GetKey(KeyCode.S))
                CameraFollowPosition.y -= MoveAmount * Time.deltaTime;
            if (Input.GetKey(KeyCode.D))
                CameraFollowPosition.x += MoveAmount * Time.deltaTime;
        }
    }

    public void HandleMovementCamera()
    {
        MainCamera.GetComponent<Camera>().orthographicSize = zoom;
        Vector3 CameraMoveDir = (CameraFollowPosition - MainCamera.transform.position).normalized;
        float distance = Vector3.Distance(CameraFollowPosition, MainCamera.transform.position);

        if (distance > 0)
        {
            Vector3 NewCameraPostion = MainCamera.transform.position + CameraMoveDir * CameraSpeed * Time.deltaTime;

            float DistanceAfterMoving = Vector3.Distance(NewCameraPostion, CameraFollowPosition);

            if (DistanceAfterMoving > distance)
            {
                NewCameraPostion = CameraFollowPosition;
            }

            MainCamera.transform.position = NewCameraPostion;
        }
    }

    public void HandleShowMenu()
    {
        if (MainCamera.GetComponent<MenuManager>().isShowing == true)
        {
            //TODO desactiver déplacement et les action du Player en général

            //TODO  Activer les actions de mise en place du player (ajouter/Supprimer/Déplacer des pokemons)
            Debug.Log("C'est activé");
        }
        else
            Debug.Log("C'est pas activé");
    }
}
