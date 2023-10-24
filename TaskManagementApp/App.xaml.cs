using Microsoft.EntityFrameworkCore;
using TaskManagementApp.Data;
using TaskManagementApp.Models;
using TaskManagementApp.Services;
using TaskManagementApp.Stores;
using TaskManagementApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Windows;

namespace TaskManagementApp
{
    public partial class App : Application
    {
        private const string CONNECTION_STRING = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = MVVMLoginDb2;";

        private readonly AppDbContextFactory _appDbContextFactory;
        private readonly NavigationStore _navigationStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        private readonly ISessionService _sessionService;
        private readonly IUserService _userService;
        private readonly ITaskService _taskService;
        private readonly INavigationService _closeModalNavigationService;

        public App(){
            _appDbContextFactory = new AppDbContextFactory(CONNECTION_STRING);
            _navigationStore = new NavigationStore();
            _modalNavigationStore = new ModalNavigationStore();

            _userService = new UserService(_appDbContextFactory);
            _taskService = new TaskService(_appDbContextFactory);
            _sessionService = new SessionService();
            _closeModalNavigationService = new CloseModalNavigationService(_modalNavigationStore);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");

            using (AppDbContext dbContext = _appDbContextFactory.CreateDbContext()) {
                dbContext.Database.Migrate();
            }

            System.Threading.Tasks.Task.Run(async () => await PopulateDatabaseAsync()).Wait();

            _navigationStore.CurrentViewModel = CreateLoginViewModel();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore, _modalNavigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

        private LoginViewModel CreateLoginViewModel()
        {
            return new LoginViewModel( _userService,
                                       _sessionService,
                                       new NavigationService(_navigationStore, CreateRegisterViewModel),
                                       new NavigationService(_navigationStore, CreateTasksMenuViewModel));
        }

        private RegisterViewModel CreateRegisterViewModel()
        {
            return new RegisterViewModel( _userService,
                                          new NavigationService(_navigationStore, CreateLoginViewModel));
        }

        private TasksMenuViewModel CreateTasksMenuViewModel()
        {
            return TasksMenuViewModel.LoadTasksMenuViewModel( _taskService,
                                                              _sessionService,
                                                              new NavigationService(_navigationStore, CreateLoginViewModel),
                                                              new ModalNavigationService(_modalNavigationStore, CreateCreateTaskViewModel),
                                                              new ModalNavigationService(_modalNavigationStore, CreateUpdateTaskViewModel));
        }

        private CreateTaskViewModel CreateCreateTaskViewModel()
        {

            return new CreateTaskViewModel( _taskService,
                                            _sessionService,
                                            _closeModalNavigationService);
        }

        private UpdateTaskViewModel CreateUpdateTaskViewModel()
        {

            return new UpdateTaskViewModel(_taskService,
                                           _closeModalNavigationService);
        }


        private async System.Threading.Tasks.Task PopulateDatabaseAsync()
        {
            using (AppDbContext dbContext = _appDbContextFactory.CreateDbContext()) {
                if (await dbContext.Tasks.AnyAsync()) return;

                Guid user1Id = Guid.NewGuid();
                Guid user2Id = Guid.NewGuid();

                var users = new List<User> {
                    new User {
                        Id = user1Id,
                        Username = "Test1",
                        Email = "Test1",
                        Password = PasswordHash.CreateHash("qwerty"),
                    },
                     new User {
                        Id = user2Id,
                        Username = "Test2",
                        Email = "Test2",
                        Password = PasswordHash.CreateHash("qwerty"),
                    }
                };

                var tasks = new List<Models.Task>{
                    new Models.Task{
                        Id = Guid.NewGuid(),
                        Name = "Task title 1",
                        Description = "Task desc 1",
                        Deadline = new DateTime(1995, 1, 2),
                        Priority = 20,
                        IsHidden = false,
                        UserId = user1Id
                    },
                    new Models.Task{
                        Id = Guid.NewGuid(),
                        Name = "Task title 2",
                        Description = "Task desc 2",
                        Deadline = new DateTime(1995, 2, 2),
                        Priority = 2,
                        IsHidden = false,
                        UserId = user2Id
                    },
                    new Models.Task{
                        Id = Guid.NewGuid(),
                        Name = "Task title 3",
                        Description = "Task desc 3",
                        CreatedTime = new DateTime(1995, 3, 1),
                        Deadline = new DateTime(1995, 3, 2),
                        Priority = 10,
                        IsHidden = false,
                        UserId = user1Id
                    },
                    new Models.Task{
                        Id = Guid.NewGuid(),
                        Name = "Task title 4",
                        Description = "Task desc 4",
                        CreatedTime = new DateTime(1995, 4, 1),
                        Deadline = new DateTime(1995, 4, 2),
                        Priority = 22,
                        IsHidden = false,
                        UserId = user1Id
                    },
                    new Models.Task{
                        Id = Guid.NewGuid(),
                        Name = "Task title 5",
                        Description = "Task desc 5",
                        CreatedTime = new DateTime(1995, 5, 1),
                        Deadline = new DateTime(1995, 5, 2),
                        Priority = 16,
                        IsHidden = false,
                        UserId = user1Id
                    },
                };

                dbContext.AddRange(users);
                dbContext.AddRange(tasks);

                dbContext.SaveChanges();
            }
        }
    }
}
