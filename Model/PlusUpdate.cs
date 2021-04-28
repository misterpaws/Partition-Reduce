using System;
public class PlusUpdate : Update
{


    public PlusUpdate(char variable, int value) : base(variable,value)
    {
        
    }
    public override Guard CreateGuardByUpdateBackwards(StateCluster sc)
    {
        Guard oldGuard = sc.GetVariableRange().GetValueOrDefault(variable);
        int newMin = Math.Max(oldGuard._minValue - constant,Program.VARIABLE_MIN);
        int newMax = oldGuard._maxValue - constant;
        Guard returnGuard = new Guard(newMin,newMax);
        if (returnGuard._minValue > returnGuard._maxValue)
        {
            return Guard.NULLGUARD;
        }
        return returnGuard;
    }
        public override Update UpdateMerge(AssignUpdate lastUpdate)
    {
            if (lastUpdate.GetVariable() != this.variable)
            {
                throw new System.ArgumentException("Merging updates with different variables", "original");
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
        AssignUpdate newUpdate = new AssignUpdate(this.variable,lastUpdate.GetConstant()); 
        return newUpdate;
    }
            public override Update UpdateMerge(MinusUpdate lastUpdate)
    {
            if (lastUpdate.GetVariable() != this.variable)
            {
                throw new System.ArgumentException("Merging updates with different variables", "original");
            }
        AssignUpdate newUpdate = new AssignUpdate(this.variable,lastUpdate.GetConstant()); 
        return newUpdate;
    }
    
}
