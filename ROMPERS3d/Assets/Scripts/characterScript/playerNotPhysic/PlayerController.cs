using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public PlayerSettings playerSettings;
    public Checkers checkers;
    InputControl inputControl;
    [HideInInspector]
    public JumpControl jumpControl;
    [HideInInspector]
    public CharacterController characterController;
    [HideInInspector]
    public ControlInteract controlInteract;
    [HideInInspector]
    public PlayerMove playerMove;
    [HideInInspector]
    public CreateWallPlayer createWall;
    Rigidbody rb;
    Vector3 asix ;

    // Start is called before the first frame update
    void Start()
    {
        jumpControl = GetComponent<JumpControl>();
        characterController = GetComponent<CharacterController>();
        controlInteract = GetComponent<ControlInteract>();
        playerMove = GetComponent<PlayerMove>();
        createWall = GetComponent<CreateWallPlayer>();
        inputControl = InputControl.instance;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Manager.instance.fullStop)
        {
            asix = new Vector3(InputControl.instance.getAxisControl().x, 0, InputControl.instance.getAxisControl().y);
            playerMove.setInput(asix);
            playerMove.calculateDir();
            playerMove.rotate();
            playerMove.PlayerMovement();
            jumpControl.JumpInput();
        }
    }

    private void FixedUpdate()
    {
        //checkers.checkGround();
    }


    public Vector3 getAxis()
    {
        return asix;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(checkers.localPointGrounded.position,checkers.radiusGrounded);
    }
}
