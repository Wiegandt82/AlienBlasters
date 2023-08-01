using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _horizontalVelocity = 4;
    [SerializeField] float _jumpVelocity = 5;
    [SerializeField] float _jumpDuration = 0.5f;
    [SerializeField] Sprite _jumpSprite;
    [SerializeField] LayerMask _layerMask;
    [SerializeField] float _footOffset;

    public bool IsGrounded;
    float _jumpEndTime;
    SpriteRenderer _spriteRenderer;
    Animator _animator;
    float _horizontal;
    

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    void OnDrawGizmos()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Gizmos.color = Color.red;
        
        Vector2 origin = new Vector2(transform.position.x, transform.position.y - spriteRenderer.bounds.extents.y);
        Gizmos.DrawLine(origin, origin + Vector2.down * 0.1f);

        //Draw Left Foot
        origin = new Vector2(transform.position.x - _footOffset, transform.position.y - spriteRenderer.bounds.extents.y);
        Gizmos.DrawLine(origin, origin + Vector2.down * 0.1f);

        //Draw Right Foot
        origin = new Vector2(transform.position.x + _footOffset, transform.position.y - spriteRenderer.bounds.extents.y);
        Gizmos.DrawLine(origin, origin + Vector2.down * 0.1f);
    }
    
    void Update()
    {
        UpdateGrounding();

        _horizontal = Input.GetAxis("Horizontal");
        Debug.Log(_horizontal);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        var vertical = rb.velocity.y;

        if (Input.GetButtonDown("Fire1") && IsGrounded)
            _jumpEndTime = Time.time + _jumpDuration;

        if (Input.GetButtonDown("Fire1") && _jumpEndTime > Time.time)
            vertical = _jumpVelocity;

        _horizontal *= _horizontalVelocity;

        rb.velocity = new Vector2(_horizontal, vertical);

        UpdateSprite();
    }

    void UpdateGrounding()
    {
        IsGrounded = false;

        //Check center
        Vector2 origin = new Vector2(transform.position.x, transform.position.y - _spriteRenderer.bounds.extents.y);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, 0.1f, _layerMask);

        if (hit.collider)
            IsGrounded = true;

        //Check left
        origin = new Vector2(transform.position.x - _footOffset, transform.position.y - _spriteRenderer.bounds.extents.y);
        hit = Physics2D.Raycast(origin, Vector2.down, 0.1f, _layerMask);

        if (hit.collider)
            IsGrounded = true;

        //Check right
        origin = new Vector2(transform.position.x + _footOffset, transform.position.y - _spriteRenderer.bounds.extents.y);
        hit = Physics2D.Raycast(origin, Vector2.down, 0.1f, _layerMask);

        if (hit.collider)
            IsGrounded = true;
    }

    void UpdateSprite()
    {
        _animator.SetBool("IsGrounded", IsGrounded);
        _animator.SetFloat("HorizontalSpeed", Mathf.Abs(_horizontal));

        if (_horizontal > 0)
            _spriteRenderer.flipX = false;
        else if (_horizontal < 0)
            _spriteRenderer.flipX = true;   
    }
}
