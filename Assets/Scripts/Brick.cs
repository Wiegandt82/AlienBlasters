using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] ParticleSystem _brickParticles;
    [SerializeField] float _laserDestructionTime = 1f;          //(Brick damage) time which will take to destroy brick

    float _takingDamageTime;                                    //(Brick damage) time we are damaging brick
    SpriteRenderer _spriteRenderer;
    float _resetColorTime;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeLaserDamage()
    {
        _spriteRenderer.color = Color.red;              //(Brick change color)changes color to red when hit by laser
        _resetColorTime = Time.time + 0.1f;             //(Brick change color) time which takes to change color, current time + 0.1 second
        _takingDamageTime += Time.deltaTime;            //(Brick change color) we are adding time flowing 

        if (_takingDamageTime >= _laserDestructionTime)
            Explode();
    }

    void Update()
    {
        if (_resetColorTime > 0 && Time.time >= _resetColorTime)               //(Brick change color) resets color back to white 
        {
            _resetColorTime = 0;
            _spriteRenderer.color = Color.white;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //assign player from collision
        var player = collision.gameObject.GetComponent<Player>();
        //if not player, don't do anything
        if (player == null)
            return;

        //check direction of collision
        Vector2 normal = collision.contacts[0].normal;
        float dot = Vector2.Dot(normal, Vector2.up); //Vector.up will be if hit is from bottom
        Debug.Log(dot);

        //destroy object on collision
        if (dot > 0.5)
        {
            player.StopJump();
            Explode();
        }

    }

    private void Explode()
    {
        //Instatiate particle effect, in current position of gameObject, Quaternion.identity => default direction
        Instantiate(_brickParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
