using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSeeker : MonoBehaviour
{
    GameObject target;
    Rigidbody2D body;
    public float Speed;
    public bool isSeeking;
    private float timeOut;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        timeOut = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSeeking)
        {
            Vector3 thisPos = gameObject.transform.position;

            if (target != null)
            {
                Vector3 tgtPosition = target.gameObject.transform.position;
                float posX = tgtPosition.x;
                float poxY = tgtPosition.y;
                var tgtDist = new Vector2(posX - thisPos.x, poxY - thisPos.y);
                body.velocity = tgtDist.normalized * 2.0f;
            }
            else
            {
                if (timeOut <= 0.0f)
                {
                    target = GetClosestEnemy(GameObject.FindGameObjectsWithTag("Enemy"));
                }
                else
                {
                    timeOut -= Time.deltaTime;
                }
            }
        }
    }
    GameObject GetClosestEnemy(GameObject[] enemies)
    {
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (GameObject potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    }
}
