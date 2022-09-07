using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using static MariaTask.Model;

namespace MariaTask
{
    public class Model : BindableBase
    {
        private int remainSamples;
        public int RemainSamples
        {
            get { return remainSamples; }
            set
            {
                remainSamples = value;
                RaisePropertyChanged("RemainSamples");
            }
        }
        public ReadOnlyObservableCollection<State> States { get; }
        public ReadOnlyObservableCollection<Sample> Samples { get; }
        public ReadOnlyObservableCollection<DateTime> DateTimes { get; }

        private ObservableCollection<DateTime> _dateTimes { get; set; }
        private ObservableCollection<State> _states { get; set; }
        private ObservableCollection<Sample> _samples { get; set; }

        public Model()
        {
            _states = new ObservableCollection<State>
            {
                new State(Cities.Moscow, DateTime.Today, 2),
                new State(Cities.Moscow, DateTime.Today.AddDays(1), 10),
                new State(Cities.Moscow, DateTime.Today.AddDays(2), 8),
                new State(Cities.Moscow, DateTime.Today.AddDays(3), 12),
                new State(Cities.Moscow, DateTime.Today.AddDays(4), 6),
                new State(Cities.Moscow, DateTime.Today.AddDays(5), 9),
                new State(Cities.Moscow, DateTime.Today.AddDays(6), 7),

                new State(Cities.Saratov, DateTime.Today, 0),
                new State(Cities.Saratov, DateTime.Today.AddDays(1), 11),
                new State(Cities.Saratov, DateTime.Today.AddDays(2), 8),
                new State(Cities.Saratov, DateTime.Today.AddDays(3), 10),
                new State(Cities.Saratov, DateTime.Today.AddDays(4), 7),
                new State(Cities.Saratov, DateTime.Today.AddDays(5), 9),
                new State(Cities.Saratov, DateTime.Today.AddDays(6), 10)
            };
            _samples = new ObservableCollection<Sample>
            {
                new Sample("123456", "Viktor", Cities.Moscow, "198862", DateTime.Today),
                new Sample("124578", "Valeriy", Cities.Moscow, "954512", null),
                new Sample("235689", "Vitaliy", Cities.Saratov, "127956", DateTime.Today),
                new Sample("745965", "Vladimir", Cities.Saratov, "984123", null),
            };
            _dateTimes = new ObservableCollection<DateTime>
            {
                DateTime.Today,
                DateTime.Today.AddDays(1),
                DateTime.Today.AddDays(2),
                DateTime.Today.AddDays(3),
                DateTime.Today.AddDays(4),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(6),
            };
            States = new ReadOnlyObservableCollection<State>(_states);
            Samples = new ReadOnlyObservableCollection<Sample>(_samples);
            DateTimes = new ReadOnlyObservableCollection<DateTime>(_dateTimes);
        }

        public DateTime? AssignDate(Sample selectedSample, DateTime date)
        {

            DateTime? currentDate = selectedSample.SampleDate;
            State selected = States.First(x => x.StateDate == date && x.City == selectedSample.City);
            if (selected.Limit <= 0)
                return currentDate;
            if (selectedSample.SampleDate == null)
            {
                selected.Limit--;
                selectedSample.SampleDate = date;
                return selected.StateDate;
            }

            return currentDate;
        }

        public int CheckRemainSamples(Cities city, DateTime date)
        {
            foreach (var state in States)
            {
                if (state.City == city && state.StateDate == date)
                {
                    State selected = state;
                    return selected.Limit;
                }
            }
            return 0;
        }

        public class Sample : BindableBase
        {
            private DateTime? date;
            public string Number { get; }
            public string Name { get; }
            public Cities City { get; }
            public string Phone { get; }
            public DateTime? SampleDate
            {
                get { return date; }
                set
                {
                    date = value;
                    RaisePropertyChanged(nameof(SampleDate));
                }
            }

            public Sample(string number, string name, Cities city, string phone, DateTime? date)
            {
                Number = number;
                Name = name;
                City = city;
                Phone = phone;
                SampleDate = date;
            }
        }

        public class State : BindableBase
        {
            private int limit;
            public Cities City { get; }
            public DateTime StateDate { get; }
            public int Limit
            {
                get { return limit; }
                set
                {
                    limit = value;
                    RaisePropertyChanged(nameof(Limit));
                }
            }

            public State(Cities city, DateTime date, int limit)
            {
                City = city;
                StateDate = date;
                Limit = limit;
            }
        }
    }

    public enum Cities
    {
        Moscow,
        Saratov
    }
}