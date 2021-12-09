using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataDogLoggingDemo
{
    public class StructuredObject
    {
        public string StringValue { get; set; } = "string value 1";
        public string AnotherStringValue { get; set; } = "string value 2";

        //public int[] SomeNumbers { get; set; } = new int[] { 3, 2, 7, 4, 99, 102, 42, 54 };

        //public SubObject SubObect { get; set; } = new();

        //public Dictionary<string, SubObject> listOfSubObjects { get; set; } = new Dictionary<string, SubObject>()
        //{
        //    {"1",  new SubObject() },
        //    {"2",  new SubObject() },
        //    {"3",  new SubObject() }
        //};
    }

    public class SubObject
    {
        public int NumberOfThings { get; set; } = 3;
        public decimal PercentageOfTotal { get; set; } = 12.345m;

        public string[] ListOfThings { get; set; } = new string[] { "thing1", "thing2", "thing3" };
    }

}
