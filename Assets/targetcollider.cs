using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class targetcollider : DefaultTrackableEventHandler
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
        temp.x = Random.Range(-1.6f, 1.7f);
        temp.y = Random.Range(-0.7f, -0.8f);
        temp.z = Random.Range(-1.6f, 1.5f);
        transform.position = new Vector3 (temp.x, temp.y, temp.z);

        if(DefaultTrackableEventHandler.trueFalse == true){
            RaycastController.instance.playSound(0);
        }
    }
}
