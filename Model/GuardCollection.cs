using System;
using System.Collections.Generic;
public class GuardCollection
{

    private Dictionary<char,Guard> _guards = new Dictionary<char, Guard>();
    public static GuardCollection NULLGUARDCOLLECTION = new GuardCollection();


    public Guard GetValueOrDefault(char key)
    {
        Guard ret;
        if(_guards.TryGetValue(key, out ret))
        {
            
        }
        else
        {
            ret = Guard.OPENGUARD;
        }
        return ret;
    }



    public bool dominatesGuardCollection(GuardCollection guardsToCompare)
    {
        //if there is no entry the guard is an openguard, so it automatically dominates the compared guard
        foreach (KeyValuePair<char,Guard> guardsFromSelf in _guards)
        {
            Guard currentGuardToCompare = guardsToCompare.GetValueOrDefault(guardsFromSelf.Key);
            if (!guardsFromSelf.Value.DominatesGuard(currentGuardToCompare))
            {
                return false;
            }
        }
        return true;
    }

    public override String ToString()
    {
        String returnString = new String("Guards:");
        foreach (KeyValuePair<char, Guard> currentEntry in _guards)
        {
            returnString += (currentEntry.Key+" "+currentEntry.Value.ToString()+"\n");
        }
        return returnString;
    }

    public GuardCollection CreateIntersectionGuardCollection(GuardCollection guardsToCompoare)
    {
        GuardCollection returnGuardCollection = new GuardCollection();
        List<char> passedVariables = new List<char>();
        Guard newGuardCandidate;

        foreach (KeyValuePair<char,Guard> currentGuard in guardsToCompoare._guards)
        {       
            passedVariables.Add(currentGuard.Key);   
            if (_guards.ContainsKey(currentGuard.Key))
            {
                newGuardCandidate = currentGuard.Value.GuardIntersection(_guards[currentGuard.Key]);
                if (newGuardCandidate == Guard.NULLGUARD)
                {
                    return NULLGUARDCOLLECTION;
                }
                returnGuardCollection.addGuard(currentGuard.Key,newGuardCandidate);
            }
            else
            {
                newGuardCandidate = new Guard(currentGuard.Value);
                returnGuardCollection.addGuard(currentGuard.Key,newGuardCandidate);       
            }
        }
        foreach (KeyValuePair<char,Guard> currentGuard in _guards)
        {       
            if (passedVariables.Contains(currentGuard.Key) == false)
            {
                newGuardCandidate = new Guard(currentGuard.Value);
                returnGuardCollection.addGuard(currentGuard.Key,newGuardCandidate);  
            }  
        }
        return returnGuardCollection;
    }

    public Dictionary<char,Guard> getGuards()
    {
        return _guards;
    }
    public void addGuard(char variable,Guard guardToAdd)
    {
        _guards.Add(variable,guardToAdd);
    }


    
    public GuardCollection createPreDateGuardCollection(UpdateCollection updateToApplyBeforeThisGuard)
    {
        List<char> passedVariables = new List<char>();
        GuardCollection returnGuardCollection = new GuardCollection();
        foreach (KeyValuePair<char,Update> currentEntry in updateToApplyBeforeThisGuard._updates)
        {
            passedVariables.Add(currentEntry.Key);
            Update currentUpdate = currentEntry.Value;
            Guard newGuard = _guards[currentEntry.Key].CreatePreDateGuard(currentUpdate);
            if (newGuard == Guard.NULLGUARD)
            {
                return NULLGUARDCOLLECTION;            
            }
            returnGuardCollection.addGuard(currentEntry.Key,newGuard);
        }
        
        foreach (KeyValuePair<char,Guard> variable in _guards)
        {
            if (!(passedVariables.Contains(variable.Key)))
            {
                Guard newGuard = new Guard(_guards[variable.Key]._minValue,_guards[variable.Key]._maxValue);
                returnGuardCollection.addGuard(variable.Key , newGuard);
            }
        }

        return returnGuardCollection;

    }

}
