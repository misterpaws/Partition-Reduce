using System;
using System.Collections.Generic;
public class UpdateCollection
{

    internal Dictionary<char,Update> _updates = new Dictionary<char, Update>();
    public static PlusUpdate EMPTYUPDATE = new PlusUpdate('x',0);
    public static UpdateCollection NULLUPDATE = new UpdateCollection();


    public void AddUpdate(Update newUpdate)
    {
        _updates.Add(newUpdate.GetVariable(),newUpdate);
    }
    public Update GetValueOrDefault(char key)
        {
        Update ret;
        try
        {
            _updates.TryGetValue(key, out ret);
        }
        catch (System.Exception)
        {
            ret = EMPTYUPDATE;
        }
        return ret;
    }
    public Dictionary<char, Update> GetUpdates()
    {
        return _updates;
    }

    public UpdateCollection CreateNewUpdateCollectionByMerging(UpdateCollection successorUpdateCollection)
    {
        UpdateCollection returnUpdateCollection = new UpdateCollection();
        List<char> passedVariables = new List<char>();
        Update newUpdateCandidate;

        foreach (KeyValuePair<char,Update> currentEntry in successorUpdateCollection._updates)
        {   
            char currentVariable = currentEntry.Key;
            Update currentUpdate = currentEntry.Value;    
            passedVariables.Add(currentVariable);   
            if (_updates.ContainsKey(currentVariable))
            {
                newUpdateCandidate = currentEntry.Value.UpdateMerge(_updates[currentVariable]);
                returnUpdateCollection.AddUpdate(newUpdateCandidate);
            }
            else
            {
                if (currentUpdate is AssignUpdate)
                {
                    newUpdateCandidate = new AssignUpdate(currentVariable,currentUpdate.GetConstant());
                }
                else
                {
                    if (currentUpdate is PlusUpdate)
                    {
                        newUpdateCandidate = new PlusUpdate(currentVariable,currentUpdate.GetConstant());
                    }
                    else
                    {
                        newUpdateCandidate = new MinusUpdate(currentVariable,currentUpdate.GetConstant());
                    }
                }
                returnUpdateCollection.AddUpdate(newUpdateCandidate);       
            }
        }
        foreach (KeyValuePair<char,Update> currentEntry in _updates)
        {       
            if (passedVariables.Contains(currentEntry.Key) == false)
            {
                char currentVariable = currentEntry.Key;
                Update currentUpdate = currentEntry.Value;  
                if (currentUpdate is AssignUpdate)
                {
                    newUpdateCandidate = new AssignUpdate(currentVariable,currentUpdate.GetConstant());
                }
                else
                {
                    if (currentUpdate is PlusUpdate)
                    {
                        newUpdateCandidate = new PlusUpdate(currentVariable,currentUpdate.GetConstant());
                    }
                    else
                    {
                        newUpdateCandidate = new MinusUpdate(currentVariable,currentUpdate.GetConstant());
                    }
                }
                returnUpdateCollection.AddUpdate(newUpdateCandidate);  
            }  
        }
        return returnUpdateCollection;
    }

}
