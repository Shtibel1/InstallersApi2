using System.ComponentModel.DataAnnotations;
using BLL.Services.AuthService;
using BLL.Validations;
using DAL.Enums;

namespace InstallersApi2.Tests;

public class ConditionalRequiredAttributeTests
{
    private class Sample
    {
        public string? Kind { get; set; }
        [ConditionalRequired(nameof(Kind), "Pro")]
        public string? License { get; set; }
    }

    private class BrokenSample
    {
        [ConditionalRequired("DoesNotExist", "x")]
        public string? Value { get; set; }
    }

    private static List<ValidationResult> Validate(object model)
    {
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(model, new ValidationContext(model), results, validateAllProperties: true);
        return results;
    }

    [Fact]
    public void Fails_when_condition_matches_and_value_missing()
    {
        var results = Validate(new Sample { Kind = "Pro" });
        var error = Assert.Single(results);
        Assert.Equal("License is required when Kind is Pro.", error.ErrorMessage);
    }

    [Fact]
    public void Passes_when_condition_matches_and_value_present()
        => Assert.Empty(Validate(new Sample { Kind = "Pro", License = "L-1" }));

    [Theory]
    [InlineData("Basic")]
    [InlineData(null)]
    public void Passes_when_condition_does_not_match(string? kind)
        => Assert.Empty(Validate(new Sample { Kind = kind }));

    [Fact]
    public void Throws_when_referenced_property_does_not_exist()
        => Assert.Throws<ArgumentException>(() => Validate(new BrokenSample()));

    [Fact]
    public void SignupModel_requires_categories_for_service_providers()
    {
        var model = new SignupModel { Name = "a", Password = "p", Phone = "050", Role = Role.ServiceProvider };
        var error = Assert.Single(Validate(model));
        Assert.Contains("Categories", error.MemberNames);
    }

    [Fact]
    public void SignupModel_accepts_service_provider_with_categories()
    {
        var model = new SignupModel { Name = "a", Password = "p", Phone = "050", Role = Role.ServiceProvider, Categories = new() { Guid.NewGuid() } };
        Assert.Empty(Validate(model));
    }
}
