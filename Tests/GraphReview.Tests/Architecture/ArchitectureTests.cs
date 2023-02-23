using NetArchTest.Rules;

namespace GraphReview.Tests.Architecture
{
    public class ArchitectureTests
    {
        public const string ApiNamespace = "GraphReview.Api";
        public const string DomainNamespace = "GraphReview.Domain";
        // public const string ContractsNamespace = "GraphReview.Contracts";
        public const string ApplicationNamespace = "GraphReview.Application";
        public const string InfrastructureNamespace = "GraphReview.Infrastructure";

        [Fact]
        public void DomainLayer_ShouldNotHaveDependencies()
        {
            // Arrange
            var assembly = typeof(GraphReview.Domain.AssemblyReference).Assembly;

            var otherProjects = new[]
            {
                ApiNamespace,
                DomainNamespace,
                //ContractsNamespace,
                ApplicationNamespace,
                InfrastructureNamespace
            };

            // Act
            var result = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(otherProjects)
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        
        [Fact]
        public void ApplicationLayer_ShouldNotHaveDependencies_ExceptOnDomainLayer()
        {
            // Arrange
            var assembly = typeof(GraphReview.Application.AssemblyReference).Assembly;

            var otherProjects = new[]
            {
                ApiNamespace,
                //ContractsNamespace,
                ApplicationNamespace,
                InfrastructureNamespace
            };

            // Act
            var result = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(otherProjects)
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }
    }
}
