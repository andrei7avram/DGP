using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float groundDist;
    [SerializeField]
    private LayerMask groundMask;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private SpriteRenderer sr;
    [SerializeField]
    private bool isUnderwater = false;
    void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 castPos = transform.position;
        castPos.y += 1f;

        if(Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, groundMask) && !isUnderwater)
        {
           if(hit.collider != null) {
            Vector3 movePos = transform.position;
            movePos.y = hit.point.y + groundDist;
            transform.position = movePos;

           }
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(x, 0, y);
        if(isUnderwater)
        {
            speed = 5;
            
            if(Input.GetKey(KeyCode.Space))
            {
                moveDir.y = 1;
            }else if(Input.GetKey(KeyCode.LeftControl))
            {
                moveDir.y = -1;
            }
        }
        rb.velocity = moveDir * speed;

        if(x!=0 && x>0)
        {
            sr.flipX = false;
        }
        else if(x!=0 && x<0)
        {
            sr.flipX = true;
        }

        
    }

    void OnTriggerEnter(Collider other)
    {
        if(LayerMask.LayerToName(other.gameObject.layer) == "Water")
        {
            isUnderwater = true;
            rb.useGravity = false;
            Debug.Log("Underwater");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(LayerMask.LayerToName(other.gameObject.layer) == "Water")
        {
            isUnderwater = false;
            rb.useGravity = true;
            Debug.Log("Not Underwater");
        }
    }

}

