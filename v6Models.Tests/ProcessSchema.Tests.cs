extern alias v1Schemas;
extern alias v2Schemas;
extern alias v3Schemas;
extern alias v4Schemas;
extern alias v5Schemas;
extern alias v6Schemas;
using Newtonsoft.Json;
using System.Collections.Generic;
using Xunit;

namespace v6Models.Tests
{
    public class ProcessSchemaTests
    {
        [Fact]
        public void Should_construct_object_from_atrributes()
        {
            // Arrange
            var id = "id";
            var title = "title";
            var version = "v6";
            var owner = "owner";
            var purpose = "purpose";

            var actors = new List<v6Schemas.Schemas.Actor>();

            // Act
            var processSchema = new v6Schemas.Schemas.ProcessSchema(id, title, owner, purpose);
            processSchema.ActorCollection = actors;

            // Assert
            Assert.True(processSchema.Id == id);
            Assert.True(processSchema.Title == title);
            Assert.True(processSchema.Version == version);
            Assert.True(processSchema.Owner == owner);
            Assert.True(processSchema.Purpose == purpose);
            Assert.True(processSchema.ActorCollection.Count == actors.Count);

        }

        [Fact]
        public void Should_be_able_to_cast_object_back_and_forward()
        {
            // Arrange
            var processSchema = new v6Schemas.Schemas.ProcessSchema();

            // Act
            var str = processSchema.ToString();

            var createdProcessSchema = JsonConvert.DeserializeObject<v6Schemas.Schemas.ProcessSchema>(str);

            // Assert
            Assert.True(processSchema.Id == createdProcessSchema.Id);
            Assert.True(processSchema.Title == createdProcessSchema.Title);
            Assert.True(processSchema.Version == createdProcessSchema.Version);
            Assert.True(processSchema.Owner == createdProcessSchema.Owner);
            Assert.True(processSchema.Purpose == createdProcessSchema.Purpose);
            Assert.True(processSchema.ActorCollection.Count == createdProcessSchema.ActorCollection.Count);


        }

        [Fact]
        public void Should_construct_object_from_schema()
        {
            // Arrange
            var id = "id";
            var title = "title";
            var owner = "owner";
            var purpose = "Purpose";
            var actors = new List<v6Schemas.Schemas.Actor>();


            var processSchema = new v6Schemas.Schemas.ProcessSchema(id, title, owner, purpose);
            processSchema.ActorCollection = actors;

            var str = JsonConvert.SerializeObject(processSchema);

            // Act 
            var schema = new v6Schemas.Schemas.ProcessSchema(str);

            // Assert
            Assert.True(processSchema.Id == id);
            Assert.True(processSchema.Title == title);
            Assert.True(processSchema.Owner == owner);
            Assert.True(processSchema.Purpose == purpose);
            Assert.True(processSchema.ActorCollection.Count == actors.Count);

        }

        [Fact]
        public void Should_make_sure_version_is_different_if_schema_has_different_object()
        {
            // Arrange
            var id = "id";
            var title = "title";
            var owner = "owner";
            var purpose = "purpose";
            var actors = new List<v6Schemas.Schemas.Actor>();


            var processSchema = new v6Schemas.Schemas.ProcessSchema(id, title, owner, purpose);
            processSchema.ActorCollection = actors;
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
            Assert.True(processSchema.ActorCollection.Count == actors.Count);

        }

        [Fact]
        public void Should_Build_Correctly_From_v1_schema()
        {
            // Arrange
            var v1Process = new v1Schemas.Schemas.ProcessSchema("id-1", "title-1");
            var v1ProcessString = JsonConvert.SerializeObject(v1Process);

            // Act
            var v4Process = new v6Schemas.Schemas.ProcessSchema(v1ProcessString);

            // Assert
            Assert.True(v4Process.Id == v1Process.Id);
            Assert.True(v4Process.Title == v1Process.Title);
            Assert.True(v4Process.Owner == "Default Owner added to V2");
            Assert.True(v4Process.Version == v1Process.Version);
            Assert.True(v4Process.Purpose == "Default Purpose added to V3");
            Assert.True(v4Process.ActorCollection.Count == 0);
        }

        [Fact]
        public void Should_Build_Correctly_From_v2_schema()
        {
            // Arrange
            var v2Processes = new v2Schemas.Schemas.ProcessSchema("id-2", "title-2", "owner-2");
            var v2ProcessesStrimg = JsonConvert.SerializeObject(v2Processes);

            // Act
            var v4Process = new v6Schemas.Schemas.ProcessSchema(v2ProcessesStrimg);

            // Assert
            Assert.True(v4Process.Id == v2Processes.Id);
            Assert.True(v4Process.Title == v2Processes.Title);
            Assert.True(v4Process.Owner == v2Processes.Owner);
            Assert.True(v4Process.Version == v2Processes.Version);
            Assert.True(v4Process.Purpose == "Default Purpose added to V3");
            Assert.True(v4Process.ActorCollection.Count == 0);

        }

        [Fact]
        public void Should_Build_Correctly_From_v3_schema()
        {
            // Arrange
            var v3Processes = new v3Schemas.Schemas.ProcessSchema("id-3", "title-3", "owner-3", "purpose-3");
            var v3ProcessesStrimg = JsonConvert.SerializeObject(v3Processes);

            // Act
            var v4Process = new v6Schemas.Schemas.ProcessSchema(v3ProcessesStrimg);

            // Assert
            Assert.True(v4Process.Id == v3Processes.Id);
            Assert.True(v4Process.Title == v3Processes.Title);
            Assert.True(v4Process.Owner == v3Processes.Owner);
            Assert.True(v4Process.Version == v3Processes.Version);
            Assert.True(v4Process.Purpose == v3Processes.Purpose);
            Assert.True(v4Process.ActorCollection.Count == 0);

        }

