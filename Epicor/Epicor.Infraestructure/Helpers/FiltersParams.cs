
namespace Epicor.Infraestructure.Helpers
{
    public  class FiltersParams
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public FiltersParams(FiltersParamsBuilder builder)
        {
            StartDate = builder.StartDate;
            EndDate = builder.EndDate;
        }


        public class FiltersParamsBuilder
        {
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }

            public FiltersParamsBuilder WithStartDate(DateTime starDate)
            {
                StartDate = starDate;
                return this;
            }

            public FiltersParamsBuilder WithEndDate(DateTime endDate)
            {
                EndDate = endDate;
                return this;
            }

            public FiltersParams Build()
            {
                return new FiltersParams(this);
            }
        }
    }
}
