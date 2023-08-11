using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Vector3 _position1;
    [SerializeField] Vector3 _position2;
    [Range(0f, 1f)] [SerializeField] float _percentAcross;

    void Update()
    {
        //Lerp function moves between two points, based on percentage across
        transform.position = Vector3.Lerp(_position1, _position2, _percentAcross);
    }
}
