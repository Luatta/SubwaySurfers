using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_Coin : MonoBehaviour
{
    private Transform player;
    // Start is called before the first frame update
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
            // Debug.Log("吃金币");
            Control_Score._instance.Addgold();
            Destroy(this.gameObject);
        }
    }
}
