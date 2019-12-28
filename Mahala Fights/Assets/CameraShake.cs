﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private PlayerCameraController cameraController;

    public IEnumerator Shake (float duration, float magnitude)
    {
        Vector2 startPos = Vector2.zero;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector2(x, y);

            elapsed += Time.deltaTime;

            yield return null;
        }
        cameraController.stopShake();
    }
}
