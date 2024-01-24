using Experimentum.Domain.Features;
using Experimentum.Shared.Features.Phones;
using FluentAssertions;

namespace Experimentum.Tests.Unit.Features.Phones
{
    public class PhoneContractExtensionsShould
    {
        [Fact]
        public void Convert_phone_to_request()
        {
            // Arrange
            var phone = Phone.Create("123-456-7890", PhoneType.Mobile).Value;

            // Act
            var request = phone.ToRequest();

            // Assert
            request.Should().BeEquivalentTo(new PhoneRequest
            {
                Id = phone.Id,
                Number = phone.Number,
                PhoneType = phone.PhoneType
            });
        }

        [Fact]
        public void Convert_phone_response_to_request()
        {
            var phoneResponse = new PhoneResponse
            {
                Id = 1,
                Number = "123-456-7890",
                PhoneType = PhoneType.Mobile
            };

            var request = phoneResponse.ToRequest();

            request.Should().BeEquivalentTo(new PhoneRequest
            {
                Id = phoneResponse.Id,
                Number = phoneResponse.Number,
                PhoneType = phoneResponse.PhoneType
            });
        }

        [Fact]
        public void Convert_phone_to_response()
        {
            var phone = Phone.Create("123-456-7890", PhoneType.Mobile).Value;

            var response = phone.ToResponse();

            response.Should().BeEquivalentTo(new PhoneResponse
            {
                Id = phone.Id,
                Number = phone.Number,
                PhoneType = phone.PhoneType
            });
        }

        [Fact]
        public void Convert_request_to_response()
        {
            var phoneRequest = new PhoneRequest
            {
                Id = 1,
                Number = "123-456-7890",
                PhoneType = PhoneType.Mobile
            };

            var response = phoneRequest.ToResponse();

            response.Should().BeEquivalentTo(new PhoneResponse
            {
                Id = phoneRequest.Id,
                Number = phoneRequest.Number,
                PhoneType = phoneRequest.PhoneType
            });
        }

        [Fact]
        public void Convert_request_to_entity()
        {
            var phoneRequest = new PhoneRequest
            {
                Id = 1, // Note: Id is not used in conversion to entity
                Number = "123-456-7890",
                PhoneType = PhoneType.Mobile
            };

            var phone = phoneRequest.ToREntity();

            phone.Should().NotBeNull();
            phone!.Number.Should().Be(phoneRequest.Number);
            phone.PhoneType.Should().Be(phoneRequest.PhoneType);
        }
    }
}