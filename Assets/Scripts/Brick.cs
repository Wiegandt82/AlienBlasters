using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] ParticleSystem _brickParticles;
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


            //Instatiate particle effect, in current position of gameObject, Quaternion.identity => default direction
            Instantiate(_brickParticles, transform.position, Quaternion.identity); 
            Destroy(gameObject);
        }
            
    }
}
