using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed = 2f;

    [SerializeField] private Transform Character;
    [SerializeField] private float offsetX, offsetY;

    private Vector3 temp;
    
    private void LateUpdate()
    {
        temp = Character.position;
        temp.x += offsetX;
        temp.y += offsetY;
        temp.z = -10f;
        transform.position = Vector3.Lerp(transform.position, temp, speed * Time.deltaTime);
    }
}
