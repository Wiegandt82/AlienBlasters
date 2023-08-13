using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] Vector2 _direction = Vector2.left; //(Shooting) Vector for direction of laser
    [SerializeField] float _distance = 10f;             //(Shooting) float for distance of laser
    [SerializeField] SpriteRenderer _laserBurst; 

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
        {
            _laserBurst.enabled = false;                                                //turns off laser burst when laser is not on
            return;
        }
            

        //(Shooting) End of LineRenderer = current position Vector2(transform.position) + offset (_direction * _distance)
        var endPoint = (Vector2)transform.position + (_direction * _distance);

        var firstThing = Physics2D.Raycast(transform.position, _direction, _distance);      //(Shooting) Will return first collider it hits

        if (firstThing.collider)                                                            //if raycast hits something
        {
            endPoint = firstThing.point;                                                    //updates endPoint with point where raycast hit
            _laserBurst.transform.position = endPoint;                                      //this set laserBurst sprite on end of laser
            _laserBurst.enabled = true;                                                     //turns laserBurst when laser hits something
            _laserBurst.transform.localScale = Vector3.one * (0.5f + Mathf.PingPong(Time.time, 0.25f)); //This will make burst pulsing

            var laserDamageable = firstThing.collider.GetComponent<ITakeLaserDamage>();     //take LadyBug from hit of raycast

            if (laserDamageable != null)                                                    //if Ladybug exists take damage
                laserDamageable.TakeLaserDamage();
        }
        else
            _laserBurst.enabled = false;


        //(shooting) Sets position of LineRenderer
        _lineRenderer.SetPosition(1, endPoint);                                 
    }
}
