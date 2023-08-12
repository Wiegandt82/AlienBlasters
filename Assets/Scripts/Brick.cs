using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] ParticleSystem _brickParticles;
    [SerializeField] float _laserDestructionTime = 1f;          //time which will take to destroy brick
    float _takingDamageTime;                                     //timne we are damaging brick

    public void TakeLaserDamage()
    {
        _takingDamageTime += Time.deltaTime;                     //we are adding time flowing 

        if(_takingDamageTime >= _laserDestructionTime)
            Explode();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //assign player from collision
        var player = collision.gameObject.GetComponent<Player>();
        //if not player do not do anything
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
