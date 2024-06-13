using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicePart : MonoBehaviour
{
    private float moveSpeed;
    private Vector3 targetPos;
    private bool isOkToMove;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isOkToMove)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, targetPos, Time.deltaTime * moveSpeed);
        }
    }

    public void SetUpData(float speed,Vector3 targetPos)
    {
        moveSpeed = speed;
        this.targetPos = targetPos;


        Debug.Log("part is moving to :" + targetPos);
        isOkToMove = true;
    }

}
