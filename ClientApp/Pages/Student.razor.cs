using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyMN.Shared.Dtos.ClassRoom;
using EasyMN.Shared.Dtos.Student;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using AntDesign;

namespace ClientApp.Pages
{
    public partial class Student : ComponentBase
    {
        private List<StudentDto>? students = new();
        private List<ClassRoomDto> classrooms = new();
        private string searchText = "";
        private bool showDialog = false;
        private bool isInitialLoading = true;
        private bool isSaving = false;
        private string errorMessage = "";
        private StudentDto currentStudent = new();

        private int pageSize = 5;
        private int currentPage = 1;
        private int totalItems = 0;
        private string? currentSortField;
        private bool? currentSortAscending;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                isInitialLoading = true;
                errorMessage = "";

                await LoadClassrooms();
                await LoadDataInternal(1);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing: {ex.Message}");
                errorMessage = $"Failed to initialize: {ex.Message}";
            }
            finally
            {
                isInitialLoading = false;
                StateHasChanged();
            }
        }


        private async Task HandleTableChange(AntDesign.TableModels.QueryModel<StudentDto> queryModel)
        {
            try
            {
                currentPage = queryModel.PageIndex;
                pageSize = queryModel.PageSize;

                if (queryModel.SortModel != null && queryModel.SortModel.Any())
                {
                    var sortModel = queryModel.SortModel.First();
                    currentSortField = sortModel.FieldName?.Split('.').LastOrDefault();
                    currentSortAscending = sortModel.Sort == "ascend";
                    Console.WriteLine($"Sort Field: {currentSortField}, Sort Ascending: {currentSortAscending}, Original Field: {sortModel.FieldName}");
                }
                else
                {
                    currentSortField = null;
                    currentSortAscending = null;
                }

                await LoadDataInternal(currentPage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error changing page: {ex.Message}");
                errorMessage = $"Error changing page: {ex.Message}";
                StateHasChanged();
            }
        }

        // Chỉ dùng cho search và các thao tác cần loading manual
        private async Task LoadDataWithLoading(int page)
        {
            try
            {
                errorMessage = "";
                await LoadDataInternal(page);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
                students = new List<StudentDto>();
                totalItems = 0;
                errorMessage = $"Error loading data: {ex.Message}";
                StateHasChanged();
            }
        }

        private async Task LoadDataInternal(int page)
        {
            var request = new EasyMN.Shared.Dtos.PagedRequest
            {
                PageNumber = page,
                PageSize = pageSize,
                Keyword = searchText?.Trim() ?? "",
                SortField = currentSortField,
                SortAscending = currentSortAscending
            };
            Console.WriteLine($"Sending request - Sort Field: {request.SortField}, Sort Ascending: {request.SortAscending}");
            var response = await StudentGrpcService.GetAllStudentsAsync(request);

            if (response?.Data != null)
            {
                students = response.Data.Items?.ToList() ?? new List<StudentDto>();
                totalItems = response.Data.TotalItems;
                currentPage = page;
            }
            else
            {
                students = new List<StudentDto>();
                totalItems = 0;
                errorMessage = response?.Message ?? "Failed to load students";
            }
        }

        private async Task LoadClassrooms()
        {
            try
            {
                var response = await ClassGrpcService.GetAllClassRoomsAsync(new());
                if (response?.Data != null)
                {
                    classrooms = response.Data.Items?.ToList() ?? new List<ClassRoomDto>();
                }
                else
                {
                    classrooms = new List<ClassRoomDto>();
                    Console.WriteLine($"Failed to load classrooms: {response?.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading classrooms: {ex.Message}");
                classrooms = new List<ClassRoomDto>();
            }
        }

        private async Task HandleSearchClick()
        {
            try
            {
                currentPage = 1;
                await LoadDataWithLoading(1);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching: {ex.Message}");
                errorMessage = $"Error searching: {ex.Message}";
            }
        }

        private async Task HandleSearchKeypress(KeyboardEventArgs e)
        {
            try
            {
                if (e.Key == "Enter")
                {
                    currentPage = 1;
                    await LoadDataWithLoading(1);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching: {ex.Message}");
                errorMessage = $"Error searching: {ex.Message}";
            }
        }

        private void OpenDialogForNew()
        {
            errorMessage = "";
            currentStudent = new StudentDto
            {
                Dob = DateTime.Today,
                ClassRoomId = 0
            };
            showDialog = true;
            StateHasChanged();
        }

        private void OpenDialogForEdit(StudentDto student)
        {
            errorMessage = "";
            currentStudent = new StudentDto
            {
                Id = student.Id,
                StudentCode = student.StudentCode,
                Name = student.Name,
                Dob = student.Dob,
                Address = student.Address,
                ClassRoomId = student.ClassRoomId
            };
            showDialog = true;
            StateHasChanged();
        }

        private void CloseDialog()
        {
            showDialog = false;
            errorMessage = "";
            currentStudent = new();
            StateHasChanged();
        }

        private bool IsFormValid()
        {
            return !string.IsNullOrWhiteSpace(currentStudent.StudentCode) &&
                   !string.IsNullOrWhiteSpace(currentStudent.Name) &&
                   currentStudent.ClassRoomId > 0;
        }

        private async Task SaveStudent()
        {
            if (!IsFormValid())
            {
                errorMessage = "Please fill in all required fields";
                return;
            }

            try
            {
                if (isSaving) return;

                isSaving = true;
                errorMessage = "";
                StateHasChanged();

                if (currentStudent.Id == 0)
                {
                    var createRequest = new CreateStudentRequest
                    {
                        StudentCode = currentStudent.StudentCode?.Trim(),
                        Name = currentStudent.Name?.Trim(),
                        Dob = currentStudent.Dob,
                        Address = currentStudent.Address?.Trim(),
                        ClassRoomId = currentStudent.ClassRoomId
                    };
                    var response = await StudentGrpcService.AddStudentAsync(createRequest);

                    if (response.Data > 0)
                    {
                        CloseDialog();
                        currentPage = 1;
                        await LoadDataWithLoading(1);
                        StateHasChanged();
                    }
                    else
                    {
                        errorMessage = response.Message ?? "Failed to create student";
                    }
                }
                else
                {
                    var updateRequest = new UpdateStudentRequest
                    {
                        Id = currentStudent.Id,
                        Name = currentStudent.Name?.Trim(),
                        Dob = currentStudent.Dob,
                        Address = currentStudent.Address?.Trim(),
                        ClassRoomId = currentStudent.ClassRoomId
                    };
                    var response = await StudentGrpcService.UpdateStudentAsync(updateRequest);

                    if (response.Data)
                    {
                        CloseDialog();
                        await LoadDataWithLoading(currentPage);
                        StateHasChanged();
                    }
                    else
                    {
                        errorMessage = response.Message ?? "Failed to update student";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving student: {ex.Message}");
                errorMessage = $"Error saving student: {ex.Message}";
            }
            finally
            {
                isSaving = false;
                StateHasChanged();
            }
        }

        private async Task DeleteStudent(int studentId)
        {
            try
            {
                var response = await StudentGrpcService.DeleteStudentAsync(new StudentRequest { Id = studentId });
                if (response.Data)
                {
                    await LoadDataWithLoading(currentPage);
                }
                else
                {
                    errorMessage = response.Message ?? "Failed to delete student";
                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting student: {ex.Message}");
                errorMessage = $"Error deleting student: {ex.Message}";
                StateHasChanged();
            }
        }
    }
}