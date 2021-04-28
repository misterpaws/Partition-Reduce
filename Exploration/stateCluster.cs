using System.Collections.Generic;
public class StateCluster
{
    private Location loc;
    private GuardCollection variableRange = new GuardCollection();

    public StateCluster(Location loc, GuardCollection stateVariables)
    {
        this.loc = loc;
        variableRange = stateVariables;
    }
    public Location GetLocation()
    {
        return this.loc;
    }
    public GuardCollection GetVariableRange()
    {
        return this.variableRange;
    }

    public override string ToString()
    {
        return "location: " + this.loc.GetID().ToString()+string.Join("-",this.variableRange.ToString());
    }

}