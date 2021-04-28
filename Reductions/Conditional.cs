using System.Collections.Generic;
using System;
public abstract class Conditional
{
    private bool _computed = false;
    private bool _sorted = false;
    private IReducable _target;

    public Conditional(IReducable target)
    {
        _target = target;
        target.AddConditional(this);
    }

    public void SetComputed()
    {
        _computed = true;
    }
 


    public abstract bool Compute();
    public bool HasComputed()
    {
        return _computed;
    }

    private void NotifyListener(ConditionalSubscriber listenerToNotify)
    {
        
    }
    public IReducable GetTarget()
    {
        return this._target;
    }
    public abstract IEnumerable<ConditionalSubscriber> GetConditionalSubscribers();
    /*public void CalculateHeuristic(int valueMod)
    {
        _result+=valueMod;
        this.PassToListeners();
    }*/
    
}