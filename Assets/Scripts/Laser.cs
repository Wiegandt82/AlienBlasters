using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    [SerializeField] Vector2 _direction = Vector2.left; //(Shooting) Vector for direction of laser
    [SerializeField] float _distance = 10f;             //(Shooting) float for distance of laser


    LineRenderer _lineRenderer;
    bool _isOn;

    void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        Toggle(false);
    }

    public void Toggle(bool state)
    {
        _isOn = state;
        _lineRenderer.enabled = state;
    }

    void Update()
    {
        if (_isOn == false)
            return;

        //(Shooting) End of LineRenderer = current position Vector2(transform.position) + offset (_direction * _distance)
        var endPoint = (Vector2)transform.position + (_direction * _distance);

        var firstThing = Physics2D.Raycast(transform.position, _direction, _distance); //(Shooting) Will return first collider it hits

        if (firstThing.collider) //if raycast hits something
        {
            endPoint = firstThing.point; //updates endPoint with point where raycast hit

            //take brick from hit of raycast
            var brick = firstThing.collider.GetComponent<Brick>();

            //if brick exists
            if (brick)
                brick.TakeLaserDamage();
        }
        //(shooting) Sets position of LineRenderer
        _lineRenderer.SetPosition(1, endPoint);                                 
    }
}
