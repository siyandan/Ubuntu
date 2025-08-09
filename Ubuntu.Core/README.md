# Ubuntu.Core.Mapping 🗺️

A lightweight and convention-based object mapper for .NET applications. `Ubuntu.Core.Mapping` provides a simple and concise way to map properties from one object to another, with a focus on simplicity and dependency injection.

-----

### Key Features ✨

  * **Simple Mapping Syntax**: Map objects with a single line of code, like `mapper.Map<UserDto>(user)`.
  * **Convention-Based Mapping**: Automatically maps properties with matching names and types.
  * **Fluent Configuration API**: Use a type-safe, fluent API to define custom property mappings.
  * **Dependency Injection-Friendly**: Designed to work seamlessly with .NET's dependency injection container.
  * **Minimalistic**: A straightforward alternative to larger mapping libraries, with no unnecessary overhead.

-----

### Installation 📦

You can install the package using the .NET CLI:

```bash
dotnet add package Ubuntu.Core
```

-----

### Getting Started 🚀

This example demonstrates how to set up and use `Ubuntu.Core.Mapping` in a console application.

1.  **Define Your Classes**:
    Create your source and destination classes. The `UserDto` class has a property (`FirstName`) with a different name from the source property (`Name`), which we will map.

    ```csharp
    public class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class UserDto
    {
        public string FirstName { get; set; }
        public int Age { get; set; }
    }
    ```

2.  **Configure Your Mappings**:
    In your application's startup, configure any custom mappings that are needed. You only need to do this once. For properties with the same name (like `Age`), no configuration is required.

    ```csharp
    MapperConfiguration.CreateMap<User, UserDto>()
        .ForMember(dest => dest.FirstName, src => src.Name);
    ```

3.  **Map Your Objects**:
    Use the `IMapper` instance to perform the mapping. The `Map` method uses the configuration to map properties correctly.

    ```csharp
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Ubuntu.Core.DependencyInjection;
    using Ubuntu.Core.Mapping;

    class Program
    {
        static void Main(string[] args)
        {
            // Set up dependency injection and mapping configuration
            var services = new ServiceCollection();
            services.AddUbuntuMappers();

            MapperConfiguration.CreateMap<User, UserDto>()
                .ForMember(dest => dest.FirstName, src => src.Name);

            var serviceProvider = services.BuildServiceProvider();
            var mapper = serviceProvider.GetRequiredService<IMapper>();

            // Perform the mapping
            var user = new User { Name = "Jane Doe", Age = 30 };
            var userDto = mapper.Map<UserDto>(user);

            // Output the result
            Console.WriteLine($"Mapped FirstName: {userDto.FirstName}");
            Console.WriteLine($"Mapped Age: {userDto.Age}");
        }
    }
    ```

-----

### License 📄

This project is licensed under the MIT License.