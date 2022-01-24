using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class GamePlaneMovement : MonoBehaviour
{
    public float forwardSpeed = 10;
    public CinemachineDollyCart dolly;

    // Start is called before the first frame update
    void Start()
    {
        SetSpeed(forwardSpeed);
    }  


    void SetSpeed(float x)
    {
        dolly.GetComponent<CinemachineDollyCart>().m_Speed = x;
    }

}
