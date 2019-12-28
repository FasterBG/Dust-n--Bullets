using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField]
    private Transform playerPos;
    [SerializeField]
    private CameraShake cameraShake;
    [SerializeField]
    private Transform holderPos;

    private bool shake = false;

    [SerializeField]
    private float duration;
    [SerializeField]
    private float magnitude;

    private float startZ;

    private void Start()
    {
        startZ = transform.position.z;
    }

    void Update()
    {
        if (shake)
        {
            StartCoroutine(cameraShake.Shake(duration, magnitude));
        }
        else
        {
            holderPos.position = new Vector3(playerPos.position.x, playerPos.position.y, startZ);
            transform.position = holderPos.position;
        }
    }

    public void Shake()
    {
        shake = true;
    }

    public void stopShake()
    {
        shake = false;
    }
}
