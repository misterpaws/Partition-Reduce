using System.Collections.Generic;

public abstract class Reduction
{
    private List<ConditionalSubscriber> _subscribers = new List<ConditionalSubscriber>();
    protected Reducer _reducer;
    protected ReductionChecker _checker;

    public Reducer GetReducer()
    {
        return _reducer;
    }
    public void SetReducer(Reducer master)
    {
        _reducer = master;
    }
    public void SetChecker(ReductionChecker checker)
    {
        _checker = checker;
    }

    private static bool _initiallyTargetsLocation;
    
    public abstract void NotifyConditionNoLongerMet(ConditionalSubscriber subscriberNoLongerMet);
    public abstract bool NotifyConditionMet(ConditionalSubscriber subscriberNoLongerMet);

    public List<ConditionalSubscriber> GetSubscribers()
    {
        return _subscribers;
    }
    public void RemoveSubscriber(ConditionalSubscriber subscriberToRemove)
    {
        _subscribers.Remove(subscriberToRemove);
        OnSubscriberRemoved(subscriberToRemove);
    }

    public abstract void OnSubscriberRemoved(ConditionalSubscriber removedSubscriber);
 


    public ConditionalSubscriber GetConditionalSubscriber()
    {
        throw new System.NotImplementedException();
    }
}
