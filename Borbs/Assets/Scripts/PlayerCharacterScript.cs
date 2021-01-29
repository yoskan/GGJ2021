using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterScript : MonoBehaviour
{

    public CharacterController controller;
    public Camera maincamera;
    private float cameraAngle;

    public float speed = 6f;

    public float turnDamping = .1f;

    float turnSmoothVel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            // Calculate the angle between the camera forward vector and the BORB's inversed forward vector
            /*
            Vector2 targetDir = new Vector2((maincamera.transform.position.x - transform.position.x),(maincamera.transform.position.z - transform.position.z));
            cameraAngle = Vector2.Angle(-transform.forward, targetDir);
            direction = Quaternion.Euler(0,cameraAngle,0) * direction;
            */

            // Have BORB face the new direction
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + maincamera.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, turnDamping);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            // Move the character forwards (relative to camera)
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }


    }
}
