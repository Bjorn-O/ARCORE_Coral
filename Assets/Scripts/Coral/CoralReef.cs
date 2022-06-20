using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoralReef : MonoBehaviour
{
    [SerializeField] float health;
    public Component[] components;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        HandleSaturation();
    }

    void HandleSaturation()
    {
        components = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mesh in components)
        {
            mesh.material.SetFloat("_Saturation_Amount", health / 100);
        }
    }
}
