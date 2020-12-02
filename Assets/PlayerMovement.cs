using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public new Rigidbody rigidbody;
    public new Transform transform;

     Dictionary<string, bool> directionStatus = new Dictionary<string, bool>();
    float turnSmoothVelocity;
    public Vector3 speed = new Vector3(10, 0, 10);
    Vector3 input;
    // Start is called before the first frame update
    void Start()
    {
        directionStatus.Add("forward", true); 
        directionStatus.Add("backward", false); 
        directionStatus.Add("right", false);                
        directionStatus.Add("left", false);          
    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.z = Input.GetAxis("Vertical");
        
        RotatePlayer(input.normalized);
        Vector3 movement = new Vector3(speed.x * input.normalized.x, 0, speed.z * input.normalized.z);
        movement *= Time.deltaTime;


        // Debug.Log(input.x);
        // Debug.Log(input.z);
        rigidbody.MovePosition(rigidbody.position + movement);
        
    }

    private void RotatePlayer(Vector3 direction){
        float turnSmoothTime = 0.1f;
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        // smooth turn movement
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);



        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        FixedDirection(angle);
    }

    private void FixedDirection(float angle){
        if(Input.anyKeyDown){
            Debug.Log(angle);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
        
    }
}
