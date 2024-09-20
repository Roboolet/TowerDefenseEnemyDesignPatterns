using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHealthBar : MonoBehaviour
{
    // i'm currently leaving this out of scope for testing

    public IHealthHaver owner;

    public void Start()
    {
        owner.OnHealthChanged += OnHealthChanged;
    }

    void OnHealthChanged()
    {
        // get owner health and change health bar/numbers/whatever
    }
}
