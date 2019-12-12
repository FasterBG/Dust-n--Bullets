using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField]
    private Transform[] players;
    [SerializeField]
    private float autoAimDistance;

    private void Update()
    {
        FaceTowards(closestPlayer());
    }

    private Transform closestPlayer()
    {
        float distance = Vector2.Distance(transform.position, players[0].position);
        int closestPlayerIndex = 0;
        for (int i = 1; i < players.Length; i++)
        {
            if(distance > Vector2.Distance(transform.position, players[i].position))
            {
                distance = Vector2.Distance(transform.position, players[i].position);
                closestPlayerIndex = i;
            }
        }
        return players[closestPlayerIndex];
    }

    private void FaceTowards(Transform objectToLook)
    {
        if(autoAimDistance > Vector2.Distance(transform.position, objectToLook.position))
        {
            Vector2 direction = new Vector2(objectToLook.position.x - transform.position.x, objectToLook.position.y - transform.position.y);
            transform.up = direction;
        }
    }
}
