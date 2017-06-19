using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareManager : MonoBehaviour 
{
    PlayerManager playerManager;

    void Awake()
    {
        playerManager = GetComponent<PlayerManager>(); 
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerManager p = collision.transform.GetComponent<PlayerManager>();

        if(p != null && p.tag.Equals("Polygon"))
        {
            if (playerManager.GetPolygin().Equals(p.GetPolygin()))
            {
                Destroy(collision.gameObject);
            }

            playerManager.SetIsRight();
        }
    }
}
