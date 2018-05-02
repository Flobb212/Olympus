using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[FlagsAttribute]
public enum Directions
{
    none = 0,
    up = 1,
    right = 2,
    down = 4,
    left = 8
}
