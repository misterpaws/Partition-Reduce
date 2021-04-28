
abstract class ConditionArgument
{
    private string Argument;
    private int _value;
    private ConditionalSubscriber Owner;
    public ConditionalSubscriber GetOwner()
    {
        return Owner;
    }
    public string GetArgument()
    {
        return Argument;
    }
    
}

