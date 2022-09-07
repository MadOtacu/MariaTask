using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static MariaTask.Model;

namespace MariaTask
{
    public class ViewModel : BindableBase
    {
        public Model _model;
        private Sample selectedSample;
        private Cities selectedCityToCheck;
        private DateTime selectedDateToCheck;
        private DateTime selectedDateToInsert;
        private string currentSampleNum;
        private string currentSampleName;
        private Cities currentSampleCity;
        private string currentSamplePhone;
        private DateTime? currentSampleDate;
        private int remainSamplesCounter;
        public ReadOnlyObservableCollection<Sample> Samples => _model.Samples;
        public ReadOnlyObservableCollection<DateTime> DateTimes => _model.DateTimes;
        public DateTime SelectedDateToInsert
        {
            get { return selectedDateToInsert; }
            set
            {
                selectedDateToInsert = value;
                RaisePropertyChanged(nameof(SelectedDateToInsert));
            }
        }
        public DateTime SelectedDateToCheck
        {
            get { return selectedDateToCheck; }
            set
            {
                selectedDateToCheck = value;
                RaisePropertyChanged(nameof(SelectedDateToCheck));
            }
        }
        public Cities SelectedCityToCheck
        {
            get { return selectedCityToCheck; }
            set
            {
                selectedCityToCheck = value;
                RaisePropertyChanged(nameof(SelectedCityToCheck));
            }
        }
        public int RemainSamplesCounter
        {
            get { return remainSamplesCounter; }
            set
            {
                remainSamplesCounter = value;
                RaisePropertyChanged(nameof(RemainSamplesCounter));
            }
        }
        public string CurrentSampleNum
        {
            get { return currentSampleNum; }
            set
            {
                currentSampleNum = value;
                RaisePropertyChanged(nameof(CurrentSampleNum));
            }
        }
        public string CurrentSampleName
        {
            get { return currentSampleName; }
            set
            {
                currentSampleName = value;
                RaisePropertyChanged(nameof(CurrentSampleName));
            }
        }
        public Cities CurrentSampleCity
        {
            get { return currentSampleCity; }
            set
            {
                currentSampleCity = value;
                RaisePropertyChanged(nameof(CurrentSampleCity));
            }
        }
        public string CurrentSamplePhone
        {
            get { return currentSamplePhone; }
            set
            {
                currentSamplePhone = value;
                RaisePropertyChanged(nameof(CurrentSamplePhone));
            }
        }
        public DateTime? CurrentSampleDate
        {
            get { return currentSampleDate; }
            set
            {
                currentSampleDate = value;
                RaisePropertyChanged(nameof(CurrentSampleDate));
            }
        }
        public DelegateCommand AssignDate { get; }
        public DelegateCommand CheckRemainSamples { get; }
        public Sample SelectedSample
        {
            get { return selectedSample; }
            set
            {
                selectedSample = value;
                CurrentSampleNum = selectedSample.Number;
                CurrentSampleName = selectedSample.Name;
                CurrentSampleCity = selectedSample.City;
                CurrentSamplePhone = selectedSample.Phone;
                CurrentSampleDate = selectedSample.SampleDate;
                RaisePropertyChanged(nameof(SelectedSample));
            }
        }
        public ViewModel()
        {
            _model = new Model();
            CheckRemainSamples = new DelegateCommand(() => RemainSamplesCounter = _model.CheckRemainSamples(SelectedCityToCheck, SelectedDateToCheck));
            AssignDate = new DelegateCommand(() => CurrentSampleDate = _model.AssignDate(SelectedSample, SelectedDateToInsert));
        }
    }
}
