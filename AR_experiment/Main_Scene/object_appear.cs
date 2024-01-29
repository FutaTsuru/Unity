using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class object_appear : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void objectappear()
    {
        this.gameObject.SetActive(true);
        Debug.Log("active");
    }

    public void objectdissapear()
    {
        this.gameObject.SetActive(false);
        Debug.Log("lost");
    }
}