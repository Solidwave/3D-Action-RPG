using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController controller;

    public Vector3 Movement => Vector3.up * verticalVelocity;
    private float verticalVelocity;

    private void Update() {
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;;
        } else {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }
    }
}
