extern alias v1Schemas;
extern alias v2Schemas;
extern alias v3Schemas;
using Newtonsoft.Json;
using Xunit;

namespace v3Models.Tests
{
    public class ProcessSchemaTests
    {
        [Fact]
        public void Should_construct_object_from_atrributes()
        {
            // Arrange
            var id = "id";
            var title = "title";
            var version = "v3";
            var owner = "owner";
            var purpose = "purpose";

            // Act
            var processSchema = new v3Schemas.Schemas.ProcessSchema(id, title, owner, purpose);

            // Assert
            Assert.True(processSchema.Id == id);
            Assert.True(processSchema.Title == title);
            Assert.True(processSchema.Version == version);
            Assert.True(processSchema.Owner == owner);
            Assert.True(processSchema.Purpose == purpose);

        }

        [Fact]
        public void Should_be_able_to_cast_object_back_and_forward()
        {
            // Arrange
            var processSchema = new v3Schemas.Schemas.ProcessSchema();

            // Act
            var str = processSchema.ToString();

            var createdProcessSchema = JsonConvert.DeserializeObject<v3Schemas.Schemas.ProcessSchema>(str);

            // Assert
            Assert.True(processSchema.Id == createdProcessSchema.Id);
            Assert.True(processSchema.Title == createdProcessSchema.Title);
            Assert.True(processSchema.Version == createdProcessSchema.Version);
            Assert.True(processSchema.Owner == createdProcessSchema.Owner);
            Assert.True(processSchema.Purpose == createdProcessSchema.Purpose);


        }

        [Fact]
        public void Should_construct_object_from_schema()
        {
            // Arrange
            var id = "id";
            var title = "title";
            var owner = "owner";
            var purpose = "Purpose";

            var processSchema = new v3Schemas.Schemas.ProcessSchema(id, title, owner, purpose);
            var str = JsonConvert.SerializeObject(processSchema);

            // Act 
            var schema = new v3Schemas.Schemas.ProcessSchema(str);

            // Assert
            Assert.True(processSchema.Id == id);
            Assert.True(processSchema.Title == title);
            Assert.True(processSchema.Owner == owner);
            Assert.True(processSchema.Purpose == purpose);
        }

        [Fact]
        public void Should_make_sure_version_is_different_if_schema_has_different_object()
        {
            // Arrange
            var id = "id";
            var title = "title";
            var owner = "owner";
            var purpose = "purpose";
            var processSchema = new v3Schemas.Schemas.ProcessSchema(id, title, owner, purpose);
            processSchema.Version = "xxx";
            var str = JsonConvert.SerializeObject(processSchema);


            // Act 
            var schema = new v2Schemas.Schemas.ProcessSchema(str);

            // Assert
            Assert.True(processSchema.Id == id);
            Assert.True(processSchema.Title == title);
            Assert.True(processSchema.Version == "xxx");
            Assert.True(processSchema.Owner == "owner");
            Assert.True(processSchema.Purpose == "purpose");
        }

        [Fact]
        public void Should_Build_Correctly_From_v1_schema()
        {
            // Arrange
            var v1Process = new v1Schemas.Schemas.ProcessSchema("id-1", "title-1");
            var v1ProcessString = JsonConvert.SerializeObject(v1Process);

            // Act
            var v3Process = new v3Schemas.Schemas.ProcessSchema(v1ProcessString);

            // Assert
            Assert.True(v3Process.Id == v1Process.Id);
            Assert.True(v3Process.Title == v1Process.Title);
            Assert.True(v3Process.Owner == "Default Owner added to V2");
            Assert.True(v3Process.Version == v1Process.Version);
            Assert.True(v3Process.Purpose == "Default Purpose added to V3");
        }

        [Fact]
        public void Should_Build_Correctly_From_v2_schema()
        {
            // Arrange
            var v2Processes = new v2Schemas.Schemas.ProcessSchema("id-2", "title-2", "owner-2");
            var v2ProcessesStrimg = JsonConvert.SerializeObject(v2Processes);

            // Act
            var v3Process = new v3Schemas.Schemas.ProcessSchema(v2ProcessesStrimg);

            // Assert
            Assert.True(v3Process.Id == v2Processes.Id);
            Assert.True(v3Process.Title == v2Processes.Title);
            Assert.True(v3Process.Owner == v2Processes.Owner);
            Assert.True(v3Process.Version == v2Processes.Version);
            Assert.True(v3Process.Purpose == "Default Purpose added to V3");
        }




    }
}
