using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}