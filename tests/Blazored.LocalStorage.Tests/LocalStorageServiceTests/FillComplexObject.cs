using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazored.LocalStorage.Tests.TestAssets;

namespace Blazored.LocalStorage.Tests.LocalStorageServiceTests
{
    internal class FillComplexObject
    {
        public Dictionary<string, List<Dict1Object>> FillDict1()
        {
            var rows = new List<string>()
            {
                "ROW1",
                "ROW2",
                "ROW3",
                "ROW4",
                "ROW5",
                "ROW6",
                "ROW7",
                "ROW8",
                "ROW9",
            };

            var returnDict = new Dictionary<string, List<Dict1Object>>();

            const int NUMBER_OF_COLS = 3;

            foreach (var row in rows)
            {
                returnDict.Add(row, new List<Dict1Object>());

                for (int i = 0; i < NUMBER_OF_COLS; i++)
                {
                    returnDict[row].Add(new Dict1Object()
                    {
                        Dict1ObjDec1 = null,
                        Dict1ObjDec2 = null,
                    }); ;
                }
            }

            return returnDict;
        }

        public Dictionary<string, List<List<bool>>> FillDict2()
        {
            var rows = new List<string>()
            {
                "ROW1",
                "ROW2",
                "ROW3",
                "ROW4",
                "ROW5",
                "ROW6",
                "ROW7",
                "ROW8",
                "ROW9",
            };

            const int NUMBER_OF_COLS = 4;

            var defaultBools = new List<bool>() { false, false, false };

            var returnDict = new Dictionary<string, List<List<bool>>>();

            foreach (var row in rows)
            {
                returnDict.Add(row, new List<List<bool>>());

                for (int i = 0; i < NUMBER_OF_COLS; i++)
                {
                    returnDict[row].Add(defaultBools);
                }
            }

            return returnDict;
        }
    }
}
