using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Handling")]
    public float movementSpeed = 5f;
    public float dodgeForce = 7f;
    public bool isDodging;
    public float fracJourney;

    [Header("Required Objects")]
    public GameObject playerObject;
    [SerializeField] private GunController _gunController;
    [SerializeField] private Animator _animator;

    //System
        private Rigidbody _rb;
    private Vector3 _moveInput;
    private Vector3 _moveVelocity;
    private Vector3 _startMarker;
    private Vector3 _endMarker;
    private Vector3 _lookPos;
    private bool _isReload;
    private float _startDodge;


    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _startDodge = Time.time;
    }
    void Update()
    {
        PlayerMaster();

        if (Input.GetMouseButtonDown(0))
        {
            _gunController.isFiring = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _gunController.isFiring = false;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (_gunController.Reload())
            {
                _isReload = true;
                Debug.Log("I'm reloading!");
            }
        }

        if (_isReload)
        {
            _gunController.FinishReload();
            _isReload = false;
        }
    }

    void PlayerMaster()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(cameraRay, out hit, 100))
        {
            _lookPos = hit.point;
        }

        Vector3 lookDir = _lookPos - transform.position;
        lookDir.y = 0;
        transform.LookAt(transform.position +  lookDir, Vector3.up);

        _moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
        _moveVelocity = _moveInput * movementSpeed;
    }

    void FixedUpdate()
    {
        _rb.velocity = _moveVelocity;
        Dodge();
    }

    void Dodge()
    {
        float distCovered = (Time.time - _startDodge) * movementSpeed;

        if (!isDodging && Input.GetKey(KeyCode.Space))
        {
            isDodging = true;
            fracJourney = 0;
            _startMarker = transform.position;
            //Add dodge animation here
            //_animator.SetBool("isDodging", isDodging);
            _endMarker = transform.position + _moveInput * dodgeForce;
        }

        if (isDodging)
        {
            //deltaTime is the amount of time between this frame and the previous frame
            fracJourney = fracJourney + Time.deltaTime * dodgeForce;

            if (fracJourney > 1)
            {  //clamp fracJourney to 1
                fracJourney = 1;
                isDodging = false; //if he's at the end of the lerp, he's done dodging after this
            }

            //lerp the guy
            transform.position = Vector3.Lerp(_startMarker, _endMarker, fracJourney);
        }
    }
}
