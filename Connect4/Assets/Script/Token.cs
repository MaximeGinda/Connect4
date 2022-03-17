using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour
{
    private Vector3 targetPosition;
    private float speed=30.0F;
    private bool canMove = false;
 
    // Start is called before the first frame update
    void Start()
    {
        //canMove = false;
    }

    // Update is called once per frame
    void Update()
    {
	if(canMove){
            if((transform.position - targetPosition).y>0F){
	        transform.position += new Vector3(0F,-speed*Time.deltaTime,0F);
	    }
            else{
                canMove = false;
                transform.position = targetPosition;
            }
	}   
    }

    public void setTargetPosition(Vector3 target){
        targetPosition = target;
        canMove = true;
    }
}
