using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInvaderMoveCommandable
{
    public void LimitMove();
    public void SetInvaders(List<GameObject> invaders);
}
