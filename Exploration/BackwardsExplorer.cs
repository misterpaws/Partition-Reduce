using System;
using System.Collections.Generic;
public class BackwardsExplorer
{
    List<StateCluster> _waitingQueue;
    Model _model;
    Query _query;
    Verifyer _owningVerifyer;

    public BackwardsExplorer(Model modelToExplore,Query goal, Verifyer owner, List<StateCluster> startingStates)
    {
        _waitingQueue = new List<StateCluster>();
        _model = modelToExplore;
        _query = goal;
        _waitingQueue = startingStates;
        _owningVerifyer = owner;
    }
    public void AddToWaiting(StateCluster sc)
    {
        this._waitingQueue.Add(sc);
    }

    public StateCluster PopNextWaiting()
    {
        if (this._waitingQueue.Count == 0)
        {
            return null;
        }
        StateCluster next = this._waitingQueue[0];
        this._waitingQueue.Remove(next);
        return next;
    }

    public bool exploreFrom(StateCluster oldStateCluster)
    {
        
        Console.WriteLine("Exploring from state " + oldStateCluster.ToString());
        if (_owningVerifyer.AddPassedStateIfUnique(oldStateCluster))
        {
            List<Edge> newEdges = oldStateCluster.GetLocation().GetIncomingEdges();
            foreach (Edge currentEdge in newEdges)
            {
                Console.WriteLine("Exploring edge: " + currentEdge.ToString());
                GuardCollection updatedClusterVariables = currentEdge.ApplyUpdatesBackwards(oldStateCluster).GetVariableRange();
                GuardCollection newStateClusterVariables = updatedClusterVariables.CreateIntersectionGuardCollection(currentEdge.GetGuard());
                StateCluster newState = new StateCluster(currentEdge.GetSource(),newStateClusterVariables);
                if (newState.GetVariableRange() != GuardCollection.NULLGUARDCOLLECTION)
                {
                    if (_query.Satisfied(newState))
                    {
                        return true;
                    }
                    else
                    {
                        if (!(currentEdge is JumpEdge))
                        {
                            this._waitingQueue.Add(newState);
                        }
                        else
                        {
                            //pass new statecluster to other partition
                        }
                        
                    }
                }

            }
        }
        Console.WriteLine("Size of waiting queue : " + _waitingQueue.Count.ToString() + "\n");
        if (_waitingQueue.Count == 0)
        {
            return false;
        }
        return exploreFrom(this.PopNextWaiting());


    }
}


