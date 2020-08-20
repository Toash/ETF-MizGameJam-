using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MyCameraController : MonoBehaviour
{
    private CinemachineVirtualCamera _vcam;
    private float _minZoom = 1;
    private float _maxZoom = 15;

    private void Awake()
    {
        _vcam = GetComponent<CinemachineVirtualCamera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraZoom();
    }

    private void CameraZoom()
    {
        _vcam.m_Lens.OrthographicSize = Mathf.Clamp(_vcam.m_Lens.OrthographicSize, _minZoom, _maxZoom);
        _vcam.m_Lens.OrthographicSize -= Input.GetAxis("Mouse ScrollWheel") * 5;

    }
    
}
