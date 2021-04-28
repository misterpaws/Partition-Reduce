using System;
public class MinusUpdate : Update
{

    public override Guard CreateGuardByUpdateBackwards(StateCluster sc)
    {
        Guard returnGuard = new Guard(sc.GetVariableRange().getGuards()[variable]._minValue + constant,Math.Min(sc.GetVariableRange().getGuards()[variable]._maxValue + constant, Program.VARIABLE_MAX));
        if (returnGuard._minValue > returnGuard._maxValue)
        {
            return Guard.NULLGUARD;
        }
        return returnGuard;
    }
    
    public MinusUpdate(char variable, int value) : base(variable,value)
    {
        
    }
    public override Update UpdateMerge(AssignUpdate lastUpdate)
    //Returns a new update that is equivalent to taking this update followed by the lastupdate passed as argument
    {
        if (lastUpdate.GetVariable() != this.variable)
        {
             throw new System.ArgumentException("Merging updates with different variables", this.ToString());
        }
        AssignUpdate newUpdate = new AssignUpdate(this.variable,lastUpdate.GetConstant()); 
        return newUpdate;
    }
        public override Update UpdateMerge(PlusUpdate lastUpdate)
    {
        if (lastUpdate.GetVariable() != this.variable)
            {
                throw new System.ArgumentException("Merging updates with different variables", "original");
            }
        int i = (lastUpdate.GetConstant()-this.constant);
        Update newUpdate;
        if (i < 0)
        {
            newUpdate = new MinusUpdate(this.variable, Math.Abs(i));
        }
        else
        {
            newUpdate = new PlusUpdate(this.variable, i);
        }
        return newUpdate;
    }

        public override Update UpdateMerge(MinusUpdate lastUpdate)
    {
        if (lastUpdate.GetVariable() != this.variable)
            {
                throw new System.ArgumentException("Merging updates with different variables", "original");
            }
        MinusUpdate newUpdate = new MinusUpdate(this.variable,(this.constant+lastUpdate.constant)); 
        return newUpdate;
    }
    
}