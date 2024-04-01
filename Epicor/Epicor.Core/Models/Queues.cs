
namespace Epicor.Core.Models
{
    public class Queues
    {
        public string Name { get; set; }
        public int Total { get; set; }
        public int TotalClosed { get; set; }
        public int TotalOpen { get; set; }
        public string Urgency { get; set; }
        public string Priority { get; set; }

        public int RangeOne { get; set; }
        public int RangeTwo { get; set; }
        public int RangeThree { get; set; }
        public int RangeFour { get; set; }
        public int RangeFive { get; set; }
        public string Status { get; set; }


        private Queues(QueuesBuilder builder)
        {
            Name = builder.Name;
            Total = builder.Total;
            Urgency = builder.Urgency;
            Priority = builder.Priority;
            TotalClosed = builder.TotalClosed;
            TotalOpen = builder.TotalOpen;
            RangeOne = builder.RangeOne;
            RangeTwo = builder.RangeTwo;
            RangeThree = builder.RangeThree;
            RangeFour = builder.RangeFour;
            RangeFive = builder.RangeFive;
            Status = builder.Status;
        }

        public class QueuesBuilder
        {
            public string Name { get; private set; }
            public int Total { get; private set; }
            public int TotalClosed { get; set; }
            public int TotalOpen { get; set; }
            public string Urgency { get; private set; }
            public string Priority { get; set; }

            public int RangeOne { get; set; }
            public int RangeTwo { get; set; }
            public int RangeThree { get; set; }
            public int RangeFour { get; set; }
            public int RangeFive { get; set; }

            public string Status { get; set; }

            public QueuesBuilder WithName(string name)
            {
                Name = name;
                return this;
            }

            public QueuesBuilder WithTotal(int total)
            {
                Total = total;
                return this;
            }

            public QueuesBuilder WithTotalClosed(int totalClosed)
            {
                TotalClosed = totalClosed;
                return this;
            }

            public QueuesBuilder WithTotalOpen(int totalOpen)
            {
                TotalOpen = totalOpen;
                return this;
            }


            public QueuesBuilder WithUrgency(string urgency)
            {
                Urgency = urgency;
                return this;
            }

            public QueuesBuilder WithPriority(string priority)
            {
                Priority = priority;
                return this;
            }

            public QueuesBuilder WithRangeOne(int rangeOne)
            {
                RangeOne = rangeOne;
                return this;
            }

            public QueuesBuilder WithRangeTwo(int rangeTwo)
            {
                RangeTwo = rangeTwo;
                return this;
            }

            public QueuesBuilder WithRangeThree(int rangeThree)
            {
                RangeThree = rangeThree;
                return this;
            }

            public QueuesBuilder WithRangeFour(int rangeFour)
            {
                RangeFour = rangeFour;
                return this;
            }

            public QueuesBuilder WithRangeFive(int rangeFive)
            {
                RangeFive = rangeFive;
                return this;
            }

            public QueuesBuilder WithStatus(string status)
            {
                Status = status;
                return this;
            }

            public Queues Build()
            {
                return new Queues(this);
            }
        }
    }

}
