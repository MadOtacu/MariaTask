using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MariaTask;
using Xunit;

namespace MariaTask.Tests
{
    public class ModelTests
    {
        [Fact]
        public void AssignDate_ShouldAssignNewDate()
        {
            var model = new Model();
            Model.Sample testSample = new Model.Sample("123456", "Viktor", Cities.Moscow, "198245", null);
            DateTime settedDate = DateTime.Today.AddDays(2);
            DateTime? expected = DateTime.Today.AddDays(2);
            DateTime? actual = model.AssignDate(testSample, settedDate);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AssignDate_ShouldAssignThemself()
        {
            var model = new Model();
            Model.Sample testSample = new Model.Sample("123456", "Viktor", Cities.Saratov, "198245", DateTime.Today);
            DateTime settedDate = DateTime.Today.AddDays(2);
            DateTime? expected = DateTime.Today;
            DateTime? actual = model.AssignDate(testSample, settedDate);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AssignDate_ShouldAssignNull()
        {
            var model = new Model();
            Model.Sample testSample = new Model.Sample("123456", "Viktor", Cities.Saratov, "198245", null);
            DateTime settedDate = DateTime.Today;
            DateTime? expected = null;
            DateTime? actual = model.AssignDate(testSample, settedDate);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CheckRemainSamples_ShouldReturnRemainSamples()
        {
            var model = new Model();
            Cities selectedCity = Cities.Moscow;
            DateTime selectedDate = DateTime.Today;
            int expected = 2;
            int actual = model.CheckRemainSamples(selectedCity, selectedDate);
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void CheckRemainSamples_ShouldReturnZero()
        {
            var model = new Model();
            Cities selectedCity = Cities.Saratov;
            DateTime selectedDate = default;
            int expected = 0;
            int actual = model.CheckRemainSamples(selectedCity, selectedDate);
            Assert.Equal(expected, actual);
        }
    }
}
