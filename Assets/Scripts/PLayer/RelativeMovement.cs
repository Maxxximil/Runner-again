using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Перемещение персонажа
[RequireComponent(typeof(CharacterController))]
public class RelativeMovement : MonoBehaviour
{
    [SerializeField] private Vector3 leftPos;
    [SerializeField] private Vector3 rightPos;
    [SerializeField] private Vector3 midPos;

    [SerializeField] private Vector3 laneChangeSpeed;


    public float gravity = -9.8f;
    private int tryCount;
    public float jumpForce = 10f;


    public InterAds interAds;
    private Animator _animator;
    private CharacterController _charController;

    private Vector3 _velocity;
    private float _forceDown;

 
    private bool _isGrounded;

    private bool _isDucking;

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

    private void Start()
    {
        _charController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _isGrounded = true;
        _isDucking = false;

        _animator.SetBool("Run", false);
        _animator.SetFloat("RunSpeed", Managers.Speed.GetData());

        tryCount = PlayerPrefs.GetInt("tryCount");
    }

    private void Update()
    {
        _animator.SetFloat("RunSpeed", Managers.Speed.GetData());
        
        if (Managers.Speed.GetData() > 0) 
        {       
           
            Vector3 leftborder = new Vector3(leftPos.x, 0, 0);
            Vector3 rightborder = new Vector3(rightPos.x, 0, 0);
            Vector3 middleborder = new Vector3(midPos.x, 0, 0);
            
            leftborder = transform.TransformDirection(leftborder);
            rightborder = transform.TransformDirection(rightborder);
            middleborder = transform.TransformDirection(middleborder);

           
            //Отслеживание свайпов
            if(Input.touchCount>0 &&Input.GetTouch(0).phase == TouchPhase.Began)
            {
                startTouchPosition = Input.GetTouch(0).position;
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                endTouchPosition = Input.GetTouch(0).position;
                if (Mathf.Abs(startTouchPosition.x - endTouchPosition.x) > Mathf.Abs(startTouchPosition.y - endTouchPosition.y)) 
                {
                    if (endTouchPosition.x > startTouchPosition.x)
                    {
                        if (_charController.transform.position.x != rightborder.x && _charController.transform.position.x == middleborder.x)
                        {
                            StartCoroutine(CoroMoveRight());
                        }

                        if (_charController.transform.position.x != rightborder.x && _charController.transform.position.x == leftborder.x)
                        {
                            StartCoroutine(CoroMoveRight());
                        }
                    }
                    if (endTouchPosition.x < startTouchPosition.x)
                    {
                        if (_charController.transform.position.x != leftborder.x && _charController.transform.position.x == middleborder.x)
                        {
                            StartCoroutine(CoroMoveLeft());
                        }

                        if (_charController.transform.position.x != leftborder.x && _charController.transform.position.x == rightborder.x)
                        {
                            StartCoroutine(CoroMoveLeft());
                        }
                    }
                }
                if (Mathf.Abs(startTouchPosition.x - endTouchPosition.x) < Mathf.Abs(startTouchPosition.y - endTouchPosition.y))
                {
                    if (endTouchPosition.y > startTouchPosition.y && _isGrounded)
                    {
                        if (_isDucking)
                        {
                            UnDucking();
                        }
                        _forceDown = -14;
                        _animator.SetBool("Jump", true);
                    }

                    if (endTouchPosition.y < startTouchPosition.y)
                    {
                        if (_isGrounded)
                        {
                            StartCoroutine(CoroDumping());
                        }
                        else
                        {
                            _forceDown = 20;
                            _velocity = (Vector3.down * _forceDown) * Time.deltaTime;
                            _charController.Move(_velocity);
                            StartCoroutine(CoroDumping());
                        }
                    }
                }

            }
            //Отслеживание нажатий кнопок

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

            //Прыжок
            if (Input.GetKeyDown(KeyCode.UpArrow) && _isGrounded)
            {
                if (_isDucking)
                {
                    UnDucking();
                }
                _forceDown = -14;
                _animator.SetBool("Jump", true);
            }
            //Подкат
            if (Input.GetKeyDown(KeyCode.DownArrow) )
            {
                if (_isGrounded)
                {
                    StartCoroutine(CoroDumping());
                }
                else
                {
                    _forceDown = 20;
                    _velocity = (Vector3.down * _forceDown) * Time.deltaTime;
                    _charController.Move(_velocity);
                    StartCoroutine(CoroDumping());
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


    //Сопрограмма перемещения влево
    IEnumerator CoroMoveLeft()
    {    
        for (int i = 0; i < 12; i++)
        {
            _charController.Move(-laneChangeSpeed);
            yield return new WaitForSeconds(0.015f);
        }      
    }

    //Сопрограмма перемещения вправо

    IEnumerator CoroMoveRight()
    {       
        for (int i = 0; i < 12; i++)
        {
            _charController.Move(laneChangeSpeed);
            yield return new WaitForSeconds(0.015f);
        }     
    }

    //Сопрограмма подката


    IEnumerator CoroDumping()
    {
        Ducking();
        yield return new WaitForSeconds(1f);
        UnDucking();
    }

    //Сесть
    private void Ducking()
    {
        _isDucking = true;
        _charController.height = 1f ;
        _charController.center = new Vector3(0 , 0.5f ,0);
        _animator.SetBool("Dump", true);
    }

    //Встать
    private void UnDucking()
    {
        _isDucking = false;
        _charController.height = 2f;
        _charController.center = new Vector3(0, 1, 0);
        _animator.SetBool("Dump", false);
    }

    //Триггер на проигрыш при косании с препятствием

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Lose")
        {
            
            //Рассылка пройгрыша
            Messenger.Broadcast("GAME_OVER");  
            _animator.SetBool("Run", false);
            tryCount++;
            //Увеличение количества попыток и показ рекламы после 2х попыток
            PlayerPrefs.SetInt("tryCount", tryCount);

            if (tryCount % 2 == 0)
            {
                interAds.ShowAd();
            }
            Managers.Speed.UpdateData(0f);
            //Managers.Speed.ChangePause(false);
        }
    }

   
}
