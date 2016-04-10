namespace TraceRT.Models.Models
{
    using GalaSoft.MvvmLight;
    using Newtonsoft.Json;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading;

    public class PingModel : ObservableObject
    {
        private bool isReady;

        private long timeStamp;
        private long send;
        private long received;
        private long lost;
        private double loss;
        private long min;
        private long max;
        private double average;

        public PingModel(string host, int bufferLength)
        {
            this.Host = host;
            this.BufferLength = bufferLength;
            this.IsWorking = false;
            this.isReady = false;
            this.Replies = new ObservableCollection<EchoReply>();
            this.CancellationTokenSource = new CancellationTokenSource();
        }

        #region Properties

        public string Host { get; set; }

        public int BufferLength { get; set; }

        public bool IsWorking { get; set; }

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

        public long TimeStamp
        {
            get 
            { 
                return this.timeStamp; 
            }

            set
            {
                this.timeStamp = value;
                this.RaisePropertyChanged();
            }
        }

        public long Send
        {
            get 
            { 
                return this.send; 
            }

            set
            {
                this.send = value;
                this.RaisePropertyChanged();
            }
        }

        public long Received
        {
            get 
            { 
                return this.received; 
            }

            set
            {
                this.received = value;
                this.RaisePropertyChanged();
            }
        }

        public long Lost
        {
            get 
            { 
                return this.lost; 
            }

            set
            {
                this.lost = value;
                this.RaisePropertyChanged();
            }
        }

        public double Loss
        {
            get 
            { 
                return this.loss;
            }

            set
            {
                this.loss = value;
                this.RaisePropertyChanged();
            }
        }

        public long MinimumTime
        {
            get 
            { 
                return this.min; 
            }

            set
            {
                this.min = value;
                this.RaisePropertyChanged();
            }
        }

        public long MaximumTime
        {
            get 
            {
                return this.max; 
            }

            set
            {
                this.max = value;
                this.RaisePropertyChanged();
            }
        }

        public double AverageTime
        {
            get 
            { 
                return this.average; 
            }

            set
            {
                this.average = value;
                this.RaisePropertyChanged();
            }
        }

        public ObservableCollection<EchoReply> Replies { get; set; }

        [JsonIgnore]
        public CancellationTokenSource CancellationTokenSource { get; private set; }
        #endregion

        public void Start() 
        {
            this.IsWorking = true;
            this.IsReady = false;
        }

        public void Add(EchoReply newPing) 
        {
            if (newPing != null && this.IsWorking && !this.IsReady) 
            {
                this.Replies.Add(newPing);
            }
        }

        public void Finish() 
        {
            this.Send = this.Replies.Count;
            this.Received = this.Replies.Count((r) => r.Status.Equals("Success"));
            this.Lost = this.send - this.received;
            this.Loss = 100 * this.lost / this.send;
            var successReply = from reply in this.Replies where reply.Status.Equals("Success") select reply;
            if (successReply.Any())
            {
                this.MinimumTime = successReply.Min(r => r.RoundtripTime);
                this.MaximumTime = successReply.Max(r => r.RoundtripTime);
                this.AverageTime = successReply.Average(r => r.RoundtripTime);
            }

            this.IsWorking = false;
            this.IsReady = true;
        }
    }
}
