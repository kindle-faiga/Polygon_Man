using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerManager : MonoBehaviour 
{
	[SerializeField]
	bool isRight = true;
	[SerializeField]
    int polygon = 3;
    [SerializeField]
	float speed = 0;
	[SerializeField]
	float gravity = 0;

	Transform groundCheck;
    Rigidbody2D rigitbody2d;

    bool isGround = true;

    public int GetPolygin() { return polygon; }
    public bool GetISRight() { return isRight; }
    public void SetIsRight() { isRight = !isRight; speed = -speed; }

	void Awake ()
	{
        rigitbody2d = GetComponent<Rigidbody2D>();
        groundCheck = transform.GetChild(0);
        if (!isRight) speed = -speed;
	}

    void Update()
    {
        isGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
    }

    void FixedUpdate()
    {
        if (isGround)
        {
            rigitbody2d.velocity = new Vector2(speed, 0);
		}
        else
        {
            rigitbody2d.velocity = new Vector2(0, -gravity);
        }
    }
}
