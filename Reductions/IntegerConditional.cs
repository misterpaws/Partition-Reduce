using System;
using System.Collections.Generic;
abstract class IntegerConditional : Conditional
{
    protected int _result;
    private List<IntegerConditionalSubscriber> _reductionsSubscriberList = new List<IntegerConditionalSubscriber>();
    public IntegerConditional(IReducable target) : base(target)
    {
    }
    public override bool Compute()
    {
        SetComputed();
        CalculateInternalValue();
        foreach (IntegerConditionalSubscriber subscriberToCheckArgument in _reductionsSubscriberList)
        {
            if (subscriberToCheckArgument.SatisfiedByValue(_result))
            {
                if (subscriberToCheckArgument.OnSatisfiedEventHandler())
                {
                    //condition is satisfied if the event resulted in a reduction that removes the IReducable we are on, so we should not attempt further reductions.
                    return true;
                }
                
            }
        }
        return false;
    }
    public override IEnumerable<ConditionalSubscriber> GetConditionalSubscribers()
    {
        return GetSubscribers();
    }

    private List<IntegerConditionalSubscriber> GetSubscribers()
    {
        return _reductionsSubscriberList;
    }

    public bool Subscribe(IntegerConditionalSubscriber conditionalSubscriber)
    {
        _reductionsSubscriberList.Add(conditionalSubscriber);
        _reductionsSubscriberList.Sort(new ConditionalSubscriberComparer());
        if (HasComputed())
        {
            if (conditionalSubscriber.SatisfiedByValue(_result))
            {
                if (conditionalSubscriber.OnSatisfiedEventHandler())
                {
                    //condition is satisfied if the event resulted in a reduction that removes the IReducable we are on, so we should not attempt further reductions.
                    return true;
                }
                
            }
            
        }
        return false;
    }
    public abstract void CalculateInternalValue();
        
    public int GetResult()
    {
        return _result;
    }
}