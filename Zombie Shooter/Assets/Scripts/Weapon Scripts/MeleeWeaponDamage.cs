using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponDamage : MonoBehaviour {

    public LayerMask collisionLayer;
    public float radius = 0.1f; //originally 3 and for the bat
    public int damage = 3;

    private void Update()
    {
        Collider2D target = Physics2D.OverlapCircle(transform.position, radius, collisionLayer);

        if (target) // to prevent null reference exception
        {
            if (target.tag == TagManager.ZOMBIE_HEALTH_TAG)
            {

                //print("We touched zombie"); //works when hitting the zombie
                target.transform.root.GetComponent<ZombieController>().DealDamage(damage); //transofrm.root brings the top most game object
            }
        }

        

    }



}
