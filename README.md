# ObjectFlow.Core.Extensibility

Objectflow provides developers with a straight-forward way of separating business logic from control flow. This leads to more flexible software that is cheaper to change. With this library you can use it via JSON definition files.

```json

[
	{
		""Name"": ""CreateGearSystem"",
		""DisplayName"": ""Create Gear System"",
		""Priority"": 0,
		""DependsOn"": [],
		""Handler"": ""MyDll.CreateGearSystem, MyDll""
	},
	{
		""Name"": ""CreateSeats"",
		""DisplayName"": ""Create Gear System"",
		""Priority"": 0,
		""DependsOn"": [],
		""Handler"": ""MyDll.CreateSeats, MyDll""
	},
	{
		""Name"": ""CreateWheels"",
		""DisplayName"": ""Create Gear System"",
		""Priority"": 0,
		""DependsOn"": [ ""CreateSeats"" ],
		""Handler"": ""MyDll.CreateWheels, MyDll""
	},
	{
		""Name"": ""CreateDoors"",
		""DisplayName"": ""Create Gear System"",
		""Priority"": 0,
		""DependsOn"": [ ""CreateWheels"" ],
		""Handler"": ""MyDll.CreateDoors, MyDll""
	},
	{
		""Name"": ""Paint"",
		""DisplayName"": ""Create Gear System"",
		""Priority"": 0,
		""DependsOn"": [ ""CreateDoors"" ],
		""Handler"": ""MyDll.Paint, MyDll""
	},
]

```

```csharp

static void Main(string[] args)
{
	var rawAssemblyDefinition = LoadJsonDefinition();
	var assemblyDefinition = rawAssemblyDefinition.ToWorkflowDefinition<Car>();
	var car = Workflow<Car>.Definition().FromDefinition(assemblyDefinition).Start(new Car { });
	return car;
}

public class CreateGearSystem : StepOperation<Car>
{
	public override Car Execute(Car data)
	{
		data.GearSystem = "Mechanical";

		return data;
	}
}

public class CreateSeats : StepOperation<Car>
{
	public override Car Execute(Car data)
	{
		data.NumberOfSeats = 4;

		return data;
	}
}

public class CreateWheels : StepOperation<Car>
{
	public override Car Execute(Car data)
	{
		data.NumberOfWheels = 4;

		return data;
	}
}

public class CreateDoors : StepOperation<Car>
{
	public override Car Execute(Car data)
	{
		data.NumberOfDoors = 5;

		return data;
	}
}

public class Paint : StepOperation<Car>
{
	public override Car Execute(Car data)
	{
		data.Color = "Black";

		return data;
	}
}

public class Car
{
	public string Color { get; set; }
	public int NumberOfSeats { get; set; }
	public int NumberOfWheels { get; set; }
	public int NumberOfDoors { get; set; }
	public string GearSystem { get; set; }
}
	
```
