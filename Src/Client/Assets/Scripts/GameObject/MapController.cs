using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public Collider minimapBoundingBox;
    // Start is called before the first frame update
    void Start()
    {
        MinimapManager.Instance.UpdateMinimap(this.minimapBoundingBox);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
