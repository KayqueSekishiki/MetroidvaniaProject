using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{

    private Rigidbody2D rig;
    private bool isFront;

    private Vector2 raycastDirection;

    public bool isRight;

    public Transform point;

    public float speed;
    public float maxVision;
    public float stopDistance;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();

        if (isRight)
        {
            transform.eulerAngles = new Vector2(0, 0);
            raycastDirection = Vector2.right;
        }
        else
        {
            transform.eulerAngles = new Vector2(0, 180);
            raycastDirection = Vector2.left;
        }
    }

    // Update is called once per frame
    void Update()
    {


    }



    private void FixedUpdate()
    {
        GetPlayer();
        OnMove();
    }

    void GetPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(point.position, raycastDirection, maxVision);

        if (hit.collider != null)
        {
            if (hit.transform.CompareTag("Player"))
            {
                isFront = true;

                float distance = Vector2.Distance(transform.position, hit.transform.position);

                if (distance <= stopDistance)
                {
                    isFront = false;
                    rig.velocity = Vector2.zero;

                    hit.transform.GetComponent<Player>().OnHit();
                }
            }
        }

    }


    void OnMove()
    {
        if (isFront)
        {
            if (isRight)
            {
                transform.eulerAngles = new Vector2(0, 0);
                raycastDirection = Vector2.right;
                rig.velocity = new Vector2(speed, rig.velocity.y);
            }
            else
            {
                transform.eulerAngles = new Vector2(0, 180);
                raycastDirection = Vector2.left;
                rig.velocity = new Vector2(-speed, rig.velocity.y);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(point.position, raycastDirection * maxVision);
    }
}
