using System;
using System.Collections.Generic;


    class Program
    {
        enum ExpComparator 
    {
        GreaterOrEqual,
        Equal,
        LessOrEqual
    }
            public const int VARIABLE_MAX = 255;
            public const int VARIABLE_MIN = 0;

        static void Main(string[] args)
        {
           Model testModel = Model.CreateTestModelC();




            testModel.PrintModel();

            Console.WriteLine("Verification Starting");

            StateCluster goal = new StateCluster(testModel.GetLocations().Find(x => (x.GetID() == 10)), new GuardCollection());
            Query testQuery = new Query(goal);

            Console.WriteLine("Query : "+goal.ToString());

            //Verifyer.INSTANCE.SetModel(testModel);
            //Verifyer.INSTANCE.SetQuery(query);
            StateCluster initialState = new StateCluster(testModel.GetInitialLocation(),testModel.GetInitialLocation().GetInvariant());
            Verifyer testVerifier = new Verifyer(testModel);
            
            bool result = testVerifier.Verify(testQuery);
            Console.WriteLine(result.ToString());
            

            Console.WriteLine("Verification Complete");
        }

    }
    

