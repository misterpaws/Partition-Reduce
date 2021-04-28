

using System.Collections.Generic;
public interface IReducable
{
    //public int GetHeuristic();
    public void Accept(ModelVisitor visitor);

    public void AddConditional(Conditional c);
    public List<IReducable> GetSuccessors();
    
    public List<IReducable> GetPredecessors();

    public List<Conditional> GetConditionals();
}