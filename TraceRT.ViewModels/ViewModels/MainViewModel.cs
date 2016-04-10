namespace TraceRT.ViewModels.ViewModels
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading;
    using System.Threading.Tasks;
    using TraceRT.DataAccess.Providers;
    using TraceRT.Models.Enums;
    using TraceRT.Models.Models;

    public class MainViewModel : ViewModelBase
    {
        #region field
        private string host;

        private bool isOpenSettingsPanel;
        private bool isWorking;
        private WorkType currentState;
        private PingModel currentPing;
        private TraceModel currentTrace;
        private CancellationTokenSource cancellationTokenSource;
        #endregion

        public MainViewModel() : base()
        {
            this.IsWorking = false;
            this.IsOpenSettingsPanel = false;

            this.cancellationTokenSource = new CancellationTokenSource();

            this.Pings = new ObservableCollection<PingModel>();
            this.Traces = new ObservableCollection<TraceModel>();

            this.InitializeCommand = new RelayCommand(this.InitializeExecute);

            this.SharedCommand = new RelayCommand(this.ShareExecute);
            this.OpenOrCloseSettingsPanelCommand = new RelayCommand(this.OpenOrCloseSettingsPanelExecute);
            this.StartCommand = new RelayCommand(this.StartExecute);
            this.StopCommand = new RelayCommand(this.StopExecute);

            this.BackCommand = new RelayCommand(this.BackExecute, this.BackCanExecute);
            this.RemoveCurrentCommand = new RelayCommand(this.RemoveCurrentExecute, this.RemoveCurrentCanExecute);
            this.OpenFullScreenCommand = new RelayCommand(this.OpenFullScreenExecute, this.OpenFullScreenCanExecute);
            this.CopyCommand = new RelayCommand(this.CopyExecute, this.CopyCanExecute);

            this.ClearPingsCommand = new RelayCommand(this.ClearPingsExecute);
            this.ClearTracesCommand = new RelayCommand(this.ClearTracesExecute);

            this.States = new List<WorkType>();
            this.States.Add(new WorkType("ping", WorkState.Ping));
            this.States.Add(new WorkType("trace", WorkState.Trace));
            this.CurrentState = this.States[0];
        }

        #region property

        public string Host
        {
            get 
            { 
                return this.host; 
            }

            set
            {
                this.host = value;
                this.RaisePropertyChanged();
            }
        }

        public bool IsOpenSettingsPanel
        {
            get 
            { 
                return this.isOpenSettingsPanel; 
            }

            set
            {
                this.isOpenSettingsPanel = value;
                this.RaisePropertyChanged();
            }
        }

        public bool IsWorking
        {
            get 
            { 
                return this.isWorking; 
            }

            set
            {
                this.isWorking = value;
                this.RaisePropertyChanged();
            }
        }

        public bool IsEmpty
        {
            get
            {
                bool result = true;
                switch (this.currentState.State)
                {
                    case WorkState.Ping:
                        result = this.Pings.Count == 0;
                        break;
                    case WorkState.Trace:
                        result = this.Traces.Count == 0;
                        break;
                }

                return result;
            }
        }


        public WorkType CurrentState
        {
            get 
            { 
                return this.currentState;
            }

            set
            {
                this.currentState = value;
                this.RaisePropertyChanged();
                this.RaisePropertyChanged(() => this.IsEmpty);
                this.AllCommandRaiseCanExecuteChanged();
            }
        }

        public List<WorkType> States { get; private set; }

        public ObservableCollection<PingModel> Pings { get; set; }

        public ObservableCollection<TraceModel> Traces { get; set; }

        public PingModel CurrentPing
        {
            get
            {
                return this.currentPing;
            }

            set
            {
                this.currentPing = value;
                this.RaisePropertyChanged();
                if (this.currentPing != null && this.currentPing.IsWorking)
                {
                    this.IsWorking = this.currentPing.IsWorking;
                }

                this.AllCommandRaiseCanExecuteChanged();
            }
        }

        public TraceModel CurrentTrace
        {
            get 
            { 
                return this.currentTrace; 
            }

            set
            {
                this.currentTrace = value;
                this.RaisePropertyChanged();

                this.AllCommandRaiseCanExecuteChanged();
            }
        }

        public RelayCommand InitializeCommand { get; set; }

        public RelayCommand SharedCommand { get; set; }

        public RelayCommand BackCommand { get; set; }

        public RelayCommand OpenOrCloseSettingsPanelCommand { get; set; }

        public RelayCommand StartCommand { get; set; }

        public RelayCommand StopCommand { get; set; }

        public RelayCommand RemoveCurrentCommand { get; set; }

        public RelayCommand OpenFullScreenCommand { get; set; }

        public RelayCommand CopyCommand { get; set; }

        public RelayCommand ClearPingsCommand { get; set; }

        public RelayCommand ClearTracesCommand { get; set; }
        #endregion

        #region function

        private void InitializeExecute()
        {
            this.AllCommandRaiseCanExecuteChanged();

            this.Pings.CollectionChanged += (s, e) =>
            {
                this.RaisePropertyChanged(() => this.IsEmpty);
            };

            this.Traces.CollectionChanged += (s, e) =>
            {
                this.RaisePropertyChanged(() => this.IsEmpty);
            };

            this.InitializePingsAsync();
            this.InitializeTraceAsync();
        }
        

        private async Task InitializePingsAsync()
        {
            await PingStorageProvider.Instance.LoadAsync();
            var tempList = PingStorageProvider.Instance.Pings;
            foreach (PingModel ping in tempList)
            {
                this.Pings.Add(ping);
            }
        }

        private async Task InitializeTraceAsync()
        {
            await TraceStorageProvider.Instance.LoadAsync();
            var tempList = TraceStorageProvider.Instance.Traces;
            foreach (TraceModel trace in tempList)
            {
                this.Traces.Add(trace);
            }
        }

        //TODO: Create Share method
        private void ShareExecute()
        {
            throw new NotImplementedException();
        }

        private void BackExecute()
        {
            switch (this.currentState.State)
            {
                case WorkState.Ping:
                    this.CurrentPing = null;
                    break;
                case WorkState.Trace:
                    this.CurrentTrace = null;
                    break;
            }

            this.IsWorking = false;
            this.Host = string.Empty;
        }

        private bool BackCanExecute()
        {
            var result = false;
            switch (this.currentState.State)
            {
                case WorkState.Ping:
                    result = this.CurrentPing != null;
                    break;
                case WorkState.Trace:
                    result = this.CurrentTrace != null;
                    break;
            }

            return result;
        }

        private void OpenOrCloseSettingsPanelExecute()
        {
            this.IsOpenSettingsPanel = !this.IsOpenSettingsPanel;
        }

        private async void StartExecute()
        {
            switch (this.currentState.State)
            {
                case WorkState.Ping:
                    await this.StartPingAsync();
                    break;
                case WorkState.Trace:
                    await this.StartTraceAsync();
                    break;
            }
        }

        private async Task StartPingAsync()
        {
            if (!this.IsWorking && !string.IsNullOrWhiteSpace(this.Host))
            {
                this.IsWorking = true;

                var settings = PingSettingsProvider.Instance.GetCurrentPingSettings();
                settings.Host = this.host;

                var newPingModel = new PingModel(settings.Host, settings.BufferLength);
                var token = newPingModel.CancellationTokenSource.Token;

                this.CurrentPing = newPingModel;
                this.Pings.Insert(0, newPingModel);

                await PingProvider.Instance.PingAsync(newPingModel, settings, token);
                await PingStorageProvider.Instance.AddAsync(newPingModel);

                this.RemoveCurrentCommand.RaiseCanExecuteChanged();

                this.IsWorking = false;
            }
        }

        private async Task StartTraceAsync()
        {
            if (!this.IsWorking && !string.IsNullOrWhiteSpace(this.Host))
            {

                this.IsWorking = true;

                var settings = TraceSettingsProvider.Instance.CreateSettings();
                settings.Host = this.host;

                var newTraceModel = new TraceModel(this.host);
                var token = newTraceModel.CancellationTokenSource.Token;

                newTraceModel.IsReady = false;

                this.CurrentTrace = newTraceModel;
                this.Traces.Insert(0, newTraceModel);

                await TraceProvider.Instance.TraceAsync(newTraceModel, settings, token);
                await TraceStorageProvider.Instance.AddAsync(newTraceModel);


                this.IsWorking = false;
            }
        }

        private void StopExecute()
        {
            switch (this.currentState.State)
            {
                case WorkState.Ping:
                    this.StopPingAsync();
                    break;
                case WorkState.Trace:
                    this.StopTraceAsync();
                    break;
            }
        }

        private void StopPingAsync()
        {
            this.CurrentPing.CancellationTokenSource.Cancel();
            this.CurrentPing.IsWorking = false;
            this.IsWorking = false;
        }

        private void StopTraceAsync()
        {
            this.CurrentTrace.CancellationTokenSource.Cancel();
            this.CurrentTrace.IsReady = true;
            this.IsWorking = false;
        }

        private async void RemoveCurrentExecute()
        {
            switch (this.currentState.State)
            {
                case WorkState.Ping:
                    await this.RemoveCurrentPingAsync();
                    break;
                case WorkState.Trace:
                    await this.RemoveCurrentTraceAsync();
                    break;
            }
        }

        private async Task RemoveCurrentPingAsync()
        {
            await PingStorageProvider.Instance.RemoveAsync(this.CurrentPing);
            this.Pings.Remove(this.CurrentPing);
            this.CurrentPing = null;
        }

        private async Task RemoveCurrentTraceAsync()
        {
            await TraceStorageProvider.Instance.RemoveAsync(this.CurrentTrace);
            this.Traces.Remove(this.CurrentTrace);
            this.CurrentTrace = null;
        }

        private bool RemoveCurrentCanExecute()
        {
            var result = false;
            switch (this.currentState.State)
            {
                case WorkState.Ping:
                    result = this.CurrentPing != null && this.CurrentPing.IsReady;
                    break;
                case WorkState.Trace:
                    result = this.CurrentTrace != null;
                    break;
            }

            return result;
        }

        //TODO: Create open full screen map method
        private void OpenFullScreenExecute()
        {
            throw new NotImplementedException();
        }

        private bool OpenFullScreenCanExecute()
        {
            var isExist = this.CurrentTrace != null;
            var isTraceState = this.currentState.State == WorkState.Trace;
            return isExist && isTraceState;
        }

        //TODO: Create copy method
        private void CopyExecute()
        {
            throw new NotImplementedException();
        }

        private bool CopyCanExecute()
        {
            var isExist = this.CurrentTrace != null;
            var isTraceState = this.currentState.State == WorkState.Trace;
            return isExist && isTraceState;
        }

        private async void ClearPingsExecute()
        {
            this.Pings.Clear();
            await PingStorageProvider.Instance.ClearAsync();
        }

        private async void ClearTracesExecute()
        {
            this.Traces.Clear();
            await TraceStorageProvider.Instance.ClearAsync();
        }

        private void AllCommandRaiseCanExecuteChanged()
        {
            this.BackCommand.RaiseCanExecuteChanged();
            this.OpenOrCloseSettingsPanelCommand.RaiseCanExecuteChanged();
            this.RemoveCurrentCommand.RaiseCanExecuteChanged();
            this.OpenFullScreenCommand.RaiseCanExecuteChanged();
            this.CopyCommand.RaiseCanExecuteChanged();
        }

        #endregion
    }
}