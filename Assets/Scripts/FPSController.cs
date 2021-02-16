using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{

    public GameObject cam;
    public Animator anim;

    float speed = 5.0f;
    float xSensitivity = 3.0f;
    float ySensitivity = 2.0f;
    float minimumX = -89.0f;
    float maximumX = 89.0f;
    Rigidbody rb;
    CapsuleCollider capsule;

    Quaternion cameraRot;
    Quaternion characterRot;

    bool cursorIsLocked = true;
    bool lockCursor = true;

    // Start is called before the first frame update
    void Start()
    {

        rb = this.GetComponent<Rigidbody>();
        capsule = this.GetComponent<CapsuleCollider>();
        cameraRot = cam.transform.localRotation;
        characterRot = this.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F)) {

            anim.SetBool("arm", !anim.GetBool("arm"));
        }
        if (Input.GetMouseButtonDown(0)) {

            anim.SetTrigger("fire");
        }
        if (Input.GetKeyDown(KeyCode.R)) {

            anim.SetTrigger("reload");
        }
        
    }

    private void FixedUpdate() {

        float yRot = Input.GetAxis("Mouse X") * xSensitivity;
        float xRot = Input.GetAxis("Mouse Y") * ySensitivity;

        cameraRot *= Quaternion.Euler(-xRot, 0.0f, 0.0f);
        characterRot *= Quaternion.Euler(0.0f, yRot, 0.0f);

        cameraRot = ClampRotationAroundXAxis(cameraRot);

        cam.transform.localRotation = cameraRot;
        this.transform.localRotation = characterRot;

        float t = Time.deltaTime;
        float x = Input.GetAxis("Horizontal") * speed * t;
        float z = Input.GetAxis("Vertical") * speed * t;

        transform.position += cam.transform.forward * z + cam.transform.right * x; //new Vector3(x, 0.0f, z);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded()) {

            rb.AddForce(0.0f, 300.0f, 0.0f);
        }
        UpdateCursorLock();
    }

    Quaternion ClampRotationAroundXAxis(Quaternion q) {

        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
        angleX = Mathf.Clamp(angleX, minimumX, maximumX);
        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }

    bool IsGrounded() {

        RaycastHit hitInfo;

        if(Physics.SphereCast(transform.position, capsule.radius, Vector3.down, out hitInfo, (capsule.height / 2.0f) - capsule.radius + 0.1f)) {

            return true;
        }
        return false;
    }

    private void SetCursorLock(bool value) {

        lockCursor = value;
        if (!lockCursor) {

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void UpdateCursorLock() {

        if (lockCursor) {

            InternalLockUpde();
        }
    }

    private void InternalLockUpde() {

        if (Input.GetKeyUp(KeyCode.Escape)) {

            cursorIsLocked = false;
        } else if (Input.GetMouseButtonUp(0)) {

            cursorIsLocked = true;
        }

        if (cursorIsLocked) {

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        } else if (!cursorIsLocked) {

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
