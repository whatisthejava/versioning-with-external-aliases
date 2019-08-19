using Newtonsoft.Json;
using Schemas;
using System;
using Xunit;

namespace v1Models.Tests
{
    public class ProcessSchemaTests
    {
        [Fact]
        public void Should_construct_object_from_atrributes()
        {
            // Arrange
            var id = "id";
            var title = "title";
            var version = "v1";

            // Act
            var processSchema = new ProcessSchema(id, title);

            // Assert
            Assert.True(processSchema.Id == id);
            Assert.True(processSchema.Title == title);
            Assert.True(processSchema.Version == version);

        }

        [Fact]
        public void Should_be_able_to_cast_object_back_and_forward()
        {
            // Arrange
            var processSchema = new ProcessSchema();

            // Act
            var str = processSchema.ToString();

            var createdProcessSchema = JsonConvert.DeserializeObject<ProcessSchema>(str);

            // Assert
            Assert.True(processSchema.Id == createdProcessSchema.Id);
            Assert.True(processSchema.Title == createdProcessSchema.Title);
            Assert.True(processSchema.Version == createdProcessSchema.Version);

        }

        [Fact]
        public void Should_construct_object_from_schema()
        {
            // Arrange
            var id = "id";
            var title = "title";
            var processSchema = new ProcessSchema(id, title);
            var str = JsonConvert.SerializeObject(processSchema);

            // Act 
            var schema = new ProcessSchema(str);

            // Assert
            Assert.True(processSchema.Id == id);
            Assert.True(processSchema.Title == title);
        }

        [Fact]
        public void Should_make_sure_version_is_different_if_schema_has_different_object()
        {
            // Arrange
            var id = "id";
            var title = "title";
            var processSchema = new ProcessSchema(id, title);
            processSchema.Version = "xxx";
            var str = JsonConvert.SerializeObject(processSchema);


            // Act 
            var schema = new ProcessSchema(str);

            // Assert
            Assert.True(processSchema.Id == id);
            Assert.True(processSchema.Title == title);
            Assert.True(processSchema.Version == "xxx");
        }




    }
}
