using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crawler : MonoBehaviour
{

    public GameObject Player;

    public float speed;

    void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.transform.position);
        if (distance > 2)
        {
            float Step = speed * Time.deltaTime;
            Vector3 TargetPos = Player.transform.position;
            TargetPos.y = transform.position.y;
            Vector3 relativePos = TargetPos - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            transform.rotation = rotation;
            transform.position = Vector3.MoveTowards(transform.position, TargetPos, Step);
        }
        else if (distance < 2)
        {

        }
    }
}
