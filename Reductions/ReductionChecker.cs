using System.Linq;
using System.Collections.Generic;
public class ReductionChecker : ModelVisitor
{
    private Model _targetModel;
    private List<Reduction> _availableReductions;

    private List<IReducable> _pendingIReducablesToCheck = new List<IReducable>();
    public ReductionChecker(Model modelToCheckConditions, List<Reduction> reductionsToApply)
    {
        _targetModel = modelToCheckConditions;
        _availableReductions = reductionsToApply;

        IEnumerable<LocationReduction> locationReductions = _availableReductions.OfType<LocationReduction>();

        foreach (LocationReduction currentReduction in locationReductions)
        {

            foreach (Location currentLocation in _targetModel.GetLocations())
            {
                currentReduction.InstantiateOnLocation(currentLocation);   
            }

/*
            foreach (Edge currentEdge in _targetModel.GetEdges())
            {
                //available for future edge reductions                   
            }*/

        }
        _pendingIReducablesToCheck.AddRange(_targetModel.GetLocations());
        _pendingIReducablesToCheck.AddRange(_targetModel.GetEdges());
    }

    public override void Visit(Location locationToCheckForReductions)
    {
        List<Conditional> conditionalsAtTarget = locationToCheckForReductions.GetConditionals().ToList();
        foreach (Conditional currentConditionals in conditionalsAtTarget)
        {
            if(currentConditionals.Compute())
            {
                break;
            }
        
        }
        if (_pendingIReducablesToCheck.Count > 0)
        {
            IReducable nextReducable = _pendingIReducablesToCheck.First();
            _pendingIReducablesToCheck.Remove(nextReducable);
            nextReducable.Accept(this);
        }
    }

    public override void Visit(Edge edgeToCheckForReductions)
    {
        List<Conditional> conditionalsAtTarget = edgeToCheckForReductions.GetConditionals();
        foreach (Conditional currentConditionals in conditionalsAtTarget)
        {
            if(currentConditionals.Compute())
            {
                break;
            }
        
        }
        if (_pendingIReducablesToCheck.Count > 0)
        {
            IReducable nextReducable = _pendingIReducablesToCheck.First();
            _pendingIReducablesToCheck.Remove(nextReducable);
            nextReducable.Accept(this);
        }

    }
}