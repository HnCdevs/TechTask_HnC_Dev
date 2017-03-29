﻿using System;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NSubstitute.ClearExtensions;
using TechnicalTask.Controllers;
using TechnicalTask.Data;
using TechnicalTask.Models;
using TechnicalTask.Repository;
using Xunit;

namespace XUnitTests
{
    public class CountriesControllerTests : IDisposable
    {
        private readonly CountryRepository _repository;
        private readonly CountriesController _controller;

        public CountriesControllerTests()
        {
            var list = new List<Country>
            {
                new Country { Id = 1, Name = "test 1" },
                new Country { Id = 2, Name = "test 2" }

            }.AsQueryable();

            var mockContext = Substitute.For<TtContext>();
            _repository = Substitute.For<CountryRepository>(mockContext);
            _repository.GetList().Returns(list);
            _repository.GetItem(Arg.Any<int>()).Returns(new Country { Id = 1, Name = "test 1" });
            _repository.Create(Arg.Any<Country>());
            _repository.Update(Arg.Any<int>(), Arg.Any<Country>());
            _repository.Delete(Arg.Any<int>());

            _controller = new CountriesController(_repository);
        }

        [Fact]
        public void GetAllTest()
        {
            var countries = _controller.Get();
            Assert.Equal(2, countries.Count());
        }

        [Fact]
        public void GetItemTest()
        {
            var country = _controller.Get(1);
            Assert.Equal(1, country.Id);
        }

        [Fact]
        public void CreateBadInputTest()
        {
            Assert.Throws<ArgumentNullException>(() => _controller.Post(null));
        }

        [Fact]
        public void CreateGoodTest()
        {
            _repository.IsValid(Arg.Any<Country>()).Returns(true);
            _controller.Post(new CountryInput());
            _repository.Received(1).Create(Arg.Any<Country>());
        }

        [Fact]
        public void CreateInvalidTest()
        {
            _repository.IsValid(Arg.Any<Country>()).Returns(false);
            Assert.Throws<ArgumentException>(() => _controller.Post(new CountryInput()));
        }

        [Fact]
        public void UpdateBadInputTest()
        {
            Assert.Throws<ArgumentNullException>(() => _controller.Put(0, null));
        }

        [Fact]
        public void UpdateGoodTest()
        {
            _repository.IsValid(Arg.Any<Country>()).Returns(true);
            _controller.Put(0, new CountryInput());
            _repository.Received(1).Update(Arg.Any<int>(), Arg.Any<Country>());
        }

        [Fact]
        public void UpdateInvalidTest()
        {
            _repository.IsValid(Arg.Any<Country>()).Returns(false);
            Assert.Throws<ArgumentException>(() => _controller.Put(0, new CountryInput()));
        }

        [Fact]
        public void DeleteTest()
        {
            _controller.Delete(0);
            _repository.Received(1).Delete(Arg.Any<int>());
        }

        public void Dispose()
        {
            _repository.ClearSubstitute();
        }
    }
}