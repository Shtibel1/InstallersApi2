using AutoMapper;
using BLL.Services;
using BLL.Vms;
using DAL.Entities;
using DAL.Interfaces;
using Moq;

namespace InstallersApi2.Tests;

public class CalculationsServiceTests
{
    [Fact]
    public async Task Create_links_every_assignment_id_and_persists()
    {
        var repo = new Mock<ICalaulationsRepository>();
        Calculation? saved = null;
        repo.Setup(r => r.Create(It.IsAny<Calculation>())).Callback<Calculation>(c => saved = c).Returns(Task.CompletedTask);
        var mapper = new Mock<IMapper>();
        mapper.Setup(m => m.Map<Calculation>(It.IsAny<CalaulationVm>())).Returns(new Calculation());
        var ids = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };

        var service = new CalaulationsServics(repo.Object, mapper.Object, Mock.Of<IServiceProvidersRepository>());
        await service.Create(new CalaulationVm { Description = "Oct", Price = 1200m, AssignmentIds = ids });

        repo.Verify(r => r.Create(It.IsAny<Calculation>()), Times.Once);
        Assert.NotNull(saved);
        Assert.Equal(ids, saved!.CalculationAssignments.Select(a => a.AssignmentId).ToList());
    }

    [Fact]
    public async Task Create_with_no_assignments_persists_empty_link_list()
    {
        var repo = new Mock<ICalaulationsRepository>();
        Calculation? saved = null;
        repo.Setup(r => r.Create(It.IsAny<Calculation>())).Callback<Calculation>(c => saved = c).Returns(Task.CompletedTask);
        var mapper = new Mock<IMapper>();
        mapper.Setup(m => m.Map<Calculation>(It.IsAny<CalaulationVm>())).Returns(new Calculation());

        var service = new CalaulationsServics(repo.Object, mapper.Object, Mock.Of<IServiceProvidersRepository>());
        await service.Create(new CalaulationVm { AssignmentIds = new() });

        Assert.Empty(saved!.CalculationAssignments);
    }
}
