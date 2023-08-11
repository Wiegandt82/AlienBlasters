using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaserSwitch : MonoBehaviour
{
    [SerializeField] Sprite _left;
    [SerializeField] Sprite _right;

    [SerializeField] UnityEvent _on; //adding event for laser
    [SerializeField] UnityEvent _off;

    SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();  //get component from collision
        if (player == null)                             //if not player than do nothing (return)
            return;

        var rigidbody = player.GetComponent<Rigidbody2D>();
        if (rigidbody.velocity.x > 0)
            TurnOn();
        else if (rigidbody.velocity.x < 0)
            TurnOff();
    }

    void TurnOff()
    {
        _spriteRenderer.sprite = _left;
        _off.Invoke();
    }

    void TurnOn()
    {
        _spriteRenderer.sprite = _right;
        _on.Invoke();
    }
}
