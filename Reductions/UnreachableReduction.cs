using System.Collections.Generic;
using System.Linq;

public class UnreachableReduction : LocationReduction
{
    public UnreachableReduction(Reducer master)
    {
        SetReducer(master);
    }
    public override void InstantiateOnLocation(Location target)
    {
        IngoingCountConditional conditionToSubscribeTo;
        IntegerArgument argumentForCondition = new IntegerArgument(ExpComparator.Equal,0);
        IEnumerable<IngoingCountConditional> conditionalsAtTarget = target.GetConditionals().OfType<IngoingCountConditional>();
        if (conditionalsAtTarget.Count() == 1)
        {
            conditionToSubscribeTo = conditionalsAtTarget.First();    
        }
        else
        {
             conditionToSubscribeTo = new IngoingCountConditional(target);
        }
               

        IntegerConditionalSubscriber subscriber = new IntegerConditionalSubscriber(10,argumentForCondition,this,conditionToSubscribeTo);
        conditionToSubscribeTo.Subscribe(subscriber);
        GetSubscribers().Add(subscriber);
    }

    public override bool NotifyConditionMet(ConditionalSubscriber subscriberMet)
    {
        Location removeLocation = (subscriberMet.GetSubscribeTarget().GetTarget() as Location);
        removeLocation.Accept(GetReducer());
        return true;
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