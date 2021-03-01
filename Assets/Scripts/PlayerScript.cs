using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    [SerializeField] LayerMask platformMask;
    const float MOVESPEED = 5f;

    float moveX;
    float jumpVelocity = 10f;
    Vector2 moveDir;
    Rigidbody2D _rb;
    BoxCollider2D _col;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<BoxCollider2D>();
    }
    private void FixedUpdate()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            _rb.velocity = new Vector2(0, jumpVelocity);
        }
        MovementInput();
    }
    bool IsGrounded()   //uses boxcast to check if grounded
    {
        RaycastHit2D _raycast2D =  Physics2D.BoxCast(_col.bounds.center, _col.bounds.size, 0f, Vector2.down, .25f,platformMask);
        return (_raycast2D.collider != null);
    }
    void MovementInput()
    {
        if (Input.GetButton("Right"))
        {
            _rb.velocity = new Vector2(+MOVESPEED, _rb.velocity.y);    
        }
        else
        {
            if(Input.GetButton("Left"))
            {
                _rb.velocity = new Vector2(-MOVESPEED, _rb.velocity.y);
            }
            else
            {
                _rb.velocity = new Vector2(0, _rb.velocity.y);
            }
        }
       
    }

    #region For MidAirJump modifier
    void MidAirCtrlMovement()       //allows for varying mid-air movement control
    {
        float midAirControl = 10f;      //multiplier, more means more control when in the air.
        if (Input.GetButton("Right"))
        {
            if (IsGrounded())
                _rb.velocity = new Vector2(+MOVESPEED, _rb.velocity.y);
            else
            {
                _rb.velocity += new Vector2(+MOVESPEED * midAirControl * Time.deltaTime, 0);
                _rb.velocity = new Vector2(Mathf.Clamp(_rb.velocity.x, -MOVESPEED, +MOVESPEED), _rb.velocity.y);
            }
        }
        else
        {
            if (Input.GetButton("Left"))
            {
                if (IsGrounded())
                    _rb.velocity = new Vector2(-MOVESPEED, _rb.velocity.y);
                else
                {
                    _rb.velocity += new Vector2(-MOVESPEED * midAirControl * Time.deltaTime, 0);
                    _rb.velocity = new Vector2(Mathf.Clamp(_rb.velocity.x, -MOVESPEED, +MOVESPEED), _rb.velocity.y);
                }
            }
            else
            {
                _rb.velocity = new Vector2(0, _rb.velocity.y);
            }
        }
    }
    #endregion


}
