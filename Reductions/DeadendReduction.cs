using System.Collections.Generic;
using System.Linq;

public class DeadendReduction : LocationReduction
{
    public DeadendReduction(Reducer master)
    {
        SetReducer(master);
    }
    public override void InstantiateOnLocation(Location target)
    {
        OutgoingCountConditional conditionToSubscribeTo;
        IntegerArgument argumentForCondition = new IntegerArgument(ExpComparator.Equal,0);
        IEnumerable<OutgoingCountConditional> conditionalsAtTarget = target.GetConditionals().OfType<OutgoingCountConditional>();
        if (conditionalsAtTarget.Count() == 1)
        {
            conditionToSubscribeTo = conditionalsAtTarget.First();    
        }
        else
        {
             conditionToSubscribeTo = new OutgoingCountConditional(target);
        }
               

        IntegerConditionalSubscriber subscriber = new IntegerConditionalSubscriber(10,argumentForCondition,this,conditionToSubscribeTo);
        conditionToSubscribeTo.Subscribe(subscriber);
        GetSubscribers().Add(subscriber);
    }

    public override bool NotifyConditionMet(ConditionalSubscriber subscriberMet)
    {
        Location removeLocation = (subscriberMet.GetSubscribeTarget().GetTarget() as Location);
        if (!_reducer.GetModel().IsCriticalLocation(removeLocation))
        {
            removeLocation.Accept(GetReducer());
            return true;
        }
        return false;
    }

    public override void NotifyConditionNoLongerMet(ConditionalSubscriber subscriberNoLongerMet)
    {
        //will never be called
    }

    public override void OnSubscriberRemoved(ConditionalSubscriber removedSubscriber)
    {
        //only one subscriber per reduction so no need to do anything else
    }
}