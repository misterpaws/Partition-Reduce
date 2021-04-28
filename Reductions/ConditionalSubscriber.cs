using System.Collections.Generic;
using System;
public abstract class ConditionalSubscriber
{
    private Conditional _subscribeTarget;
    protected Reduction _owner;
    protected int _priority;

   public abstract int GetPriority();


    public Reduction GetReduction()
    {
        return _owner;
    }
    public bool OnSatisfiedEventHandler()
    {
        if (GetReduction().NotifyConditionMet(this))
        {
            return true;
        }
        return false;
    }
    public Conditional GetSubscribeTarget()
    {
        return _subscribeTarget;
    }
    public void SetSubscribeTarget(Conditional target)
    {
        _subscribeTarget = target;
    }
}