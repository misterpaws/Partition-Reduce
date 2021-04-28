using System;
using System.Collections;
using System.Collections.Generic;
public class ConditionalSubscriberComparer: IComparer<ConditionalSubscriber>
{
    public int Compare(ConditionalSubscriber x, ConditionalSubscriber y)
    {
        if (x.GetPriority() == (y.GetPriority()))
        {
            return 0;
        }
        else if (x.GetPriority() > (y.GetPriority()))
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }
}