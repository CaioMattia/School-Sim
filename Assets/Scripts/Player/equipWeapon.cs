using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equipWeapon : MonoBehaviour
{
    public GameObject weaponObj;
    public Transform weaponTransform;

    public Transform player;

    public Transform playerHand;
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(weaponTransform.position, player.position) < 1.5f)
        {
            if (Input.GetKey(KeyCode.F))
            {
                weaponObj.transform.parent = playerHand.transform;
                weaponObj.transform.position = playerHand.transform.position;
                weaponObj.transform.localRotation = Quaternion.identity;
                weaponObj.transform.localRotation = Quaternion.Euler(-10f, 0f, 80f);

            }
        }

    }
}
