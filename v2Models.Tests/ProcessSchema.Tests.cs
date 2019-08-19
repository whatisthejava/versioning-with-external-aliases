extern alias v1Schemas;
extern alias v2Schemas;
using Newtonsoft.Json;
using v1Schemas::Schemas;
using Xunit;

namespace v2Models.Tests
{
    public class ProcessSchemaTests
    {
        [Fact]
        public void Should_construct_object_from_atrributes()
        {
            // Arrange
            var id = "id";
            var title = "title";
            var version = "v2";
            var owner = "owner";

            // Act
            var processSchema = new v2Schemas.Schemas.ProcessSchema(id, title, owner);

            // Assert
            Assert.True(processSchema.Id == id);
            Assert.True(processSchema.Title == title);
            Assert.True(processSchema.Version == version);
            Assert.True(processSchema.Owner == owner);

        }

        [Fact]
        public void Should_be_able_to_cast_object_back_and_forward()
        {
            // Arrange
            var processSchema = new v2Schemas.Schemas.ProcessSchema();

            // Act
            var str = processSchema.ToString();

            var createdProcessSchema = JsonConvert.DeserializeObject<v2Schemas.Schemas.ProcessSchema>(str);

            // Assert
            Assert.True(processSchema.Id == createdProcessSchema.Id);
            Assert.True(processSchema.Title == createdProcessSchema.Title);
            Assert.True(processSchema.Version == createdProcessSchema.Version);
            Assert.True(processSchema.Owner == createdProcessSchema.Owner);

        }

        [Fact]
        public void Should_construct_object_from_schema()
        {
            // Arrange
            var id = "id";
            var title = "title";
            var owner = "owner";

            var processSchema = new v2Schemas.Schemas.ProcessSchema(id, title, owner);
            var str = JsonConvert.SerializeObject(processSchema);

            // Act 
            var schema = new v2Schemas.Schemas.ProcessSchema(str);

            // Assert
            Assert.True(processSchema.Id == id);
            Assert.True(processSchema.Title == title);        
            Assert.True(processSchema.Owner== owner);
        }

        [Fact]
        public void Should_make_sure_version_is_different_if_schema_has_different_object()
        {
            // Arrange
            var id = "id";
            var title = "title";
            var owner = "owner";
            var processSchema = new v2Schemas.Schemas.ProcessSchema(id, title, owner);
            processSchema.Version = "xxx";
            var str = JsonConvert.SerializeObject(processSchema);


            // Act 
            var schema = new v2Schemas.Schemas.ProcessSchema(str);

            // Assert
            Assert.True(processSchema.Id == id);
            Assert.True(processSchema.Title == title);
            Assert.True(processSchema.Version == "xxx");
            Assert.True(processSchema.Owner == "owner");
        }

        [Fact]
        public void Should_Build_Correctly_From_Previously_created_Schema()
        {
            // Arrange
            var v1Process = new v1Schemas.Schemas.ProcessSchema("id-1", "title-1");
            var v1ProcessString = JsonConvert.SerializeObject(v1Process);

            // Act
            var v2Process = new v2Schemas.Schemas.ProcessSchema(v1ProcessString);

            // Assert
            Assert.True(v2Process.Id == v1Process.Id);
            Assert.True(v2Process.Title == v1Process.Title);
            Assert.True(v2Process.Owner == "Default Owner added to V2");
            Assert.True(v2Process.Version == v1Process.Version);
        }




    }
}
