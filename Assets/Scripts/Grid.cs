using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Grid : MonoBehaviour
{
    public GameObject Target;
    public GameObject Structure;
    Vector3 truePos;
    public float gridSize;

    void LateUpdate()
    {
        truePos.x = Mathf.Floor(Target.transform.position.x / gridSize) * gridSize;
        truePos.y = Mathf.Floor(Target.transform.position.y / gridSize) * gridSize;
        truePos.z = Mathf.Floor(Target.transform.position.z / gridSize) * gridSize;

        Structure.transform.position = truePos;
    }
}
