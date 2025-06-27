using NetArchTest.Rules;
using Xunit;

namespace GenAiDemo.ArchTests.Architecture;

public class DddArchitectureTests
{
    [Fact]
    public void Interfaces_Should_Not_Depend_On_Infrastructure()
    {
        var interfacesAssembly = typeof(GenAiDemo.Interfaces.Controllers.OrdersController).Assembly;
        var infrastructureAssembly = typeof(GenAiDemo.Infrastructure.Repositories.OrderRepository).Assembly;

        var result = Types.InAssembly(interfacesAssembly)
            .ShouldNot()
            .HaveDependencyOn(infrastructureAssembly.GetName().Name!)
            .GetResult();

        Assert.True(result.IsSuccessful);
    }
}
