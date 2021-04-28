public class Query
{
    private StateCluster reachable;
    public Query(StateCluster sc)
    {
        reachable = sc;
    }
    public StateCluster GetReachable()
    {
        return this.reachable;
    }
    public bool Satisfied(StateCluster sc)
    {
        if (reachable.GetLocation().GetID()==sc.GetLocation().GetID() && this.reachable.GetVariableRange().dominatesGuardCollection(sc.GetVariableRange()))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public Location GetLocation()
    {
        return reachable.GetLocation();
    }
}