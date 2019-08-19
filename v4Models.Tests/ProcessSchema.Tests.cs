extern alias v1Schemas;
extern alias v2Schemas;
extern alias v3Schemas;
extern alias v4Schemas;
using Newtonsoft.Json;
using System.Collections.Generic;
using v4Schemas::Schemas;
using Xunit;

namespace v4Models.Tests
{
    public class ProcessSchemaTests
    {
        [Fact]
        public void Should_construct_object_from_atrributes()
        {
            // Arrange
            var id = "id";
            var title = "title";
            var version = "v4";
            var owner = "owner";
            var purpose = "purpose";

            var actors = new List<Actor>();

            // Act
            var processSchema = new v4Schemas.Schemas.ProcessSchema(id, title, owner, purpose);
            processSchema.Actors = actors;

            // Assert
            Assert.True(processSchema.Id == id);
            Assert.True(processSchema.Title == title);
            Assert.True(processSchema.Version == version);
            Assert.True(processSchema.Owner == owner);
            Assert.True(processSchema.Purpose == purpose);
            Assert.True(processSchema.Actors.Count == actors.Count);

        }

        [Fact]
        public void Should_be_able_to_cast_object_back_and_forward()
        {
            // Arrange
            var processSchema = new v4Schemas.Schemas.ProcessSchema();

            // Act
            var str = processSchema.ToString();

            var createdProcessSchema = JsonConvert.DeserializeObject<v4Schemas.Schemas.ProcessSchema>(str);

            // Assert
            Assert.True(processSchema.Id == createdProcessSchema.Id);
            Assert.True(processSchema.Title == createdProcessSchema.Title);
            Assert.True(processSchema.Version == createdProcessSchema.Version);
            Assert.True(processSchema.Owner == createdProcessSchema.Owner);
            Assert.True(processSchema.Purpose == createdProcessSchema.Purpose);
            Assert.True(processSchema.Actors.Count == createdProcessSchema.Actors.Count);


        }

        [Fact]
        public void Should_construct_object_from_schema()
        {
            // Arrange
            var id = "id";
            var title = "title";
            var owner = "owner";
            var purpose = "Purpose";
            var actors = new List<Actor>();


            var processSchema = new v4Schemas.Schemas.ProcessSchema(id, title, owner, purpose);
            processSchema.Actors = actors;

            var str = JsonConvert.SerializeObject(processSchema);

            // Act 
            var schema = new v4Schemas.Schemas.ProcessSchema(str);

            // Assert
            Assert.True(processSchema.Id == id);
            Assert.True(processSchema.Title == title);
            Assert.True(processSchema.Owner == owner);
            Assert.True(processSchema.Purpose == purpose);
            Assert.True(processSchema.Actors.Count == actors.Count);

        }

        [Fact]
        public void Should_make_sure_version_is_different_if_schema_has_different_object()
        {
            // Arrange
            var id = "id";
            var title = "title";
            var owner = "owner";
            var purpose = "purpose";
            var actors = new List<Actor>();


            var processSchema = new v4Schemas.Schemas.ProcessSchema(id, title, owner, purpose);
            processSchema.Actors = actors;
            processSchema.Version = "xxx";
            var str = JsonConvert.SerializeObject(processSchema);

            
            // Act 
            var schema = new v2Schemas.Schemas.ProcessSchema(str);

            // Assert
            Assert.True(processSchema.Id == id);
            Assert.True(processSchema.Title == title);
            Assert.True(processSchema.Version == "xxx");
            Assert.True(processSchema.Owner == owner);
            Assert.True(processSchema.Purpose == purpose);
            Assert.True(processSchema.Actors.Count == actors.Count);

        }

        [Fact]
        public void Should_Build_Correctly_From_v1_schema()
        {
            // Arrange
            var v1Process = new v1Schemas.Schemas.ProcessSchema("id-1", "title-1");
            var v1ProcessString = JsonConvert.SerializeObject(v1Process);

            // Act
            var v4Process = new v4Schemas.Schemas.ProcessSchema(v1ProcessString);

            // Assert
            Assert.True(v4Process.Id == v1Process.Id);
            Assert.True(v4Process.Title == v1Process.Title);
            Assert.True(v4Process.Owner == "Default Owner added to V2");
            Assert.True(v4Process.Version == v1Process.Version);
            Assert.True(v4Process.Purpose == "Default Purpose added to V3");
            Assert.True(v4Process.Actors.Count == 0);
        }

        [Fact]
        public void Should_Build_Correctly_From_v2_schema()
        {
            // Arrange
            var v2Processes = new v2Schemas.Schemas.ProcessSchema("id-2", "title-2", "owner-2");
            var v2ProcessesStrimg = JsonConvert.SerializeObject(v2Processes);

            // Act
            var v4Process = new v4Schemas.Schemas.ProcessSchema(v2ProcessesStrimg);

            // Assert
            Assert.True(v4Process.Id == v2Processes.Id);
            Assert.True(v4Process.Title == v2Processes.Title);
            Assert.True(v4Process.Owner == v2Processes.Owner);
            Assert.True(v4Process.Version == v2Processes.Version);
            Assert.True(v4Process.Purpose == "Default Purpose added to V3");
            Assert.True(v4Process.Actors.Count == 0);

        }

        [Fact]
        public void Should_Build_Correctly_From_v3_schema()
        {
            // Arrange
            var v3Processes = new v3Schemas.Schemas.ProcessSchema("id-3", "title-3", "owner-3", "purpose-3");
            var v3ProcessesStrimg = JsonConvert.SerializeObject(v3Processes);

            // Act
            var v4Process = new v4Schemas.Schemas.ProcessSchema(v3ProcessesStrimg);

            // Assert
            Assert.True(v4Process.Id == v3Processes.Id);
            Assert.True(v4Process.Title == v3Processes.Title);
            Assert.True(v4Process.Owner == v3Processes.Owner);
            Assert.True(v4Process.Version == v3Processes.Version);
            Assert.True(v4Process.Purpose == v3Processes.Purpose);
            Assert.True(v4Process.Actors.Count == 0);

        }


        // Lists
        [Fact]
        public void Should_make_sure_list_is_created_correctly()
        {
            // Arrange
            
            var actors = new List<Actor>();
            actors.Add(new Actor() { Age = "11", Name = "Roddy" });
            actors.Add(new Actor() { Age = "8", Name = "Lawson" });

            // Act
            var processSchema = new v4Schemas.Schemas.ProcessSchema("", "", "", "");
            processSchema.Actors = actors;

            // Assert
            Assert.True(processSchema.Actors.Count == 2);
            Assert.True(processSchema.Actors[0].Name == "Roddy");
            Assert.True(processSchema.Actors[1].Name == "Lawson");
            Assert.True(processSchema.Actors[0].Age == "11");
            Assert.True(processSchema.Actors[1].Age == "8");

        }




    }
}
