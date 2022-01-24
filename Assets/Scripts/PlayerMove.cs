using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMove : MonoBehaviour
{
    public Transform playerModel;
    public float xySpeed = 13; 
    public Transform aimTarget;
    public float lookSpeed = 200;
    public float leanLimit = 55;


    // Start is called before the first frame update
    void Start()
    {
        playerModel = transform.GetChild(0);        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");        

        LocalMove(horizontal, vertical, xySpeed);
        RotationLook(horizontal, vertical, lookSpeed);

        HorizontalLean(playerModel, horizontal, leanLimit, .1f); 
    }

    void LocalMove(float x, float y, float speed)
    {
        this.transform.localPosition += new Vector3(x, y, 0) * speed * Time.deltaTime;        
    }

    private void LateUpdate()
    {
        ClampPlayerMove();
    }

    void ClampPlayerMove() // Limits player movement to inside the camera view
    {
        Vector3 position = Camera.main.WorldToViewportPoint(this.transform.position);
        position.x = Mathf.Clamp01(position.x);   
        position.y = Mathf.Clamp01(position.y); 
        transform.position = Camera.main.ViewportToWorldPoint(new Vector3 (position.x, position.y, position.z));
    }

    void RotationLook(float h, float v, float speed)
    {
        aimTarget.parent.position = Vector3.zero;
        aimTarget.localPosition = new Vector3(h, v, 1);
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.LookRotation(aimTarget.position), Mathf.Deg2Rad * speed);
    }

    void HorizontalLean(Transform target, float axis, float leanLimit, float lerpTime)
    {
        Vector3 targetEulerAngels = target.localEulerAngles;
        target.localEulerAngles = new Vector3(targetEulerAngels.x, targetEulerAngels.y, Mathf.LerpAngle(targetEulerAngels.z, -axis * leanLimit, lerpTime));
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(aimTarget.position, .5f);
        Gizmos.DrawSphere(aimTarget.position, .15f);
    }

}
