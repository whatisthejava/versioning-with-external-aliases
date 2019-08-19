# Readme

## Introduction
This git repo has been written to demonstrate a strategy for model versioning within an versioned API. 

The project has been set up to allow v1 clients to continue using older API's without being forced to update.  
The models and the latest controller will allow older clients to continue to use the older API without being forced to update. 

This version is v1. 

It has no versioning yet

Its a simple web api project which sets up 

API versioning
Swagger endpoints 
and the idea of a latest controller and v1 controllers 


### API versioning 
Api versioning  is configured via Startup.ConfigureApiVersioning
We use a custom API version Reader which reads the version out the URL path.  

### Swagger has been set up to work with API versioning 
### Documentation on external aliases 
[Microsoft Docs](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/extern-alias)
[Stack Overflow ](https://stackoverflow.com/questions/2347260/when-must-we-use-extern-alias-keyword-in-c) - Someone has suggested this for versioning, although i cant find any one trying it.
[Walkthough from 2006](https://blogs.msdn.microsoft.com/ansonh/2006/09/27/extern-alias-walkthrough/) - Yes its that old. 
[Scott Hansellman blog](https://www.hanselman.com/blog/ASPNETCoreRESTfulWebAPIVersioningMadeEasy.aspx) - Versioning isnt this easy 

### Models 
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

### Controllers
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
	public ActionResult<List<ProcessSchema>> Get()
 
	[HttpGet]
	[Route("{id}")]
    public ActionResult<ProcessSchema> GetById(string id)
	
	[HttpPost]
    public ActionResult Write([FromBody] JObject jsonResult)
````

The latest controller will redirect calls back to a specified API if a header of api-version is passed to it 
If no header is passed then the latest controller will redirect to the latest version of the API as specified in the API configuration 


### Notes
This isnt set up to work with a databae, it uses a folder on your computer.  It can be set in 

appsettings.json 

````
  "StorageRepo": {
    "PathToDb": "/Development/Temp/"
````

### Test App
A MVC test app has been created to demonstrate the API.
