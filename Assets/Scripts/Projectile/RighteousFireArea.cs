using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RighteousFireArea : DamageArea
{
    private void Awake()
    {
        buffType = BuffType.RighteousFireDamage;
    }
}
