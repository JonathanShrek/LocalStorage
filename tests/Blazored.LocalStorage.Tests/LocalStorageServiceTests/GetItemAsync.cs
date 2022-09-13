using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage.JsonConverters;
using Blazored.LocalStorage.Serialization;
using Blazored.LocalStorage.StorageOptions;
using Blazored.LocalStorage.TestExtensions;
using Blazored.LocalStorage.Tests.TestAssets;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Blazored.LocalStorage.Tests.LocalStorageServiceTests
{
    public class GetItemAsync
    {
        private readonly LocalStorageService _sut;
        private readonly IStorageProvider _storageProvider;
        private readonly IJsonSerializer _serializer;

        private const string Key = "testKey";

        public GetItemAsync()
        {
            var mockOptions = new Mock<IOptions<LocalStorageOptions>>();
            var jsonOptions = new JsonSerializerOptions();
            jsonOptions.Converters.Add(new TimespanJsonConverter());
            mockOptions.Setup(u => u.Value).Returns(new LocalStorageOptions());
            _serializer = new SystemTextJsonSerializer(mockOptions.Object);
            _storageProvider = new InMemoryStorageProvider();
            _sut = new LocalStorageService(_storageProvider, _serializer);
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void ThrowsArgumentNullException_When_KeyIsInvalid(string key)
        {
            // arrange / act
            var action = new Func<Task>(async () => await _sut.GetItemAsync<object>(key));

            // assert
            Assert.ThrowsAsync<ArgumentNullException>(action);
        }
        
        [Theory]
        [InlineData("Item1", "stringTest")]
        [InlineData("Item2", 11)]
        [InlineData("Item3", 11.11)]
        public async Task ReturnsDeserializedDataFromStore<T>(string key, T data)
        {
            // Arrange
            await _sut.SetItemAsync(key, data);
            
            // Act
            var result = await _sut.GetItemAsync<T>(key);

            // Assert
            Assert.Equal(data, result);
        }

        [Fact]
        public async Task ReturnsComplexObjectFromStore()
        {
            // Arrange
            var objectToSave = new TestObject(2, "Jane Smith");
            await _sut.SetItemAsync(Key, objectToSave);

            // Act
            var result = await _sut.GetItemAsync<TestObject>(Key);

            // Assert
            Assert.Equal(objectToSave.Id, result.Id);
            Assert.Equal(objectToSave.Name, result.Name);
        }

        [Fact]
        public async Task ReturnsComplexObject2FromStore()
        {
            var dict1Data = new FillComplexObject().FillDict1();
            var dict2Data = new FillComplexObject().FillDict2();
            var dict3Data = new FillComplexObject().FillDict2();
            var intList = new List<int>();
            decimal? decimal1 = null;
            decimal? decimal2 = null;
            decimal? decimal3 = null;
            decimal? decimal4 = null;
            decimal? decimal5 = null;
            var bool1 = false;
            var string1 = "";

        // Arrange
        var objectToSave = new TestComplexObject(
            dict1Data,
            dict2Data,
            dict3Data,
            intList,
            decimal1,
            decimal2,
            decimal3,
            decimal4,
            decimal5,
            bool1,
            string1
        );
            await _sut.SetItemAsync(Key, objectToSave);

            // Act
            var result = await _sut.GetItemAsync<TestComplexObject>(Key);

            var dict1CountEqual = objectToSave.Dict1.Count == result.Dict1.Count;
            var dict2CountEqual = objectToSave.Dict2.Count == result.Dict2.Count;
            var dict3CountEqual = objectToSave.Dict3.Count == result.Dict3.Count;

            var dict1KeyEqual = false;
            foreach (var k1 in objectToSave.Dict1)
            {
                foreach (var k2 in result.Dict1)
                {
                    if (k1.Key == k2.Key)
                    {
                        dict1KeyEqual = true;
                    }
                }
            }

            var dict2KeyEqual = false;
            foreach (var k1 in objectToSave.Dict2)
            {
                foreach (var k2 in result.Dict2)
                {
                    if (k1.Key == k2.Key)
                    {
                        dict2KeyEqual = true;
                    }
                }
            }

            var dict3KeyEqual = false;
            foreach (var k1 in objectToSave.Dict3)
            {
                foreach (var k2 in result.Dict3)
                {
                    if (k1.Key == k2.Key)
                    {
                        dict3KeyEqual = true;
                    }
                }
            }

            var dict1ValuesEqual = false;
            foreach (var v1 in objectToSave.Dict1)
            {
                foreach (var v2 in result.Dict1)
                {
                    foreach (var values1 in v1.Value)
                    {
                        foreach (var values2 in v2.Value)
                        {
                            if (values1.Dict1ObjDec1 == values2.Dict1ObjDec1 &&
                                values1.Dict1ObjDec2 == values2.Dict1ObjDec2)
                            {
                                dict1ValuesEqual = true;
                            }
                        }
                    }
                }
            }

            var dict2ValuesEqual = true;
            foreach (var v1 in objectToSave.Dict2)
            {
                foreach (var v2 in result.Dict2)
                {
                    foreach (var values1 in v1.Value)
                    {
                        foreach (var values2 in v2.Value)
                        {
                            foreach (var item1 in values1)
                            {
                                foreach (var item2 in values2)
                                {
                                    if (item1 != item2)
                                    {
                                        dict2ValuesEqual = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            var dict3ValuesEqual = true;
            foreach (var v1 in objectToSave.Dict3)
            {
                foreach (var v2 in result.Dict3)
                {
                    foreach (var values1 in v1.Value)
                    {
                        foreach (var values2 in v2.Value)
                        {
                            foreach (var item1 in values1)
                            {
                                foreach (var item2 in values2)
                                {
                                    if (item1 != item2)
                                    {
                                        dict3ValuesEqual = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            var areDicts1Equal = dict1CountEqual && dict1KeyEqual && dict1ValuesEqual;
            var areDicts2Equal = dict2CountEqual && dict2KeyEqual && dict2ValuesEqual;
            var areDicts3Equal = dict3CountEqual && dict3KeyEqual && dict3ValuesEqual;

            // Assert
            Assert.True(areDicts1Equal);
            Assert.True(areDicts2Equal);
            Assert.True(areDicts3Equal);

            Assert.Equal(objectToSave.IntList, result.IntList);
            Assert.Equal(objectToSave.Decimal1, result.Decimal1);
            Assert.Equal(objectToSave.Decimal2, result.Decimal2);
            Assert.Equal(objectToSave.Decimal3, result.Decimal3);
            Assert.Equal(objectToSave.Decimal4, result.Decimal4);
            Assert.Equal(objectToSave.Decimal5, result.Decimal5);
            Assert.Equal(objectToSave.Bool1, result.Bool1);
            Assert.Equal(objectToSave.String1, result.String1);
        }
        
        [Fact]
        public async Task ReturnsNullFromStore_When_NullValueSaved()
        {
            // Arrange
            var valueToSave = (string)null;
            await _sut.SetItemAsync(Key, valueToSave);

            // Act
            var result = await _sut.GetItemAsync<string>(Key);

            // Assert
            Assert.Null(result);
        }
        
        [Fact]
        public async Task ReturnsStringFromStore_When_JsonExceptionIsThrown()
        {
            // Arrange
            var valueToSave = "[{ id: 5, name: \"Jane Smith\"}]";
            await _storageProvider.SetItemAsync(Key, valueToSave);

            // Act
            var result = await _sut.GetItemAsync<string>(Key);

            // Assert
            Assert.Equal(valueToSave, result);
        }
    }
}
