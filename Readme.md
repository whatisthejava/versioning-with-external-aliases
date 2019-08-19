


# Readme

## Introduction
This git repo has been written to demonstrate a strategy for model versioning within an versioned API. 

The project has been set up to allow v1 clients to continue using older API's without being forced to update.  
The models and the latest controller will allow older clients to continue to use the older API without being forced to update. 

This version is v5. 

Its a simple web api project which sets up 

API versioning
Swagger endpoints 

## Behavioural Differences in V5
The ProcessSchemas object has changed the internal collection name from actors to actorCollections.  

````
	public string Id { get; set; }  // From V1 
	public string Title { get; set; }  // From V1
	public string Owner { get; set; }  // From V2
	public string Purpose { get; set; }   // From V3
	public List<Actor> ActorCollection { get; set; } //New attribute   
	public string Version = "v5";  // Update the version 
````

## Changes from V4
We have now added the following projects
*  v5Models
   * Add a reference to v1Models 
		* In the reference to v1Models add an Alias calling it v1Schemas.
   * Add a reference to v2Models 
		* In the reference to v2Models add an Alias calling it v2Schemas.
   * Add a reference to v3Models 
		* In the reference to v3Models add an Alias calling it v3Schemas.
   * Add a reference to v4Models 
		* In the reference to v4Models add an Alias calling it v4Schemas.		
		
*  v5Models.Tests
   * Add a reference to v1Models 
		* In the reference to v1Models add an Alias calling it v1Schemas.
   * Add a reference to v2Models 
		* In the reference to v2Models add an Alias calling it v2Schemas.
   * Add a reference to v3Models 
		* In the reference to v3Models add an Alias calling it v3Schemas.	
   * Add a reference to v4Models 
		* In the reference to v4Models add an Alias calling it v4Schemas.	
   * Add a reference to v5Models 
		* In the reference to v5Models add an Alias calling it v5Schemas.			

*  Aliases
	* Add a reference to v5Models
		* In the reference to v5Models add an Alias calling it v4Schemas.		


And add the following  classes 
*  v5/ProcessesController 
	

And add the following changes 
*  Aliases/Startup.cs
   * In ConfigureSwagger 
		* Appended  new c.SwaggerEndpoint("/swagger/v5.0/swagger.json", "V5"); to line 52
	* In ConfigureApiVersioning
		* Update default version from 4.0 to 5.0
   * In AddSwagger 
	   * Preappend   new c.SwaggerEndpoint("/swagger/v5.0/swagger.json", "V5"); to line 125
		

Add External Aliases 
*  In latest/ProcessesController 
   * At top of file 
	   	* Add extern alias v5Schemas;
		* Modify using v5Schemas::Schemas;
*  In v5/ProcessesController 
	* At top of file 
	   	* Add extern alias v5Schemas;
   
*  In v5Models    
   * At top of file 
	  	* Add extern alias v4Schemas;
	
*  In testApp
	* Add v5 Page
		* redirect to correct version of API ;
 

   
## Explanation of versioning in API 
There is two ways to redirect clients to the correct API.

If a client wants to use the v1 endpoint they can use 
/api/v1/processes 
or /api/processes and provide a header with a key of api-version and a value of 1 

If a client wants to use the v2 endpoint they can use 
/api/v2/processes 
or /api/processes and provide a header with a key of api-version and a value of 2

If a client wants to use the v3 endpoint they can use 
/api/v3/processes 
or /api/processes and provide a header with a key of api-version and a value of 3

If a client wants to use the v4 endpoint they can use 
/api/v4/processes 
or /api/processes and provide a header with a key of api-version and a value of 4

If a client wants to use the v5 endpoint they can use 
/api/v5/processes 
or /api/processes and provide a header with a key of api-version and a value of 5


In latest controller 
````
        [HttpGet]
        public ActionResult<List<ProcessSchema>> LatestGet()
        {
            if (Request.ContainsApiVersion())
            {
                return Redirect($"/api/v{Request.GetApiVersion()}/processes");
            }
            return Redirect($"/api/v{CurrentVersionOfApiToMapTo}/processes");
        }				
		
		And the post 
		
        /// <summary>
        /// The reason this accepts a jsonResult is if in future we change the schema significantly we need to be able to still accept this version of the schema 
        /// </summary>
        /// <param name="jsonResult"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LatestWrite([FromBody] JObject jsonResult)
        {
            int version = CurrentVersionOfApiToMapTo;
            int.TryParse(Request.GetApiVersion(), out version);

            jsonResult["version"] = $"v{version}";
            var processSchema = new ProcessSchema(JsonConvert.SerializeObject(jsonResult));

            _repo.Write(new v1StorageModel.StorageModel() { Id = processSchema.Id, VersionOfContents = version, Contents = JsonConvert.SerializeObject(jsonResult) });
            return Ok();
        }
````

If the request contains the api-header key it will redirect back to that version 
if it doesnt the latest will redirect to the latest version as defined in the Startup

## Explanation of versioning in Model 
The v5 ProcessSchema is now responsible for versioning back to the v4.  
To do this we implement the following methods in the ProcessSchema. 


