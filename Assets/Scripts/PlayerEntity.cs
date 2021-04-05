using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : BaseEntity
{
    // Start is called before the first frame update
    [SerializeField] LayerMask platformMask;
    [SerializeField] LayerMask downJumpMask;
    LayerMask defaultMask;

    float jumpVelocity = 10f;
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

    public static PlayerEntity instance;

    bool isDownJump;
    private void Awake()
    {
        instance = this;
    }
    protected override void Start()
    {
        base.Start();
        defaultMask = gameObject.layer;
    }

    protected override void Update()
    {
        base.Update();
        JumpLogic();
        MovementInput();
        SetGunDirectionToMouse();

    }
    bool IsGrounded()   
     //uses boxcast to check if grounded 
     //causes potential bug when boxcast is still clipping into the platform as player is falling through it
     //which delays the hovertimer from starting allowing the player to fall through multiple platforms if too close.
    {
        Vector2 colBounds = new Vector2(_col.bounds.size.x, _col.bounds.size.y / 5f);
        RaycastHit2D _raycast2D = Physics2D.BoxCast(_col.bounds.center, colBounds, 0f, Vector2.down, .5f, platformMask);
        return (_raycast2D.collider != null);
    }
    private void OnDrawGizmos()
    {
        if (_col != null)
        {
            Vector2 colBounds = new Vector2(_col.bounds.size.x, _col.bounds.size.y / 5f);
            Gizmos.DrawCube(_col.bounds.center + ((Vector3)Vector2.down * 0.5f), colBounds);
        }
    }
    void MovementInput()
    {
        if (Input.GetButton("Right"))
        {
            _rb.velocity = new Vector2(+entityStat.speed, _rb.velocity.y);

        }
        else
        {
            if (Input.GetButton("Left"))
            {
                _rb.velocity = new Vector2(-entityStat.speed, _rb.velocity.y);
            }
            else
            {
                _rb.velocity = new Vector2(0, _rb.velocity.y);
            }
        }
        if (Input.GetButton("Down"))         //crouching and stuff
        {
            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                isDownJump = true;
                gameObject.layer = LayerMask.NameToLayer("DownJumpLayer");
            }
        }
        if (Input.GetButton("Up"))           //might not be needed
        {

        }
        if (!IsGrounded())
        {
            hoverTime += Time.deltaTime;
        }
        else hoverTime = 0;
        if (isDownJump && hoverTime > 0f)
        {
            gameObject.layer = defaultMask.value;
            isDownJump = false;
        }
    }
    void JumpLogic()
    {
        jumpPressTime -= Time.deltaTime;
        groundTime -= Time.deltaTime;

        if (IsGrounded()) groundTime = coyoteTime;

        //Jump buffer
        if (Input.GetButtonDown("Jump") && !Input.GetButton("Down"))
        {
            if (groundTime > 0 && jumpPressTime < -(jumpTimeBuffer * 2))
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
                _rb.velocity = new Vector2(+entityStat.speed, _rb.velocity.y);
            else
            {
                _rb.velocity += new Vector2(+entityStat.speed * midAirControl * Time.deltaTime, 0);
                _rb.velocity = new Vector2(Mathf.Clamp(_rb.velocity.x, -entityStat.speed, +entityStat.speed), _rb.velocity.y);
            }
        }
        else
        {
            if (Input.GetButton("Left"))
            {
                if (IsGrounded())
                    _rb.velocity = new Vector2(-entityStat.speed, _rb.velocity.y);
                else
                {
                    _rb.velocity += new Vector2(-entityStat.speed * midAirControl * Time.deltaTime, 0);
                    _rb.velocity = new Vector2(Mathf.Clamp(_rb.velocity.x, -entityStat.speed, +entityStat.speed), _rb.velocity.y);
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
        _rb.velocity += (Vector2)forceDir * force;
    }

    public void GunRecoilVert(float force)
    {
        /*  Takes angle between vector down and mouse, if angle is within 15deg
         *  Apply force vertically only, calculated from recoil.
         *  Player cannot shoot up to go down faster.
         *  Code below aims to clamp jump force to limit the max height, but it's wrong.
         */
        if (IsGrounded()) return;
        float angle = Vector2.Angle(mouseDir, Vector2.down);
        if (angle > 18f) return;
        if (force <= 0) force = 0;
        Vector3 forceDir = -mouseDir;
        forceDir.Normalize();
        if (forceDir.y > 0f)
            _rb.velocity = new Vector2(0, forceDir.y * force);
        //else _rb.velocity = _rb.velocity;
        //float y = Mathf.Clamp((forceDir.y * force), 0, jumpVelocity);
        //_rb.velocity = new Vector2(0, y);
    }
}
