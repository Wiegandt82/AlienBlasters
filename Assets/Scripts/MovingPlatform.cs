using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Vector3 _position1;
    [SerializeField] Vector3 _position2;
    [Range(0f, 1f)] [SerializeField] float _percentAcross;
    [SerializeField] float _speed = 1f;

    void Update()
    {
        //below will move between time.time and 1 mutiply by speed factor
        _percentAcross = Mathf.PingPong(Time.time * _speed, 1f); 

        //Lerp function moves between two points, based on percentage across
        transform.position = Vector3.Lerp(_position1, _position2, _percentAcross);
    }

    void OnDrawGizmos()
    {
        //Platform start position red
        Gizmos.color = Color.red;
        var collider = GetComponent<BoxCollider2D>();
        Gizmos.DrawWireCube(_position1, collider.bounds.size);
        Gizmos.DrawWireCube(_position2, collider.bounds.size);

        //Platform end position
        Gizmos.color = Color.yellow;
        var currentPosition = Vector3.Lerp(_position1, _position2, _percentAcross);
        Gizmos.DrawWireCube(currentPosition, collider.bounds.size);
    }

    //Below allows to set start postion by moving platform and setting it in inspector
    [ContextMenu (nameof(SetPosition1))] public void SetPosition1() => _position1 = transform.position;

    //Below allows to set start postion by moving platform and setting it in inspector
    [ContextMenu(nameof(SetPosition2))] public void SetPosition2() => _position2 = transform.position;
}
