using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public Resource resource;

    [Header("Collactable quantity")]
    [SerializeField]
    [Range(0,10)]
    int min;

    [SerializeField]
    [Range(0, 10)]
    int max;

    public enum Resource
    {
        Wood,
        Stone,
        Iron,
        Gold,
        Diamond
    }
}
