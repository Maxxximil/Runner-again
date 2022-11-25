using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RelativeMovement : MonoBehaviour
{
    [SerializeField] private Vector3 leftPos;
    [SerializeField] private Vector3 rightPos;
    [SerializeField] private Vector3 midPos;

    [SerializeField] private Vector3 laneChangeSpeed;


    private float _moveSpeed;
    public float gravity = -9.8f;
    
    public float jumpForce = 10f;

    

    private Animator _animator;
    private CharacterController _charController;

    private Vector3 _velocity;
    private float _forceDown;

    private Vector3 vector;
    private bool _isGrounded;

    private bool _isDucking;



    private void Start()
    {
        _charController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _isGrounded = true;
        _isDucking = false;

        _moveSpeed = Managers.Speed.GetData(); 

        _animator.SetBool("Run", true);
        vector = new Vector3(3, 0, 0);
    }

    private void Update()
    {
        if(Time.timeScale != 0)
        {
            _moveSpeed = Managers.Speed.GetData();
            Vector3 movement = new Vector3(0, 0, _moveSpeed);
            Vector3 leftborder = new Vector3(leftPos.x, 0, 0);
            Vector3 rightborder = new Vector3(rightPos.x, 0, 0);
            Vector3 middleborder = new Vector3(midPos.x, 0, 0);
            movement *= Time.deltaTime;
            movement = transform.TransformDirection(movement);
            leftborder = transform.TransformDirection(leftborder);
            rightborder = transform.TransformDirection(rightborder);
            middleborder = transform.TransformDirection(middleborder);

            _charController.Move(movement);

            if (Input.GetKeyDown(KeyCode.LeftArrow) && _charController.transform.position.x != leftborder.x
                && _charController.transform.position.x == middleborder.x)
            {
                StartCoroutine(CoroMoveLeft());
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) && _charController.transform.position.x != leftborder.x
                && _charController.transform.position.x == rightborder.x)
            {
                StartCoroutine(CoroMoveLeft());
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && _charController.transform.position.x != rightborder.x
                && _charController.transform.position.x == middleborder.x)
            {
                StartCoroutine(CoroMoveRight());
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) && _charController.transform.position.x != rightborder.x
                && _charController.transform.position.x == leftborder.x)
            {
                StartCoroutine(CoroMoveRight());
            }


            if (Input.GetKeyDown(KeyCode.UpArrow) && _isGrounded)
            {
                if (_isDucking)
                {
                    UnDucking();
                }
                _forceDown = -15;
                _animator.SetBool("Jump", true);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) && _isGrounded)
            {
                if (_isDucking)
                {
                    UnDucking();
                }
                else
                {
                    Ducking();
                }
            }

            if (_charController.isGrounded && _forceDown > 0) _forceDown = 1;
            else _forceDown += 40 * Time.deltaTime;


            _velocity = (Vector3.down * _forceDown) * Time.deltaTime;
            _charController.Move(_velocity);


            _isGrounded = _charController.isGrounded;

            if (_isGrounded)
            {
                _animator.SetBool("Jump", false);
            }
        }
    }

    IEnumerator CoroMoveLeft()
    {
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        for (int i = 0; i < 12; i++)
        {
            _charController.Move(-laneChangeSpeed);
            yield return new WaitForSeconds(0.015f);
        }
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
    IEnumerator CoroMoveRight()
    {
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        for (int i = 0; i < 12; i++)
        {
            _charController.Move(laneChangeSpeed);
            yield return new WaitForSeconds(0.015f);
        }
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

    private void MoveLeft(Vector3 leftborder)
    {
        //Vector3 lane = laneChangeSpeed * Time.deltaTime;
        _charController.Move(-laneChangeSpeed);
    }

    private void Ducking()
    {
        _isDucking = true;
        _charController.height = 1f ;
        _charController.center = new Vector3(0 , 0.5f ,0);
        _animator.SetBool("Dump", true);
    }

    private void UnDucking()
    {
        _isDucking = false;
        _charController.height = 2f;
        _charController.center = new Vector3(0, 1, 0);
        _animator.SetBool("Dump", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Lose")
        {
            Messenger.Broadcast("GAME_OVER");  
            _animator.SetBool("Run", false);
            Time.timeScale = 0;
        }
    }

   
}
