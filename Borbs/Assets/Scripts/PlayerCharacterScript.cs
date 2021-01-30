using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterScript : MonoBehaviour
{
    public Camera maincamera;
    private float cameraAngle;
    private Rigidbody myrigidbody;
    public int playerID = 1;
    [SerializeField]
    private bool isgrounded = true;
    [SerializeField]
    private bool recovering = false;
    //private Quaternion recoveryRotation;
    //private Vector3 recoveryPosition;
    private float recoverytimer;
    public float recoverytime = .3f;

    public float GetUpTime = .2f;

    public float recoverydamp = 5f;
    public float speed = 6f;
    private float maxspeed;
    private float lowerspeed;

    public float turnDamping = .1f;

    float turnSmoothVel;

    public Terrain Ter;

    // Start is called before the first frame update
    void Start()
    {
        Ter = GameObject.Find("Terrain").GetComponent<Terrain>();
        myrigidbody = gameObject.GetComponent<Rigidbody>();
        lowerspeed = speed * .1f;
        maxspeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal "+playerID);
        float vertical = Input.GetAxisRaw("Vertical "+playerID);
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        //bORB is at the mercy of physics ;(
        if(!isgrounded)
        {
            //Check if the bORB should recover
            recoverytimer += Time.deltaTime;
            if (recoverytimer > recoverytime && !recovering) Recover();

        }

        //if bORB is recovering
        if (recovering && recoverytimer > GetUpTime)FinishRecovery();
        


        //bORB is grounded ;D
        if (direction.magnitude >= 0.1f)
        {
            // Have BORB face the new direction
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + maincamera.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, turnDamping);
            //transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            // Move the character forwards (relative to camera)
            //controller.Move(moveDir.normalized * speed * Time.deltaTime);
            myrigidbody.AddForce(moveDir.normalized * speed * Time.deltaTime);
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, GetComponent<SphereCollider>().radius + 0.1f);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.transform == transform)
            {
                return;
            }
            Collision(hitCollider);
        }
        
    }
        

    // when bORB collides
    private void Collision(Collider collider)
    {
        Debug.Log(myrigidbody.velocity.magnitude);
        if (myrigidbody.velocity.magnitude < 3)
        {
            return;
        }
        if (collider.transform == Ter.transform)
        {
            return;
        }
        if (isgrounded)
            {
                isgrounded = false;
            speed = lowerspeed;
        }
    }

    private void Recover()
    {
        recovering = true;
        recoverytimer = 0f;
        myrigidbody.velocity = new Vector3(0, 5f, 0);
    }
    private void FinishRecovery()
    {
        isgrounded = true;
        myrigidbody.velocity = new Vector3(0,0,0);
        recoverytimer = 0f;
        recovering = false;
        speed = maxspeed;
    }
}
