using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_DoubleCoin : MonoBehaviour
{
    private Transform player;
    
    void Start()
    {
        
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player.position.z < transform.position.z - 200)
        {
            Destroy(this.gameObject);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Debug.Log("双倍金币");
            Destroy(this.gameObject);
        }
    }
}
