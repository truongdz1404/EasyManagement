using AntDesign.Charts;
using EasyMN.Shared.Dtos.Report;
using Microsoft.AspNetCore.Components;

namespace ClientApp.Pages
{
    public partial class Report : ComponentBase
    {
        private DashboardStatsDto? stats;
        private List<ClassStatsDto> classStats = new();
        private object[] pieChartData = Array.Empty<object>();
        private object[] columnChartData = Array.Empty<object>();

        private PieConfig pieConfig = new PieConfig
        {
            AppendPadding = 10,
            AngleField = "value",
            ColorField = "type",
            Radius = 0.8,
            Label = new PieLabelConfig { Type = "outer" }
        };

        private ColumnConfig columnConfig = new ColumnConfig
        {
            Title = new AntDesign.Charts.Title
            {
                Visible = true,
                Text = "Students per Class"
            },
            XField = "type",
            YField = "value"
        };

        protected override async Task OnInitializedAsync()
        {
            await LoadDashboardStats();
            await LoadClassStats();
        }

        private async Task LoadDashboardStats()
        {
            var response = await ReportGrpcService.GetDashboardStatsAsync();
            if (!string.IsNullOrEmpty(response.Message) && response.Data != null)
            {
                stats = response.Data;
            }
        }

        private async Task LoadClassStats()
        {
            var response = await ReportGrpcService.GetAllClassStatsAsync();
            classStats = response.Data ?? new List<ClassStatsDto>();
                pieChartData = classStats.Select(c => new { type = c.ClassName, value = c.TotalStudents }).ToArray();
            columnChartData = classStats.Select(c => new { type = c.ClassName, value = c.TotalStudents }).ToArray();

        }
    }
}
