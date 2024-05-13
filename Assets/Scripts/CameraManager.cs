using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    private float orthographicSize = 20f;

    private float targetOthographicSize;

    private float speed = 30f;

    // Update is called once per frame
    private void Start()
    {
        targetOthographicSize = orthographicSize;
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
       
        Vector3 moveDir = new Vector3(x,y).normalized;

        transform.position += moveDir * speed * Time.deltaTime;

        // handle zoom
        float minOthogarphicSize = 10;

        float maxOthographicSize = 50;

        // set min max for input
        targetOthographicSize = Mathf.Clamp(targetOthographicSize, minOthogarphicSize, maxOthographicSize);

        float zoomAmount = 2f;

        float zoomSpeed = 5f;

        targetOthographicSize += Input.mouseScrollDelta.y * zoomAmount;

        orthographicSize = Mathf.Lerp(orthographicSize, targetOthographicSize, Time.deltaTime * zoomSpeed);

        virtualCamera.m_Lens.OrthographicSize = orthographicSize;

        //refactor code

    }


}
