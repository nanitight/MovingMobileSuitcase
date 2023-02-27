using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavMeshAgent))]
public class SkaterMovement : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset inputActions;
    private InputActionMap playerActionMap;
    private InputAction movement;
    [SerializeField] private Camera camera;
    private NavMeshAgent skaterAgent;
    [SerializeField][Range(0, 0.99f)] private float smoothing = 0.25f;
    [Range(1, 20)] public float ycamDist,  zcamDist = 10;
    public  float targetLerpSpeed = 1;




    private Vector3 targetDirection, lastDirection, movementVector;
    private float lerpTime = 0;

    private void Awake()
    {
        skaterAgent = GetComponent<NavMeshAgent>();
        playerActionMap = inputActions.FindActionMap("Player");
        movement = playerActionMap.FindAction("Move");
        movement.started += HandleMovementAction;
        movement.canceled += HandleMovementAction;
        movement.performed += HandleMovementAction;

        movement.Enable();
        playerActionMap.Enable();
        inputActions.Enable();

    }

    private void HandleMovementAction(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        movementVector = new Vector3(input.x, 0, input.y);
    }

    private void Update()
    {
        movementVector.Normalize();
        if (movementVector != lastDirection)
        {
            lerpTime = 0; 
        }
        lastDirection= movementVector;
        targetDirection = Vector3.Lerp(targetDirection, movementVector, Mathf.Clamp01(lerpTime*targetLerpSpeed *(1-smoothing)));
        skaterAgent.Move(skaterAgent.speed * Time.deltaTime * targetDirection);

        Vector3 lookdirection = movementVector;
        if (lookdirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookdirection), Mathf.Clamp01(lerpTime*targetLerpSpeed * (1 - smoothing)));
        }
        lerpTime += Time.deltaTime;
    }

    private void LateUpdate()
    {
        //Vector3 target = transform.position - camera.transform.position;

        camera.transform.position = transform.position + Vector3.up * ycamDist + Vector3.back*zcamDist;


    }

    /* private void Start()
    {
        skaterAgent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        skaterAgent.Move(transform.forward * Time.deltaTime);

    }*/
}
