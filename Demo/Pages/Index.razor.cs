using Microsoft.AspNetCore.Components;

namespace Aptacode.Geometry.Demo.Pages
{
    public class IndexBase : ComponentBase
    {
        #region Dependencies

        [Inject]
        public ILogger<IndexBase> Logger { get; set; } = null!;

        #endregion

        #region Properties

        public IEnumerable<ProfileFunctionResult> Results = new List<ProfileFunctionResult>();

        #endregion

        protected void RunOnClick()
        {
            var runner = new ProfileRunner(GeometryProfileFunctions.GeometryFunctions());
            Results = runner.Run(10, 100);
            OrderResults();

            StateHasChanged();
        }

        private string _order = "Group";
        private void OrderResults()
        {
            Results = _order switch
            {
                "Group" => Results.OrderByDescending(r => r.Title),
                "Fastest" => Results.OrderBy(r => r.Average),
                "Slowest" => Results.OrderByDescending(r => r.Average),
                _ => Results
            };
        }
        
        protected void OrderByGroup()
        {
            _order = "Group";
            OrderResults();
            StateHasChanged();
        }       
        
        protected void OrderByFastest()
        {
            _order = "Fastest";
            OrderResults();
            StateHasChanged();
        }
        protected void OrderBySlowest()
        {
            _order = "Slowest";
            OrderResults();
            StateHasChanged();
        }

    }
}
