using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text_damage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("destroyMe", 0.5f);
    }
    void destroyMe()
    {
        Destroy(gameObject);
    }
}
