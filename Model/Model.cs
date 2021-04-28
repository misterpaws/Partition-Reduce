using System;
using System.Collections.Generic;
public class Model
{
    private List<Location> _locations = new List<Location>();
    private List<Location> _criticalLocations = new List<Location>();
    private Location _startLocation;
    private List<Edge> _edges = new List<Edge>();
    public readonly List<char> _variables = new List<char>();
    public Model(List<Location> newLocations, Location start, List<Edge> newEdges)
    {
        this._locations = newLocations;
        this._startLocation = start;
        this._edges = newEdges;
    }
    public Location GetInitialLocation()
    {
        return _startLocation;
    }
    public List<Location> GetLocations()
    {
        return _locations;
    }
    public void AddLocationAsCritical(Location criticalLocation)
    {
        _criticalLocations.Add(criticalLocation);
    }
    public bool IsCriticalLocation(Location locationToCheck)
    {
        if (_criticalLocations.Contains(locationToCheck))
        {
            return true;
        }
        return false;
    }
    public void PrintModel()
    {
        Console.WriteLine("Initial "+_startLocation.ToString());
        int final = _locations.Count;
        for (int i = 0; i < final; i++)
        {
            Console.WriteLine(this._locations[i].ToString());
        }
    } 


    public void Reduce(Edge edgeToRemove)
    {
        Location destination = edgeToRemove.GetDestination();
        Location origin = edgeToRemove.GetSource();


    }
    public List<Edge> GetEdges()
    {
        return _edges;
    }
    public static Model CreateTestModelA()
    {
        Console.WriteLine("Creating test model A");
        //Small basic model with no variables

        Location l1 = new Location(1);    
        Location l2 = new Location(2);    
        Location l3 = new Location(3);    
        Location l4 = new Location(4); 
        Location l5 = new Location(5);    
        Location l6 = new Location(6);      
        Edge e1 = new Edge(l1,l2);
        Edge e2 = new Edge(l2,l3);
        Edge e3 = new Edge(l3,l4);
        Edge e4 = new Edge(l3,l5);
        Edge e5 = new Edge(l5,l1);
        Edge e6 = new Edge(l5,l6);
        Edge e7 = new Edge(l6,l3);
        Edge e8 = new Edge(l6,l6);
        Edge e9 = new Edge(l1,l4);

        List<Location> locationList = new List<Location>();
        List<Edge> edgeList = new List<Edge>();

        locationList.Add(l1);
        locationList.Add(l2);
        locationList.Add(l3);
        locationList.Add(l4);
        locationList.Add(l5);
        locationList.Add(l6);
        edgeList.Add(e1);
        edgeList.Add(e2);
        edgeList.Add(e3);
        edgeList.Add(e4);
        edgeList.Add(e5);
        edgeList.Add(e6);
        edgeList.Add(e7);
        edgeList.Add(e8);
        edgeList.Add(e9);

        Model testModel = new Model(locationList,l1,edgeList);

        return testModel;
    }

        public static Model CreateTestModelB()
    {
        Console.WriteLine("Creating test model B");
        //looping test model with variables and guards

        Location l1 = new Location(1);    
        Location l2 = new Location(2);    
        Location l3 = new Location(3);      
        Edge e1 = new Edge(l1,l1);
        Edge e2 = new Edge(l1,l2);
        Edge e3 = new Edge(l2,l2);
        Edge e4 = new Edge(l2,l2);
        Edge e5 = new Edge(l2,l3);
        UpdateCollection e1collection = new UpdateCollection();
        e1collection.AddUpdate(new PlusUpdate('x',1));
        e1.SetUpdate(e1collection);
        UpdateCollection e3collection = new UpdateCollection();
        e3collection.AddUpdate(new PlusUpdate('x',1));
        e3.SetUpdate(e3collection);
        UpdateCollection e4collection = new UpdateCollection();
        e4collection.AddUpdate(new MinusUpdate('x',1));
        e4.SetUpdate(e4collection);
        
        GuardCollection e2Guards = new GuardCollection();
        e2Guards.addGuard('x',new Guard(10,Program.VARIABLE_MAX));
        e2.SetGuard(e2Guards);

        GuardCollection e5Guards = new GuardCollection();
        e5Guards.addGuard('x',new Guard(Program.VARIABLE_MIN,3));
        e5.SetGuard(e5Guards);

        List<Location> locationList = new List<Location>();
        List<Edge> edgeList = new List<Edge>();

        locationList.Add(l1);
        locationList.Add(l2);
        locationList.Add(l3);

        edgeList.Add(e1);
        edgeList.Add(e2);
        edgeList.Add(e3);
        edgeList.Add(e4);
        edgeList.Add(e5);

        Model testModel = new Model(locationList,l1,edgeList);

        return testModel;
    }

            public static Model CreateTestModelC()
    {
        Console.WriteLine("Creating test model C");
        //first test model for path and deadend reductions

        Location l1 = new Location(1);    
        Location l2 = new Location(2);    
        Location l3 = new Location(3);
        Location l4 = new Location(4);   
        Location l5 = new Location(5);   
        Location l6 = new Location(6);   
        Location l7 = new Location(7);   
        Location l8 = new Location(8);   
        Location l9 = new Location(9);   
        Location l10 = new Location(10);    
        Location l11 = new Location(11);      
        Edge e1 = new Edge(l1,l2);
        Edge e2 = new Edge(l1,l4);
        Edge e3 = new Edge(l1,l3);
        Edge e4 = new Edge(l2,l5);
        Edge e5 = new Edge(l5,l6);
        Edge e6 = new Edge(l6,l7);
        Edge e7 = new Edge(l6,l8);
        Edge e8 = new Edge(l8,l9);
        Edge e9 = new Edge(l6,l10);
        Edge e10 = new Edge(l10,l1);
        Edge e11 = new Edge(l11,l4);

        List<Location> locationList = new List<Location>();
        List<Edge> edgeList = new List<Edge>();

        locationList.Add(l1);
        locationList.Add(l2);
        locationList.Add(l3);
        locationList.Add(l4);
        locationList.Add(l5);
        locationList.Add(l6);
        locationList.Add(l7);
        locationList.Add(l8);
        locationList.Add(l9);
        locationList.Add(l10);
        locationList.Add(l11);

        edgeList.Add(e1);
        edgeList.Add(e2);
        edgeList.Add(e3);
        edgeList.Add(e4);
        edgeList.Add(e5);
        edgeList.Add(e6);
        edgeList.Add(e7);
        edgeList.Add(e8);
        edgeList.Add(e9);
        edgeList.Add(e10);
        edgeList.Add(e11);

        Model testModel = new Model(locationList,l1,edgeList);

        return testModel;
    }
}

