using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    [SerializeField] LayerMask platformMask;
    float moveSpeed = 5f;

    float jumpVelocity = 10f;
    Rigidbody2D _rb;
    BoxCollider2D _col;

    [SerializeField]
    float jumpTimeBuffer = 0.25f;        //Buffer for jump time
    [SerializeField]
    float groundTime;                    //times how long player has stayed on ground

    public float hoverTime;             //time for how long player has hovered 

  
    [SerializeField]
    float jumpPressTime;   
    [SerializeField]
    float coyoteTime = 0.2f;            //coyote time buffer

    //Gun Mousemovement stuff
    Vector3 mouseInput;
    Vector3 mouseDir;
    public GameObject gun;

    public static PlayerScript instance;

    //[Range(0, 1)]
    //float jumpLimit = 0.5f;             //makes it so that holding jump gets you higher
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<BoxCollider2D>();
    }
    private void FixedUpdate()
    {
        
    }

    void Update()
    {
        JumpLogic();
        MovementInput();
        SetGunDirectionToMouse();
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
             _rb.velocity = new Vector2(+moveSpeed, _rb.velocity.y);  

        }
        else
        {
            if(Input.GetButton("Left"))
            {
                  _rb.velocity = new Vector2(-moveSpeed, _rb.velocity.y);
            }
            else
            {
                _rb.velocity = new Vector2(0, _rb.velocity.y);
            }
        }
        if(Input.GetButton("Down"))         //crouching and stuff
        {

        }
        if(Input.GetButton("Up"))           //might not be needed
        {

        }
        if (!IsGrounded())
        {
            hoverTime += Time.deltaTime;
        }
        else hoverTime = 0;
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
                _rb.velocity = new Vector2(+moveSpeed, _rb.velocity.y);
            else
            {
                _rb.velocity += new Vector2(+moveSpeed * midAirControl * Time.deltaTime, 0);
                _rb.velocity = new Vector2(Mathf.Clamp(_rb.velocity.x, -moveSpeed, +moveSpeed), _rb.velocity.y);
            }
        }
        else
        {
            if (Input.GetButton("Left"))
            {
                if (IsGrounded())
                    _rb.velocity = new Vector2(-moveSpeed, _rb.velocity.y);
                else
                {
                    _rb.velocity += new Vector2(-moveSpeed * midAirControl * Time.deltaTime, 0);
                    _rb.velocity = new Vector2(Mathf.Clamp(_rb.velocity.x, -moveSpeed, +moveSpeed), _rb.velocity.y);
                }
            }
            else
            {
                _rb.velocity = new Vector2(0, _rb.velocity.y);
            }
        }
    }
    #endregion

    void SetGunDirectionToMouse()
    {
        mouseInput = Input.mousePosition;       //mouse pos in pixel
        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mouseInput);         //convert to world pos
        mousePosWorld.z = 0;                                                        //make sure that z is 0 cos 2D
        mouseDir = mousePosWorld - transform.position;                              //get the direction, pos to player
        gun.transform.right = mouseDir;                                             //gun faces that direction. (maybe normalize?)
    }

    public void GunRecoil(float force)          //adds a force to push the player back
    {
        Vector3 forceDir = -mouseDir;
        forceDir.Normalize();
        _rb.velocity += (Vector2)forceDir*force;
    }

    public void GunRecoilVert(float force)
    {
        if (force <= 0) force = 0;
        Vector3 forceDir = -mouseDir;
        forceDir.Normalize();
        if (forceDir.y > 0f)
            _rb.velocity = new Vector2(0, forceDir.y * force);

    }
}
