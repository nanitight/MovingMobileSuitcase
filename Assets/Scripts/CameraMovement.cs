
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerPos; 
    public Transform offsetObj;

    [Range(0.1f, 2.0f)]
    public float rotationSpeed = 1f;
    [Range(0.1f, 6.0f)]
    public float moveSpeed = 3.5f;

    // Update is called once per frame
    void Update()
    {
        Vector3 targetDirection = playerPos.position - transform.position;
        float singleStep = rotationSpeed * Time.deltaTime;


        Vector3 newDierction = Vector3.RotateTowards(transform.forward , targetDirection, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDierction); 

        transform.position = Vector3.MoveTowards(transform.position, offsetObj.position, moveSpeed * Time.deltaTime); 
    }
}
