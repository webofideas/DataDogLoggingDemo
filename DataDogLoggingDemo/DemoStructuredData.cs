using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataDogLoggingDemo
{
    public class StructuredObject
    {
        public static StructuredObject GetRandom()
        {
            var ret = new StructuredObject();
            ret.StringValue = $"string value {GetRandom1To15()}";
            ret.AnotherStringValue = $"string value {GetRandom1To15()}";

            var size = GetRandom1To15();
            ret.SomeNumbers = new int[size];
            for (int i=0; i<size; ++i)
            {
                ret.SomeNumbers[i] = GetRandom1To15();
            }

            size = GetRandom1To5();
            ret.ListOfSubObjects = new Dictionary<string, SubObject>();
            for (int i = 0; i < size; ++i)
            {
                ret.ListOfSubObjects.Add($"{i+1}",new SubObject());
            }

            return ret;
        }


        public string StringValue { get; set; } = "string value 1";
        public string AnotherStringValue { get; set; } = "string value 2";

        public int[] SomeNumbers { get; set; } = new int[] { 3, 2, 7, 4, 99, 102, 42, 54 };

        public SubObject SubObect { get; set; } = new();

        public Dictionary<string, SubObject> ListOfSubObjects { get; set; } = new Dictionary<string, SubObject>()
        {
            {"1",  new SubObject() },
            {"2",  new SubObject() },
            {"3",  new SubObject() }
        };

        private static Random rand = new Random();
        private static int GetRandom1To15()
        {
            return StructuredObject.rand.Next(0, 15);
        }
        private static int GetRandom1To5()
        {
            return StructuredObject.rand.Next(0, 5);
        }
    }

    public class SubObject
    {
        public int NumberOfThings { get; set; } = 3;
        public decimal PercentageOfTotal { get; set; } = 12.345m;

        public string[] ListOfThings { get; set; } = new string[] { "thing1", "thing2", "thing3" };
    }

}
