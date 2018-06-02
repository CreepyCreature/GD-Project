﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineralParticle : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;

    private Transform _targetTransform;
    private Vector2 _startPos;
    private float _startTime;
    private float _totalDistance;

    private bool _started = false;
    
    public void MoveTowards(Transform target)
    {
        _targetTransform = target;
        StartCoroutine(BeginMovement());
    }
    
    private IEnumerator BeginMovement()
    {
        _startTime = Time.time;
        _totalDistance = Vector2.Distance(transform.position, _targetTransform.position);
        float distance = _totalDistance;

        while (true)
        {
            float distCovered = (Time.time - _startTime) * speed;
            float pctDist = distCovered / _totalDistance;
            transform.position = Vector2.Lerp(transform.position, _targetTransform.position, pctDist);

            distance = Vector2.Distance(transform.position, _targetTransform.position);
            if (Mathf.Approximately(distance, 0.0f))
            {
                Debug.Log("here");
                Destroy(gameObject);
            }

            yield return null;
        }
    }
}