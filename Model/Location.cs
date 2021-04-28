using System;
using System.Collections.Generic;

public class Location : IReducable
{
    //private int _heuristic;
    private int _id;
    private List<Edge> _outgoingEdges = new List<Edge>();
    private List<Edge> _incomingEdges = new List<Edge>();
    private GuardCollection _invariants = new GuardCollection();
    private List<Conditional> _conditionals = new List<Conditional>();
    public Location(int newid)
    {
        this._id = newid;
    }
    public override string ToString()
    {
        string endString = _id.ToString() + "";
        int final = _outgoingEdges.Count;
        if (final > 0)
        {
            endString += " => ";
            for (int i = 0; i < final; i++)
            {
                endString += _outgoingEdges[i].GetDestination().GetID().ToString();
            }
        }
        return endString;

    }
    public int GetID()
    {
        return _id;
    }
    public void RemoveIngoingEdge(Edge removingEdge)
    {
        _incomingEdges.Remove(removingEdge);
    }
    public void RemoveOutgoingEdge(Edge removingEdge)
    {
        _outgoingEdges.Remove(removingEdge);
    }

    public List<IReducable> GetSuccessors()
    {
        List<IReducable> returnEdges = new List<IReducable>();
        foreach (Edge edge in _outgoingEdges)
        {
            returnEdges.Add(edge);
        }
        return  returnEdges;
    }
    public List<IReducable> GetPredecessors()
    {
        List<IReducable> returnEdges = new List<IReducable>();
        foreach (Edge edge in _incomingEdges)
        {
            returnEdges.Add(edge);
        }
        return  returnEdges;
    }
    public void Accept(ModelVisitor m)
    {
        m.Visit(this);
    }

    public void SetInvariant(GuardCollection newInvariant)
    {
        this._invariants = newInvariant;
    }
    public GuardCollection GetInvariant()
    {
        return _invariants;
    }
    public void AddIngoingEdge(Edge newEdge)
    {
        this._incomingEdges.Add(newEdge);
    }
    public void AddOutgoingEdge(Edge newEdge)
    {
        this._outgoingEdges.Add(newEdge);
    }
    public List<Edge> GetOutgoingEdges()
    {
        return this._outgoingEdges;
    }
    public List<Edge> GetIncomingEdges()
    {
        return this._incomingEdges;
    }
    public void OnIngoingCountChange()
    {

    }
    public void OnOutgoingCountChange()
    {

    }

    public void AddConditional(Conditional c)
    {
        _conditionals.Add(c);
    }

    public List<Conditional> GetConditionals()
    {
        return _conditionals;
    }

    /*   public int GetHeuristic()
  {
      return _heuristic;
  }*/
}