````

        public ProcessSchema(string potentialSchema)
        {
            var schema = JsonConvert.DeserializeObject<ProcessSchema>(potentialSchema);
            if (schema.Version == this.Version)
            {
                this.Id = schema.Id;
                this.Title = schema.Title;
                this.Owner = schema.Owner;
                this.Purpose = schema.Purpose;
                this.ActorCollection = schema.ActorCollection;
            }
            else
            {
                var previousSchema = ToPreviousVersion(potentialSchema);             
                this.Id = previousSchema.Id;
                this.Title = previousSchema.Title;
                this.Owner = previousSchema.Owner;
                this.Purpose = previousSchema.Purpose;
                this.ActorCollection = previousSchema.Actors.Select(x => new Actor(x.ToString())).ToList<Actor>();
            }
            this.ActorCollection = this.ActorCollection != null ? this.ActorCollection : schema.ActorCollection;

            this.Version = schema.Version;
        }

	
	/// <summary>
	/// Using external aliases i can convert this object into the previous verison 
	/// </summary>
	/// <returns></returns>
        public v4Schemas.Schemas.ProcessSchema ToPreviousVersion(string schema)
        {
            return new v4Schemas.Schemas.ProcessSchema(schema);
        }

````
This code allows a v5 API to accept a v1 or v2 or v3 or v4 schema string and it will return a v5 object.  
This allows us to convert a v3 ProcessSchema into a v5 ProcessSchema 
	


## API versioning 
Api versioning  is configured via Startup.ConfigureApiVersioning
We use a custom API version Reader which reads the version out the URL path.  

## Swagger has been set up to work with API versioning 
### Documentation on external aliases 
[Microsoft Docs](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/extern-alias)
[Stack Overflow ](https://stackoverflow.com/questions/2347260/when-must-we-use-extern-alias-keyword-in-c) - Someone has suggested this for versioning, although i cant find any one trying it.
[Walkthough from 2006](https://blogs.msdn.microsoft.com/ansonh/2006/09/27/extern-alias-walkthrough/) - Yes its that old. 
[Scott Hansellman blog](https://www.hanselman.com/blog/ASPNETCoreRESTfulWebAPIVersioningMadeEasy.aspx) - Versioning isnt this easy 

## Models 
The Models project contains a single simple POCO named ProcessSchema.

This is the object we will be adding version control to.  

At this point the POCO has the following attributes

````
	public string Id { get; set; }
	public string Title { get; set; }
	public string Version = "v1";   // The version is hard coded for simplicity at this time
````

and the following methods 

````
	public override string ToString()
	{	
		return JsonConvert.SerializeObject(this);
	}
````
The other model is in project StorageRepository 
The StorageModel.cs is outlined below 

````
    public class StorageModel
    {
        public string Id { get; set; }
        public string Contents { get; set; }
        public int VersionOfContents { get; set; }
	}
	
````
This model has three atributes. 
Id - The unique id of the model 
The Contents - The ProcessSchema model is serialized using JSON and written here.  Wrapping it in the StorageModel allows us more flexibility when reading and writing
VersionOfContents - The version of the schema 



## Controllers
At this point we have two controllers
v1/ProcessesControllers  which has three endpoints 

````
	[HttpGet] 
	public ActionResult<List<ProcessSchema>> Get()
 
	[HttpGet]
	[Route("{id}")]
    public ActionResult<ProcessSchema> GetById(string id)
	
	[HttpPost]
    public ActionResult Write([FromBody] JObject jsonResult)
````

The latest controller has the following endpoints 
````
[HttpGet] 
	public ActionResult<List<ProcessSchema>> LatestGet()
 
	[HttpGet]
	[Route("{id}")]
    public ActionResult<ProcessSchema> LatestGetById(string id)
	
    /// <summary>
	/// The reason this accepts a jsonResult is if in future we change the schema significantly we need to be able to still accept this version of the schema 
	/// </summary>
	/// <param name="jsonResult"></param>
	/// <returns></returns>
	[HttpPost]
    public ActionResult LatestWrite([FromBody] JObject jsonResult)
````


The latest controller will redirect calls back to a specified API if a header of api-version is passed to it 
If no header is passed then the latest controller will redirect to the latest version of the API as specified in the API configuration 


## Notes
This isnt set up to work with a databae, it uses a folder on your computer.  It can be set in 

appsettings.json 

````
  "StorageRepo": {
    "PathToDb": "/Development/Temp/"
````

## Test App
A MVC test app has been created to demonstrate the API.

The MVC has a V1 page.  If the V1 Page is loaded it has the ability to use the latest controller or the v1 controller 
The MVC has a V2 page.  If the V2 Page is loaded it has the ability to use the latest controller or the v2 controller 
The MVC has a V3 page.  If the V3 Page is loaded it has the ability to use the latest controller or the v3 controller 
The MVC has a V4 page.  If the V4 Page is loaded it has the ability to use the latest controller or the v4 controller 
The MVC has a V5 page.  If the V5 Page is loaded it has the ability to use the latest controller or the v5 controller 


## Running the App
Download the code 
Open in Visual Studio
Make The solution a multi start project 
Run 

The API will open in Swagger 

The Test App will open on the index page. 