        [Fact]
        public void Should_Build_Correctly_From_v4_schema()
        {
            // Arrange
            var v4Processes = new v4Schemas.Schemas.ProcessSchema("id-3", "title-3", "owner-3", "purpose-3");
            var v4ProcessesStrimg = JsonConvert.SerializeObject(v4Processes);

            // Act
            var v5Process = new v6Schemas.Schemas.ProcessSchema(v4ProcessesStrimg);

            // Assert
            Assert.True(v5Process.Id == v4Processes.Id);
            Assert.True(v5Process.Title == v4Processes.Title);
            Assert.True(v5Process.Owner == v4Processes.Owner);
            Assert.True(v5Process.Version == v4Processes.Version);
            Assert.True(v5Process.Purpose == v4Processes.Purpose);
            Assert.True(v5Process.ActorCollection.Count == 0);
        }

        [Fact]
        public void Should_Build_Correctly_From_v4_schema_with_actors()
        {
            // Arrange
            var v4Processes = new v4Schemas.Schemas.ProcessSchema("id-3", "title-3", "owner-3", "purpose-3");
            v4Processes.Actors = new List<v4Schemas.Schemas.Actor>() { new v4Schemas.Schemas.Actor() { Name = "bob", Age = "13" } };
            var v4ProcessesStrimg = JsonConvert.SerializeObject(v4Processes);

            // Act
            var v6Process = new v6Schemas.Schemas.ProcessSchema(v4ProcessesStrimg);

            // Assert
            Assert.True(v6Process.Id == v4Processes.Id);
            Assert.True(v6Process.Title == v4Processes.Title);
            Assert.True(v6Process.Owner == v4Processes.Owner);
            Assert.True(v6Process.Version == v4Processes.Version);
            Assert.True(v6Process.Purpose == v4Processes.Purpose);
            Assert.True(v6Process.ActorCollection.Count == 1);
            Assert.True(v6Process.ActorCollection[0].ActorName.FirstName == "bob");

            Assert.True(v6Process.ActorCollection[0].Role == "Default Role Added to v6");
        }


        [Fact]
        public void Should_Build_Correctly_From_v5_schema()
        {
            // Arrange
            var v5Processes = new v5Schemas.Schemas.ProcessSchema("id-3", "title-3", "owner-3", "purpose-3");
            var v5ProcessesStrimg = JsonConvert.SerializeObject(v5Processes);

            // Act
            var v6Process = new v6Schemas.Schemas.ProcessSchema(v5ProcessesStrimg);

            // Assert
            Assert.True(v6Process.Id == v5Processes.Id);
            Assert.True(v6Process.Title == v5Processes.Title);
            Assert.True(v6Process.Owner == v5Processes.Owner);
            Assert.True(v6Process.Version == v5Processes.Version);
            Assert.True(v6Process.Purpose == v5Processes.Purpose);
            Assert.True(v6Process.ActorCollection.Count == 0);
        }

        [Fact]
        public void Should_Build_Correctly_From_v5_schema_with_actors()
        {
            // Arrange
            var v5Processes = new v5Schemas.Schemas.ProcessSchema("id-3", "title-3", "owner-3", "purpose-3");
            v5Processes.ActorCollection = new List<v5Schemas.Schemas.Actor>() { new v5Schemas.Schemas.Actor() { Name = "bob", Age = "13" } };
            var v5ProcessesStrimg = JsonConvert.SerializeObject(v5Processes);

            // Act
            var v6Process = new v6Schemas.Schemas.ProcessSchema(v5ProcessesStrimg);

            // Assert
            Assert.True(v6Process.Id == v5Processes.Id);
            Assert.True(v6Process.Title == v5Processes.Title);
            Assert.True(v6Process.Owner == v5Processes.Owner);
            Assert.True(v6Process.Version == v5Processes.Version);
            Assert.True(v6Process.Purpose == v5Processes.Purpose);
            Assert.True(v6Process.ActorCollection.Count == 1);
            Assert.True(v6Process.ActorCollection[0].ActorName.FirstName == "bob");

            Assert.True(v6Process.ActorCollection[0].Role == "Default Role Added to v6");
        }

        // Lists
        [Fact]
        public void Should_make_sure_list_is_created_correctly()
        {
            // Arrange

            var actors = new List<v6Schemas.Schemas.Actor>();
            actors.Add(new v6Schemas.Schemas.Actor() { Role = "Student", ActorName = new v6Schemas.Schemas.ActorName() { FirstName = "Roddy", LastName = "P" } });
            actors.Add(new v6Schemas.Schemas.Actor() { Role = "Monkey", ActorName = new v6Schemas.Schemas.ActorName() { FirstName = "Lawson", LastName = "P" } });

            // Act
            var processSchema = new v6Schemas.Schemas.ProcessSchema("", "", "", "");
            processSchema.ActorCollection = actors;

            // Assert
            Assert.True(processSchema.ActorCollection.Count == 2);
            Assert.True(processSchema.ActorCollection[0].ActorName.FirstName == "Roddy");
            Assert.True(processSchema.ActorCollection[1].ActorName.FirstName == "Lawson");
            Assert.True(processSchema.ActorCollection[0].ActorName.LastName == "P");
            Assert.True(processSchema.ActorCollection[1].ActorName.LastName == "P");
            Assert.True(processSchema.ActorCollection[0].Role == "Student");
            Assert.True(processSchema.ActorCollection[1].Role == "Monkey");
            
        }
    }
}
