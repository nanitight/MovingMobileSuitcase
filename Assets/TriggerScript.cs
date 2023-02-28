using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TriggerScript : MonoBehaviour
{
    bool fixedRotation = false;
    float fixedRotationValue = -90;
    Transform playerTransform;
    public MeshRenderer playerMaterial;
    public Material[] materialHolder;
    int materialIndex = 0;
    private void Start()
    {
        playerTransform = GetComponent<Transform>();
        //playerMaterial.materials[0] = materialHolder[materialIndex];
    }
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("ENTERED");
        if (collision.CompareTag("Speed"))
        {
            GetComponent<NavMeshAgent>().speed = 10;
        }
        else if (collision.CompareTag("Rotation"))
        {
            fixedRotation = true;
        }
        else if (collision.CompareTag("Color"))
        {
            materialIndex++; 
            if (materialIndex >= materialHolder.Length)
            {
                materialIndex = materialHolder.Length - 1;
            }
            Debug.Log("setting, " + materialIndex + " to: " + materialHolder[materialIndex].name);
            Material[] newMaterials = { materialHolder[materialIndex] };
            playerMaterial.materials = newMaterials;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("EXITED");
        if (other.CompareTag("Speed"))
        {
            GetComponent<NavMeshAgent>().speed = 3.5f;
        }
        else if (other.CompareTag("Rotation"))
        {
            fixedRotation = false;
        }
        else if (other.CompareTag("Color"))
        {
            materialIndex--;
            if (materialIndex <0)
            {
                materialIndex = 0;
            }
            Material[] newMaterials = { materialHolder[materialIndex] };
            playerMaterial.materials = newMaterials;
        }
    }

    private void Update()
    {
        if (fixedRotation)
        {
            playerTransform.eulerAngles = new Vector3(playerTransform.eulerAngles.x, fixedRotationValue, playerTransform.eulerAngles.z);
        }
    }
}