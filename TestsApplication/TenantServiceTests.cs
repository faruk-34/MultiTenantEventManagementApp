using Application.Models.SubRequestModel;
using Application.Models.SubResponseModel;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Moq;

namespace Tests.Application
{
    public class TenantServiceTests
    {
        private readonly Mock<ITenantRepository> _tenantRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly TenantService _tenantService;

        public TenantServiceTests()
        {
            _tenantRepositoryMock = new Mock<ITenantRepository>();
            _mapperMock = new Mock<IMapper>();
            _tenantService = new TenantService(_tenantRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Insert_Should_Return_Success_Response_When_Insert_Is_Successful()
        {
            // Arrange
            var request = new RequestTenant { Name = "TestTenant" };
            var tenantEntity = new Tenant { Id = 1, Name = "TestTenant" };
            var tenantVM = new TenantVM { Id = 1, Name = "TestTenant" };

            _mapperMock.Setup(m => m.Map<Tenant>(request)).Returns(tenantEntity);
            _mapperMock.Setup(m => m.Map<TenantVM>(tenantEntity)).Returns(tenantVM);

            // Act
            var result = await _tenantService.Insert(request, CancellationToken.None);

            // Assert
            _tenantRepositoryMock.Verify(r => r.Insert(tenantEntity, It.IsAny<CancellationToken>()), Times.Once);
            Assert.True(result.IsSuccess);
            Assert.Equal("Tenant baþarýyla oluþturuldu.", result.MessageTitle);
            Assert.Equal(tenantVM.Id, result.Data.Id);
        }

        [Fact]
        public async Task Insert_Should_Return_Failure_Response_When_Exception_Thrown()
        {
            // Arrange
            var request = new RequestTenant { Name = "TestTenant" };

            _mapperMock.Setup(m => m.Map<Tenant>(request)).Throws(new System.Exception("Insert failed"));

            // Act
            var result = await _tenantService.Insert(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Insert failed", result.ErrorMessage);
        }

        [Fact]
        public async Task Get_Should_Return_Tenant_When_Found()
        {
            // Arrange
            var tenantEntity = new Tenant { Id = 1, Name = "TestTenant" };
            var tenantVM = new TenantVM { Id = 1, Name = "TestTenant" };

            _tenantRepositoryMock.Setup(r => r.Get(1, It.IsAny<CancellationToken>())).ReturnsAsync(tenantEntity);
            _mapperMock.Setup(m => m.Map<TenantVM>(tenantEntity)).Returns(tenantVM);

            // Act
            var result = await _tenantService.Get(1, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(tenantVM.Id, result.Data.Id);
        }

        [Fact]
        public async Task Get_Should_Return_Error_When_Tenant_Not_Found()
        {
            // Arrange
            _tenantRepositoryMock.Setup(r => r.Get(99, It.IsAny<CancellationToken>())).ReturnsAsync((Tenant)null);

            // Act
            var result = await _tenantService.Get(99, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Tenant bulunamadý.", result.ErrorMessage);
        }

        [Fact]
        public async Task Get_Should_Return_Error_When_Exception_Thrown()
        {
            // Arrange
            _tenantRepositoryMock.Setup(r => r.Get(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new System.Exception("Database error"));

            // Act
            var result = await _tenantService.Get(1, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Database error", result.ErrorMessage);
        }
    }

}
