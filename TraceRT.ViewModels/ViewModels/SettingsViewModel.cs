namespace TraceRT.ViewModels.ViewModels
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TraceRT.DataAccess.Providers;

    public class SettingsViewModel : ViewModelBase
    {
        public bool CanPingUntilStopped
        {
            get
            {
                return PingSettingsProvider.Instance.CanPingUntilStopped;
            }

            set
            {
                PingSettingsProvider.Instance.CanPingUntilStopped = value;
                this.RaisePropertyChanged();
            }
        }

        public bool CanPingHyperV
        {
            get
            {
                return PingSettingsProvider.Instance.CanPingHyperV;
            }

            set
            {
                PingSettingsProvider.Instance.CanPingHyperV = value;
                this.RaisePropertyChanged();
            }
        }

        public bool CanPingIpV4
        {
            get
            {
                return PingSettingsProvider.Instance.CanPingIpV4;
            }

            set
            {
                PingSettingsProvider.Instance.CanPingIpV4 = value;
                this.RaisePropertyChanged();
            }
        }

        public bool CanPingIpV6
        {
            get
            {
                return PingSettingsProvider.Instance.CanPingIpV6;
            }

            set
            {
                PingSettingsProvider.Instance.CanPingIpV6 = value;
                this.RaisePropertyChanged();
            }
        }

        public int BufferLength
        {
            get
            {
                return PingSettingsProvider.Instance.BufferLength;
            }

            set
            {
                PingSettingsProvider.Instance.BufferLength = value;
                this.RaisePropertyChanged();
            }
        }

        public int RequestNumber
        {
            get
            {
                return PingSettingsProvider.Instance.RequestNumber;
            }

            set
            {
                PingSettingsProvider.Instance.RequestNumber = value;
                this.RaisePropertyChanged();
            }
        }

        public int Ttl
        {
            get
            {
                return PingSettingsProvider.Instance.Ttl;
            }

            set
            {
                PingSettingsProvider.Instance.Ttl = value;
                this.RaisePropertyChanged();
            }
        }

        public int PingTimeOut
        {
            get
            {
                return PingSettingsProvider.Instance.TimeOut;
            }

            set
            {
                PingSettingsProvider.Instance.TimeOut = value;
                this.RaisePropertyChanged();
            }
        }

        public bool IsTraceDontResolveAddress
        {
            get
            {
                return TraceSettingsProvider.Instance.IsTraceDontResolveAddress;
            }

            set
            {
                TraceSettingsProvider.Instance.IsTraceDontResolveAddress = value;
                this.RaisePropertyChanged();
            }
        }

        public bool RoundTrip
        {
            get
            {
                return TraceSettingsProvider.Instance.RoundTrip;
            }

            set
            {
                TraceSettingsProvider.Instance.RoundTrip = value;
                this.RaisePropertyChanged();
            }
        }

        public bool CanTraceIpV4
        {
            get
            {
                return TraceSettingsProvider.Instance.CanTraceIpV4;
            }

            set
            {
                TraceSettingsProvider.Instance.CanTraceIpV4 = value;
                this.RaisePropertyChanged();
            }
        }

        public bool CanTraceIpV6
        {
            get
            {
                return TraceSettingsProvider.Instance.CanTraceIpV6;
            }

            set
            {
                TraceSettingsProvider.Instance.CanTraceIpV6 = value;
                this.RaisePropertyChanged();
            }
        }

        public string Source
        {
            get
            {
                return TraceSettingsProvider.Instance.Source;
            }

            set
            {
                TraceSettingsProvider.Instance.Source = value;
                this.RaisePropertyChanged();
            }
        }

        public int MaxHops
        {
            get
            {
                return TraceSettingsProvider.Instance.MaxHops;
            }

            set
            {
                TraceSettingsProvider.Instance.MaxHops = value;
                this.RaisePropertyChanged();
            }
        }

        public int TraceTimeOut
        {
            get
            {
                return TraceSettingsProvider.Instance.TimeOut;
            }

            set
            {
                TraceSettingsProvider.Instance.TimeOut = value;
                this.RaisePropertyChanged();
            }
        }

        public RelayCommand InitializeSettingsCommand { get; set; }

        private void InitializeSettingsExecute()
        {
            PingSettingsProvider.Instance.Load();
            this.RaisePropertyChanged(() => this.CanPingUntilStopped);
            this.RaisePropertyChanged(() => this.CanPingIpV4);
            this.RaisePropertyChanged(() => this.CanPingIpV6);
            this.RaisePropertyChanged(() => this.CanPingHyperV);
            this.RaisePropertyChanged(() => this.RequestNumber);
            this.RaisePropertyChanged(() => this.BufferLength);
            this.RaisePropertyChanged(() => this.Ttl);
            this.RaisePropertyChanged(() => this.PingTimeOut);

            TraceSettingsProvider.Instance.Load();
            this.RaisePropertyChanged(() => this.IsTraceDontResolveAddress);
            this.RaisePropertyChanged(() => this.RoundTrip);
            this.RaisePropertyChanged(() => this.CanTraceIpV4);
            this.RaisePropertyChanged(() => this.CanTraceIpV6);
            this.RaisePropertyChanged(() => this.Source);
            this.RaisePropertyChanged(() => this.MaxHops);
            this.RaisePropertyChanged(() => this.TraceTimeOut);
        }

    }
}
