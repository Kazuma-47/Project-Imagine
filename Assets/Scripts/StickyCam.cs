using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyCam : MonoBehaviour
{

    public Transform player;

    void Update()
    {
		if (!player) return;
        transform.position = player.position;
    }
}
