using System;
using System.Collections.Generic;
public class Guard
{
    public static Guard NULLGUARD = new Guard(1,0);
    public static Guard OPENGUARD = new Guard(Program.VARIABLE_MIN,Program.VARIABLE_MAX);
    internal int _minValue;
    internal int _maxValue;

    public Guard(int min, int max)
    {
        _minValue = min;
        _maxValue = max;
    }
    public Guard(Guard oldGuard)
    {
        _minValue = oldGuard._minValue;
        _maxValue = oldGuard._maxValue;
    }

    public bool DominatesGuard(Guard compareGuard)
    {
        //returns true if the argument guard is fully contained inside this guard

            if (this._minValue > compareGuard._minValue || this._maxValue < compareGuard._maxValue)
            {
                return false;
            }

        return true;
    }

    public Guard GuardIntersection(Guard compareGuard)
    //returns the range satisfied by both guards. returns nullguard if nothing satisfies.
    {
        if (this._minValue > compareGuard._maxValue || this._maxValue < compareGuard._minValue)
        {
            return NULLGUARD;
        }
        else
        {
            return new Guard(Math.Max(_minValue,compareGuard._minValue),Math.Min(_maxValue,compareGuard._maxValue));
        }
        
    }
    public Guard CreatePreDateGuard(Update updateToApplyBeforeThisGuard)
    //given an update, produces the guard that will allow all values that after applying the update would satisfy the original guard
    {
        Type updateType = updateToApplyBeforeThisGuard.GetType();
        Guard newGuard;
        switch (updateToApplyBeforeThisGuard)
        {
            case AssignUpdate caseUpdate:
                return newGuard = new Guard(Program.VARIABLE_MIN,Program.VARIABLE_MAX);
                
            case PlusUpdate caseUpdate:
                return newGuard = new Guard(_minValue-caseUpdate.GetConstant(),_maxValue-caseUpdate.GetConstant());
                
            case MinusUpdate caseUpdate:
                return newGuard = new Guard(_minValue+caseUpdate.GetConstant(),_maxValue+caseUpdate.GetConstant());
                
            default:
                throw new Exception("Somehow passed an update to createPreDateGuard that isn't a known update type"+updateType.ToString());
        }
    }
    public override string ToString()
    {
        return _minValue.ToString()+"-"+_maxValue.ToString();
    }
}

