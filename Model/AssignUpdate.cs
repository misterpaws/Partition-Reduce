
public class AssignUpdate : Update
{

    public AssignUpdate(char variable, int value) : base(variable,value)
    {
        
    }

    public override Guard CreateGuardByUpdateBackwards(StateCluster currentState)
    {
        if (currentState.GetVariableRange().getGuards()[variable]._minValue <= GetConstant() && currentState.GetVariableRange().getGuards()[variable]._maxValue >= GetConstant())
        {
            return Guard.OPENGUARD;
        }
        return Guard.NULLGUARD;
        
    }


    public override Update UpdateMerge(AssignUpdate lastUpdate)
    {
            if (lastUpdate.variable != this.variable)
            {
                throw new System.ArgumentException("Merging updates with different variables", "original");
            }
        AssignUpdate newUpdate = new AssignUpdate(this.variable,0); 
        return newUpdate;
    }

        public override Update UpdateMerge(PlusUpdate lastUpdate)
    {
        if (lastUpdate.GetVariable() != this.variable)
            {
                throw new System.ArgumentException("Merging updates with different variables", "original");
            }
        AssignUpdate newUpdate = new AssignUpdate(this.variable,0); 
        return newUpdate;
    }

        public override Update UpdateMerge(MinusUpdate lastUpdate)
    {
        if (lastUpdate.GetVariable() != this.variable)
            {
                throw new System.ArgumentException("Merging updates with different variables", "original");
            }
        AssignUpdate newUpdate = new AssignUpdate(this.variable,0); 
        return newUpdate;
    }
    
}
