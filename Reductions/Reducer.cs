using System.Linq;
using System.Collections.Generic;
public class Reducer : ModelVisitor
{
    private Model _reducingModel;
    private Verifyer _verifyer;

    public Reducer(Model reducingModel, Verifyer owner)
    {
        _reducingModel = reducingModel;
        _verifyer = owner;
    }

    public override void Visit(Location locationToDestroy)
    {
        List<Conditional> conditionalsToRemove = locationToDestroy.GetConditionals();
        foreach (Conditional currentConditional in conditionalsToRemove)
        {
            IEnumerable<ConditionalSubscriber> subscribersToRemove = currentConditional.GetConditionalSubscribers();
            foreach (ConditionalSubscriber currentSubscriber in subscribersToRemove)
            {
                currentSubscriber.GetReduction().RemoveSubscriber(currentSubscriber);
            }
        }
        
        List<Edge> precedingEdges = locationToDestroy.GetIncomingEdges();
        List<Edge> succeedingEdges = locationToDestroy.GetOutgoingEdges();

        for (int i = 0; i < precedingEdges.Count; i++)
        {
            precedingEdges[i].Accept(this);
        }
        /*
        foreach (Edge currentEdge in precedingEdges)
        {
            currentEdge.Accept(this);
        }
        foreach (Edge currentEdge in succeedingEdges)
        {
            currentEdge.Accept(this);
        }
        */
        for (int i = 0; i < succeedingEdges.Count; i++)
        {
            succeedingEdges[i].Accept(this);
        }

        _reducingModel.GetLocations().Remove(locationToDestroy);
    }

    public override void Visit(Edge edgeToCheckForReductions)
    {
        Location destination = edgeToCheckForReductions.GetDestination();
        Location source = edgeToCheckForReductions.GetSource();

        destination.RemoveIngoingEdge(edgeToCheckForReductions);
        _verifyer.GetReductionChecker().Visit(destination);
        
        source.RemoveOutgoingEdge(edgeToCheckForReductions);
        _verifyer.GetReductionChecker().Visit(source);

        List<Conditional> conditionalsToRemove = edgeToCheckForReductions.GetConditionals();
        foreach (Conditional currentConditional in conditionalsToRemove)
        {
            IEnumerable<ConditionalSubscriber> subscribersToRemove = currentConditional.GetConditionalSubscribers();
            foreach (ConditionalSubscriber currentSubscriber in subscribersToRemove)
            {
                currentSubscriber.GetReduction().RemoveSubscriber(currentSubscriber);
            }
        }
    }
/*
    public void Visit(Partition partitionToReduce)
    {

    }

    public void Visit(JumpEdge jumpEdgeToRemove)
    {

    }
    */
    public Model GetModel()
    {
        return _reducingModel;
    }
    public Verifyer GetVerifyer()
    {
        return _verifyer;
    }
}