using System;

public class JumpEdge : Edge
{
    private Partition _sourcePartition;
    private Partition _destinationPartition;

    public JumpEdge(Location newsource, Location newdestination, Partition source, Partition destination) : base(newsource, newdestination)
    {
        _sourcePartition = source;
        _destinationPartition = destination;
    }
}