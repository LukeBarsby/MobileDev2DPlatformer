using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected State _state;
    public void SetState(State state)
    {
        this._state = state;
        _state.Start();
    }

    public void UpdateState()
    {
        _state.Update();
    }
}
