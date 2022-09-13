using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazored.LocalStorage.Tests.TestAssets
{
    public class TestComplexObject
    {
        public Dictionary<string, List<Dict1Object>> Dict1 { get; set; } = new LocalStorageServiceTests.FillComplexObject().FillDict1();
        public Dictionary<string, List<List<bool>>> Dict2 { get; set; } = new LocalStorageServiceTests.FillComplexObject().FillDict2();
        public Dictionary<string, List<List<bool>>> Dict3 { get; set; } = new LocalStorageServiceTests.FillComplexObject().FillDict2();
        //public Dictionary<string, List<List<bool>>> Dict3 { get; set; } = new();
        public List<int> IntList { get; set; } = new();
        public decimal? Decimal1 { get; set; }
        public decimal? Decimal2 { get; set; }
        public decimal? Decimal3 { get; set; }
        public decimal? Decimal4 { get; set; }
        public decimal? Decimal5 { get; set; }
        public bool Bool1 { get; set; }
        public string String1 { get; set; } = "";

        public TestComplexObject() { }
        public TestComplexObject(
            Dictionary<string, List<Dict1Object>> dict1,
            Dictionary<string, List<List<bool>>> dict2,
            Dictionary<string, List<List<bool>>> dict3,
            List<int> intList,
            decimal? decimal1,
            decimal? decimal2,
            decimal? decimal3,
            decimal? decimal4,
            decimal? decimal5,
            bool bool1,
            string string1
        )
        {
            Dict1 = dict1;
            Dict2 = dict2;
            Dict3 = dict3;
            IntList = intList;
            Decimal1 = decimal1;
            Decimal2 = decimal2;
            Decimal3 = decimal3;
            Decimal4 = decimal4;
            Decimal5 = decimal5;
            Bool1 = bool1;
            String1 = string1;
        }
    }

    public class Dict1Object
    {
        public decimal? Dict1ObjDec1 { get; set; }
        public decimal? Dict1ObjDec2 { get; set; }
    }
}
