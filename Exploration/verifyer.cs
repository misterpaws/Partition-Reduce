using System;
using System.Collections.Generic;
public class Verifyer
{    private List<StateCluster> passedList;
    private Model _model;
    private BackwardsExplorer _explorer;
    private Reducer _reducer;
    private ReductionChecker _checker;
    private Query _query;
    
    public Verifyer(Model inputModel)
    {
        _model = inputModel;
        passedList = new List<StateCluster>();
    }


    public bool Verify(Query queryToVerify)
    {
        _model.AddLocationAsCritical(queryToVerify.GetLocation());
        _model.AddLocationAsCritical(_model.GetInitialLocation());
        _query = queryToVerify;
        List<Reduction> _reductionsToApply = new List<Reduction>();
        _reducer = new Reducer(_model,this);
        _reductionsToApply.Add(new UnreachableReduction(_reducer));
        _reductionsToApply.Add(new DeadendReduction(_reducer));
        _reductionsToApply.Add(new PathReduction(_reducer));
        _checker = new ReductionChecker(_model,_reductionsToApply);
        _model.GetInitialLocation().Accept(_checker);
        Console.WriteLine("New Reduced Model:\n");
        _model.PrintModel();
        this._explorer = new BackwardsExplorer(_model,this._query,this, new List<StateCluster>());
        Console.WriteLine("Initial D");
        StateCluster initialState = new StateCluster(_model.GetInitialLocation(),new GuardCollection());
        
        return _explorer.exploreFrom(initialState);
    }


   public bool  AddPassedStateIfUnique(StateCluster passedState)
    {
        foreach (StateCluster oldState in this.passedList)
        {
            if (oldState.GetLocation() == passedState.GetLocation())
            {   
                Console.WriteLine("Comparing for uniqueness between states "+passedState.ToString()+oldState.ToString());
                if (oldState.GetVariableRange().dominatesGuardCollection(passedState.GetVariableRange()))
                {
                    return false;
                }
                
            }
        }
            this.passedList.Add(passedState);
            return true;

    }

    public ReductionChecker GetReductionChecker()
    {
        return _checker;
    }

}