using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetcollider : MonoBehaviour
{
    public static targetcollider instance;

    void Start(){
        Vector3 temp;
        temp.x = 1.261f;
        temp.y = 0.217f;
        temp.z = -1.285f;
        transform.position = new Vector3 (temp.x, temp.y, temp.z);
    }

    void Awake(){
        if(instance == null){
            instance = this;
        }
    }

    void OnTriggerEnter(Collider other){
        moveTarget();
    }

    public void moveTarget(){
        Vector3 temp;
        temp.x = Random.Range(-2.5f, 2.5f);
        temp.y = Random.Range(0.4f, 0.8f);
        temp.z = Random.Range(-2.5f, 2.5f);
        transform.position = new Vector3 (temp.x, temp.y-6.5f, temp.z);
    }
}
