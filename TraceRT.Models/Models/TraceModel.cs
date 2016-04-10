namespace TraceRT.Models.Models
{
    using GalaSoft.MvvmLight;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading;

    public class TraceModel : ObservableObject
    {
        private bool isReady;

        public TraceModel(string host) 
        {
            this.Host = host;
            this.Nodes = new ObservableCollection<EchoReply>();
            this.NodesGeoInformation = new ObservableCollection<HostGeoInformation>();
            this.CancellationTokenSource = new CancellationTokenSource();
        }

        #region Property
        public string Host { get; set; }

        public bool IsReady
        {
            get
            {
                return this.isReady;
            }

            set
            {
                this.isReady = value;
                this.RaisePropertyChanged();
            }
        }

        public ObservableCollection<EchoReply> Nodes { get; set; }

        public ObservableCollection<HostGeoInformation> NodesGeoInformation { get; set; }

        [JsonIgnore]
        public CancellationTokenSource CancellationTokenSource { get; private set; }
        #endregion
    }
}