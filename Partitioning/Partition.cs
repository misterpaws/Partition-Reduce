using System;
using System.Collections.Generic;

public class Partition : IReducable
{
    private Model _model;
    private bool _canAnswerQuerry;
    private Reducer _reducer;
    private BackwardsExplorer _explorer;

    private List<Edge> _ingoingJumpEdges;
    private List<Edge> _outgoingJumpEdges;
    public void OnCriticalLocationRemoved(Location criticalRemovedLocation)
    {
        
    }
    public void Accept(ModelVisitor visitor)
    {
        throw new NotImplementedException();
    }

    public void AddConditional(Conditional c)
    {
        throw new NotImplementedException();
    }

    public List<Conditional> GetConditionals()
    {
        throw new NotImplementedException();
    }

    public List<IReducable> GetPredecessors()
    {
        throw new NotImplementedException();
    }

    public List<IReducable> GetSuccessors()
    {
        throw new NotImplementedException();
    }
}