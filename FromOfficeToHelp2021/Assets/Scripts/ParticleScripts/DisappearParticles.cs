﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearParticles : MonoBehaviour
{
    public float destroyTime;

    private void OnTriggerEnter(Collider target)
    {
        Destroy(this.gameObject, destroyTime);
    }
}
