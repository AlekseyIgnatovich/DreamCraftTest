using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ManeuverDirection
{
    Forward,
    Left,
    Right
}

public struct Maneuver
{
    public ManeuverDirection ManeuverDirection;
}
