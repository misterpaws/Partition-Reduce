public class IntegerArgument : Argument
{
    private ExpComparator _expression;
    private int _value;    
    public IntegerArgument(ExpComparator comp, int value)
    {
        this._expression = comp;
        this._value = value;
    }

    public bool IsSatisfiedByCase(int valueToCheck)
    {
        switch (_expression)
            {
                case ExpComparator.GreaterOrEqual:

                    if (_value >= valueToCheck)
                    {
                        return true;
                    }
                    return false;
                case ExpComparator.Equal:
                    
                    if (_value == valueToCheck)
                    {
                        return true;
                    }
                    return false;
                case ExpComparator.LessOrEqual:
                    
                    if (_value <= valueToCheck)
                    {
                        return true;
                    }
                    return false;
            }
        return true;
    }
}