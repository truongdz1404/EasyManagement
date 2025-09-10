using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyMN.Shared.Dtos.ClassRoom;
using EasyMN.Shared.Dtos.Student;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace ClientApp.Pages
{
    public partial class Student : ComponentBase
    {
        private List<StudentDto>? students = new();
        private List<ClassRoomDto> classrooms = new();
        private string searchText = "";
        private bool showDialog = false;
        private bool isLoading = true;
        private bool isSaving = false;
        private string errorMessage = "";
        private StudentDto currentStudent = new();

        // Pagination
        private int pageSize = 5;
        private int currentPage = 1;
        private int totalItems = 0;
        private int totalPages => totalItems > 0 ? (int)Math.Ceiling(totalItems / (double)pageSize) : 1;
        private int startPage => Math.Max(1, currentPage - 2);
        private int endPage => Math.Min(totalPages, startPage + 4);

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await LoadClassrooms();
                await LoadData(1);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing: {ex.Message}");
                errorMessage = $"Failed to initialize: {ex.Message}";
            }
        }
        private int SelectedClassRoomId
        {
            get => currentStudent.ClassRoomId;
            set
            {
                currentStudent.ClassRoomId = value;
                Console.WriteLine($"ClassRoomId set to: {value}"); // Debug
            }
        }
        private async Task LoadData(int page)
        {
            try
            {
                isLoading = true;
                errorMessage = "";
                await InvokeAsync(StateHasChanged); // Force UI update

                currentPage = page;
                var response = await StudentGrpcService.GetAllStudentsAsync(new EasyMN.Shared.Dtos.PagedRequest
                {
                    PageNumber = page,
                    PageSize = pageSize,
                    Keyword = searchText?.Trim() ?? ""
                });

                if (response?.Data != null)
                {
                    students = response.Data.Items?.ToList() ?? new List<StudentDto>();
                    totalItems = response.Data.TotalItems;
                }
                else
                {
                    students = new List<StudentDto>();
                    totalItems = 0;
                    errorMessage = response?.Message ?? "Failed to load students";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
                students = new List<StudentDto>();
                totalItems = 0;
                errorMessage = $"Error loading data: {ex.Message}";
            }
            finally
            {
                isLoading = false;
                await InvokeAsync(StateHasChanged); // Force UI update
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
            if (!isLoading)
            {
                await LoadData(1);
            }
        }

        private async Task HandleSearchKeypress(KeyboardEventArgs e)
        {
            if (e.Key == "Enter" && !isLoading)
            {
                await LoadData(1);
            }
        }

        private async Task GoToPage(int page)
        {
            if (page >= 1 && page <= totalPages && page != currentPage && !isLoading)
            {
                await LoadData(page);
            }
        }

        private void OpenDialogForNew()
        {
            errorMessage = "";
            currentStudent = new StudentDto
            {
                Dob = DateTime.Today,
                ClassRoomId = 0 // Đặt default, user sẽ chọn
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
                        await LoadData(currentPage);
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
                        await LoadData(currentPage);
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
            if (isLoading) return;

            try
            {
                var response = await StudentGrpcService.DeleteStudentAsync(new StudentRequest { Id = studentId });
                if (response.Data)
                {
                    await LoadData(currentPage);
                }
                else
                {
                    errorMessage = response.Message ?? "Failed to delete student";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting student: {ex.Message}");
                errorMessage = $"Error deleting student: {ex.Message}";
            }
        }
    }
}