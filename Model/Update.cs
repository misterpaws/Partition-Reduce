
public abstract class Update
{
    protected char variable;
    protected int constant;

    public Update(char variable, int constant)
    {
        this.variable = variable;
        this.constant = constant;

    }

    public char GetVariable()
    {
        return this.variable;
    }
    public int GetConstant()
    {
        return this.constant;
    }



    public abstract Guard CreateGuardByUpdateBackwards(StateCluster sc);

    public abstract Update UpdateMerge(AssignUpdate lastUpdate);
    public abstract Update UpdateMerge(PlusUpdate lastUpdate);
    public abstract Update UpdateMerge(MinusUpdate lastUpdate);
    public Update UpdateMerge(Update mergeWith)
    {
        if (mergeWith is AssignUpdate)
        {
            return UpdateMerge(mergeWith as AssignUpdate);
        }
        else
        {
            if (mergeWith is PlusUpdate)
            {
                return UpdateMerge(mergeWith as PlusUpdate);
            }
            else
            {
                return UpdateMerge(mergeWith as MinusUpdate);
            }
        }
    }
    
}
