using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    //Handling
    //public float rotationSpeed = 450f;
    public float movementSpeed = 5f;
    //public float runSpeed = 8f;
    public GameObject bulletSpawnPoint;
    public float waitTime;
    public GameObject playerObject;
    public GameObject bullet;

    //System
    private Quaternion targetRotation;

    //Components
    private CharacterController _controller;
    private Camera mainCam;


    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        mainCam = Camera.main;
    }
    void Update()
    {
        ControlMouse();
        //ControlWASD();
        /*
        if (Input.GetMouseButtonDown(0))
        {
            gun.Shoot();
        }
        else if(Input.GetMouseButton(0))
        {
            gun.ShootContinuous();
        }*/
    }

    void ControlMouse()
    {
        /*
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.transform.position.y - transform.position.y));
        targetRotation = Quaternion.LookRotation(mousePos - new Vector3(transform.position.x, 0f, transform.position.z));
        transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);
        */
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDist = 0.0f;

        if (playerPlane.Raycast(ray, out hitDist))
        {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            targetRotation.x = 0f;
            targetRotation.z = 0f;
            playerObject.transform.rotation = Quaternion.Slerp(playerObject.transform.rotation, targetRotation, 7f * Time.deltaTime);
        }

        /*
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        Vector3 movement = input;
        movement *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1) ? 7f : 1f;
        movement *= (Input.GetButton("Run")) ? runSpeed : walkSpeed;
        movement += Vector3.up * -8;

        _controller.Move((movement * Time.deltaTime).normalized);*/

        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);

        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bullet.transform, bulletSpawnPoint.transform.position, Quaternion.identity);
        
    }

}
