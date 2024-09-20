using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthHaver
{
    public float Health { get; set; }
    public event Action OnHealthChanged;
}
