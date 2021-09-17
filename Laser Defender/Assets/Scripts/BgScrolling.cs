using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgScrolling : MonoBehaviour
{
    [SerializeField] private float backgroundScrollSpeed = 0.02f;

    private Material bgMaterial;

    private Vector2 offset;
    
    void Start()
    {
        bgMaterial = GetComponent<Renderer>().material;
        offset = new Vector2(backgroundScrollSpeed, backgroundScrollSpeed);
    }

    void Update()
    {
        bgMaterial.mainTextureOffset += offset * Time.deltaTime;
    }
}
