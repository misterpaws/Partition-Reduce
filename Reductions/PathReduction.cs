using System.Collections.Generic;
using System.Linq;
using System;

public class PathReduction : LocationReduction
{
    List<IntegerConditionalSubscriber> _ingoingSubscribers = new List<IntegerConditionalSubscriber>();
    Dictionary<ConditionalSubscriber,IntegerConditionalSubscriber> _subscriberMap = new Dictionary<ConditionalSubscriber, IntegerConditionalSubscriber>();
    public PathReduction(Reducer master)
    {
        SetReducer(master);
    }
    public override void InstantiateOnLocation(Location target)
    {
        IngoingCountConditional conditionToSubscribeTo;
        IntegerArgument argumentForCondition = new IntegerArgument(ExpComparator.Equal,1);
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

    private void InstantiateSecondConditional(Location target, ConditionalSubscriber triggerCondition)
    {
        if (_reducer.GetModel().IsCriticalLocation(target))
        {
            
        }
        else
        {
            OutgoingCountConditional conditionToSubscribeTo;
            IntegerArgument argumentForCondition = new IntegerArgument(ExpComparator.Equal,1);
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
            _subscriberMap.Add(triggerCondition,subscriber);
            conditionToSubscribeTo.Subscribe(subscriber);
            GetSubscribers().Add(subscriber);
            target.Accept(_reducer.GetVerifyer().GetReductionChecker());
        }
    }

    public override bool NotifyConditionMet(ConditionalSubscriber subscriberMet)
    {
        Location removeLocation = (subscriberMet.GetSubscribeTarget().GetTarget() as Location);
        if (!(subscriberMet.GetSubscribeTarget() is OutgoingCountConditional))
        {
            if (!_subscriberMap.ContainsKey(subscriberMet))
            {
                InstantiateSecondConditional(removeLocation,subscriberMet);
                return false;
            }
        }
        else
        {
            
            {
                            Console.WriteLine("Applying Path Redcution on "+removeLocation.ToString());
            Edge oldOutgoingEdge = removeLocation.GetOutgoingEdges()[0];
            Edge oldIngoingEdge = removeLocation.GetIncomingEdges()[0];
            GuardCollection oldOutgoingGuards = oldOutgoingEdge.GetGuard();
            GuardCollection oldIngoingGuards = oldIngoingEdge.GetGuard();
            UpdateCollection oldOutgoingUpdates = oldOutgoingEdge.GetUpdates();
            UpdateCollection oldIngoingUpdates = oldIngoingEdge.GetUpdates();

            /*  To apply path reduction, we need to predate the outgoing guard by the ingoing update, 
                then get the intersection between the predated guards and the ingoing guards.
                Next we need to merge the updates, overwrite the new guard and update on the outgoing edge,
                then move the outgoing edge to the source of the original ingoing edge. */

            GuardCollection replacementGuardCollection;
            GuardCollection preDateOfOldGuardCollection = oldOutgoingEdge.GetGuard().createPreDateGuardCollection(oldIngoingUpdates);
            replacementGuardCollection = preDateOfOldGuardCollection.CreateIntersectionGuardCollection(oldIngoingGuards);
            if (replacementGuardCollection == GuardCollection.NULLGUARDCOLLECTION)
            {
                removeLocation.Accept(GetReducer());
            }
            UpdateCollection replacementUpdateCollection;
            replacementUpdateCollection = oldIngoingUpdates.CreateNewUpdateCollectionByMerging(oldOutgoingUpdates);

            oldOutgoingEdge.SetGuard(replacementGuardCollection);
            oldOutgoingEdge.SetUpdate(replacementUpdateCollection);
            oldOutgoingEdge.SetSource(oldIngoingEdge.GetSource());
            removeLocation.Accept(GetReducer());
            return true;
            }

        }
        return false;
        
    }

    public override void NotifyConditionNoLongerMet(ConditionalSubscriber subscriberNoLongerMet)
    {
        //will never be called
    }

    public override void OnSubscriberRemoved(ConditionalSubscriber removedSubscriber)
    {
        if (_subscriberMap.ContainsKey(removedSubscriber))
        {
            ConditionalSubscriber coConditionalSubscriber = _subscriberMap[removedSubscriber];
            _subscriberMap.Remove(removedSubscriber);
            this.RemoveSubscriber(coConditionalSubscriber);
        }

    }
    
}