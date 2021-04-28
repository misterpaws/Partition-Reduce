using System.Collections.Generic;
using System;
public class IntegerConditionalSubscriber : ConditionalSubscriber
{

    private IntegerArgument _argument;
    
    public IntegerConditionalSubscriber(int priority, IntegerArgument argument, Reduction owner, Conditional conditionToSubscribeTo)
    {
        _priority = priority;
        _argument = argument;
        _owner = owner;
        SetSubscribeTarget(conditionToSubscribeTo);
        
    }
    public IntegerArgument GetIntegerArgument()
    {
        return _argument;
    }

    public override int GetPriority()
    {
        return 10;
    }

    public bool SatisfiedByValue(int value)
    {
       return _argument.IsSatisfiedByCase(value);
    }
}


