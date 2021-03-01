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

    [SerializeField]
    float jumpTimeBuffer = 0.25f;        //Buffer for jump time
    [SerializeField]
    float groundTime;                    //times how long player has stayed on ground

    //[Range(0, 1)]
    //float jumpLimit = 0.5f;             //makes it so that holding jump gets you higher
    [SerializeField]
    float jumpPressTime;   
    [SerializeField]
    float coyoteTime = 0.2f;            //coyote time buffer
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
        JumpLogic();
        MovementInput();
    }
    bool IsGrounded()   //uses boxcast to check if grounded
    {
        RaycastHit2D _raycast2D =  Physics2D.BoxCast(_col.bounds.center, _col.bounds.size, 0f, Vector2.down, .15f,platformMask);
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
    void JumpLogic()
    {
        jumpPressTime -= Time.deltaTime;
        groundTime -= Time.deltaTime;

        if (IsGrounded()) groundTime = coyoteTime;
     
        //Jump buffer
        if (Input.GetButtonDown("Jump"))
        {
            if(groundTime > 0 && jumpPressTime < -(jumpTimeBuffer*2))
            {
                groundTime = 0;
                _rb.velocity = new Vector2(_rb.velocity.x, jumpVelocity);
            }
            jumpPressTime = jumpTimeBuffer;
        }
        if (jumpPressTime > 0 && IsGrounded())
        {
            jumpPressTime = 0;
            _rb.velocity = new Vector2(_rb.velocity.x, jumpVelocity);
        }
        #region JumpLimit       
        //Releasing jump will cut the jump height
        //if (Input.GetButtonUp("Jump"))
        //{
        //    if(_rb.velocity.y > 0)
        //    {
        //        _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * jumpLimit);
        //    }
        //}
        #endregion
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
