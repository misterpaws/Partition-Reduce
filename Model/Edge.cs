using System.Collections.Generic;
using System.Linq;
public class Edge : IReducable
{
    //private int _heuristic;
    private Location _source;
    private Location _destination;
    private GuardCollection _guards = new GuardCollection();
    private UpdateCollection _updates = new UpdateCollection();
    private List<Conditional> _conditionals = new List<Conditional>();

    public Edge(Location newsource, Location newdestination)
    {
        this._source = newsource;
        _source.AddOutgoingEdge(this);
        this._destination = newdestination;
        _destination.AddIngoingEdge(this);

    }

    public void AddConditional(Conditional c)
    {
        _conditionals.Add(c);
    }
    public void Accept(ModelVisitor m)
    {
        m.Visit(this);
    }

    
    public List<IReducable> GetSuccessors()
    {
        return new List<IReducable> { _destination };
    }
        public List<IReducable> GetPredecessors()
    {
        return new List<IReducable> { _source };
    }

    public override string ToString()
    {
        string returnString = "";
        foreach (KeyValuePair<char, Guard> StringGuard in _guards.getGuards())
        {
            returnString+="Guard on "+StringGuard.Key+" : "+StringGuard.Value.ToString() + "\n";
        }
        foreach (KeyValuePair<char, Update> stringUpdate in _updates.GetUpdates())
        {
            returnString+="Update on "+stringUpdate.Value.ToString() + "\n";
        }


        return _source.GetID().ToString() + " -> " +_destination.GetID().ToString();
    }
    public GuardCollection GetGuard()
    {
        return _guards;
    }
    public UpdateCollection GetUpdates()
    {
        return _updates;
    }

    public void SetUpdate(UpdateCollection u)
    {
        _updates=u;
    }
    public void SetGuard(GuardCollection g)
    {
        _guards=g;
    }
    public void SetSource(Location newSource)
    {
        _source.RemoveOutgoingEdge(this);
        newSource.AddOutgoingEdge(this);
        _source = newSource;
    }
    public Location GetDestination()
    {
        return this._destination;
    }
    public Location GetSource()
    {
        return this._source;
    }

    public StateCluster ApplyUpdatesBackwards(StateCluster sc)
    {
        GuardCollection returnGuards = new GuardCollection();
        List<char> passedVariables = new List<char>();
        foreach (KeyValuePair<char, Update> currentUpdate in _updates.GetUpdates())
        {
            Guard newValue = currentUpdate.Value.CreateGuardByUpdateBackwards(sc);
            returnGuards.addGuard(currentUpdate.Key,newValue);
            passedVariables.Add(currentUpdate.Key);
        }
        foreach (KeyValuePair<char,Guard> oldGuard in sc.GetVariableRange().getGuards())
        {
            if (!passedVariables.Contains(oldGuard.Key))
            {
                Guard newGuard = new Guard(oldGuard.Value._minValue,oldGuard.Value._maxValue);
                returnGuards.addGuard(oldGuard.Key,newGuard);
            }
        }
        StateCluster newState = new StateCluster(sc.GetLocation(),returnGuards);
        return newState;

    }

    public List<Conditional> GetConditionals()
    {
        return _conditionals;
    }

}
